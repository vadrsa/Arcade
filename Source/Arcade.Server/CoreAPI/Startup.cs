using AutoMapper;
using Common.Configuration;
using Common.Core;
using Common.Enums;
using Common.Filters;
using Common.Localization;
using Common.ResponseHandling;
using Common.Services;
using Facade.Configuration;
using Facade.Managers;
using FluentValidation.AspNetCore;
using Managers.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
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

            services.AddHttpContextAccessor();
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

            Mapper.Initialize(cfg => cfg.AddProfiles(typeof(ProfileLocator).Assembly));
            services.AddOptions();
            IConfigurationSection globalOptionsSection = Configuration.GetSection("GlobalOptions");

            GlobalOptions globalOptions = globalOptionsSection.Get<GlobalOptions>();
            services.Configure<GlobalOptions>(globalOptionsSection);
            // Configure Response Options
            services.Configure<ResponseOptions>(options =>
            {
                options.Dictionary = new LocalizationDictionary<object, ApiResponse>();
                options.Dictionary.Add(
                    "en",
                    new Dictionary<object, ApiResponse>
                    {
                        [FaultCode.InvalidInput] = new FaultResponse(HttpStatusCode.BadRequest, "Invalid data"),
                        [FaultCode.InvalidID] = new FaultResponse(HttpStatusCode.BadRequest, "Invalid ID"),
                        [FaultCode.InvalidLimit] = new FaultResponse(HttpStatusCode.BadRequest, "Invalid Limit"),
                        [FaultCode.InvalidPage] = new FaultResponse(HttpStatusCode.BadRequest, "Invalid Page"),
                        [FaultCode.InvalidUserCredentials] = new FaultResponse(HttpStatusCode.BadRequest, "Invalid Credentials"),
                        [FaultCode.NotAllCulturesProvided] = new FaultResponse(HttpStatusCode.BadRequest, "Data must be provided in all supported languages."),
                    }
                );
            });
            services.AddSingleton<ResponseProvider>();
            services.AddSingleton<CurrencyProvider>();
            services.AddNotifications();
            services.AddSignalR();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<ServiceProvider>(services.BuildServiceProvider());

        }

        private void AddManagers(IServiceCollection services)
        {
            services.AddTransient<IAuthenticationManager, Managers.Implementation.AuthenticationManager>();
            services.AddTransient<IUserRolesManager, UserRolesManager>();
            services.AddTransient<IImageManager, ImageManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseResponseCaching();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            //app.UseSimulatedLatency(TimeSpan.FromMilliseconds(2000), TimeSpan.FromMilliseconds(6000));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseErrorHandling();
            app.UseSession();
            app.UseRequestLocalization();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
