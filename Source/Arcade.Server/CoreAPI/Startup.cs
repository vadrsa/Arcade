using AutoMapper;
using BusinessEntities;
using Common.Configuration;
using Common.Core;
using Common.Faults;
using Common.Filters;
using Common.ResponseHandling;
using DataAccess;
using DataAccess.Repositories;
using Facade.Configuration;
using Facade.Managers;
using Facade.Repositories;
using FluentValidation.AspNetCore;
using LinqToDB.Data;
using LinqToDB.DataProvider.SqlServer;
using LinqToDB.Identity;
using Managers.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Text;

namespace CoreAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Set Linq2DB Connection String
            DataConnection
                .AddConfiguration(
                    "Default",
                    Configuration.GetConnectionString("arcade"),
                    new SqlServerDataProvider("Default", SqlServerVersion.v2012));

            DataConnection.DefaultConfiguration = "Default";

            // Configure Linq2DB Identity
            services.AddIdentity<User, Role>()
                .AddLinqToDBStores(new DefaultConnectionFactory())
                .AddDefaultTokenProviders();

            LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = true;

            #region Authentication Configuration

            // Configure Jwt Security
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["JwtIssuer"],
                        ValidAudience = Configuration["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                })
                .AddCookie(options =>
                {
                    options.Cookie.Name = "Interop";
                    options.DataProtectionProvider =
                        DataProtectionProvider.Create(new DirectoryInfo("C:\\Github\\Identity\\artifacts"));

                });

            #endregion

            // Add Managers to service collection
            AddManagers(services);

            // Allow Cross Site Access
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddMvc(opt =>
            {
                // Set up entity fluid validation
                opt.Filters.Add<ValidatorActionFilter>();
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    // Configure JSON serialization and deserialization options
                    // Set Enum Converter
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                    // Ignore null values
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                })
                // Add the fuelint validation
                .AddFluentValidation();
            services.AddResponseCaching();
            services.AddMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = ".iSmartBar.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(60);
                options.Cookie.IsEssential = true;
            });
            services.AddScoped<ArcadeContext>();
            //var mapperConfiguration =  new MapperConfiguration(conf => conf.AddMaps(Assembly.GetAssembly(typeof(ProfileLocator))));
            //services.AddSingleton<AutoMapper.IConfigurationProvider>(mapperConfiguration);
            //services.AddSingleton<IMapper>(mapperConfiguration.CreateMapper());
            services.AddOptions();
            IConfigurationSection globalOptionsSection = Configuration.GetSection("GlobalOptions");

            services.AddAutoMapper(typeof(ProfileLocator).Assembly);

            GlobalOptions globalOptions = globalOptionsSection.Get<GlobalOptions>();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<ServiceProvider>(services.BuildServiceProvider());
        }

        private void AddManagers(IServiceCollection services)
        {
            services.AddTransient<IAuthenticationManager, AuthenticationManager>();
            services.AddTransient<IUserRolesManager, UserRolesManager>();
            services.AddTransient<IImageManager, ImageManager>();
            services.AddTransient<IAssetManager, AssetManager>();
            services.AddTransient<IGameManager, GameManager>();
            services.AddTransient<IFaultManager, FaultManager>();
            services.AddTransient<IEmployeeManager, EmployeeManager>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IGameRepository, GameRepository>();
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<ISystemSettingManager, SystemSettingManager>();
            services.AddTransient<ISystemSettingRepository, SystemSettingRepository>();
            services.AddTransient<IComputerTypeRepository, ComputerTypeRepository>();
            services.AddTransient<IComputerTypeManager, ComputerTypeManager>();
            services.AddTransient<IComputerRepository, ComputerRepository>();
            services.AddTransient<IComputerManager, ComputerManager>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<ISessionRepository, SessionRepository>();
            services.AddTransient<ISessionManager, SessionManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseStaticFiles();
            app.UseResponseCaching();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();
            app.UseRequestLocalization();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseHttpContext();
            app.UseMiddleware<ErrorhandlingMiddleware>();
            //app.UseSimulatedLatency(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(5));
            app.UseMvc();
        }
    }
}
