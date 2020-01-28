using Common.Enums;
using Common.ResponseHandling;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SharedEntities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Core
{
    public static class HttpContextExtensions
    {
        private static IHttpContextAccessor m_httpContextAccessor;

        public static HttpContext Current => m_httpContextAccessor.HttpContext;

        public static string AppBaseUrl => $"{Current.Request.Scheme}://{Current.Request.Host}";

        internal static void Configure(IHttpContextAccessor contextAccessor)
        {
            m_httpContextAccessor = contextAccessor;
        }

        public static Task WriteErrorDataAsync(this HttpContext context, FaultResponse response, CancellationToken token = default(CancellationToken))
        {
            return context.Response.WriteAsync(JsonConvert.SerializeObject(response), token);
        }

        public static CultureInfo GetCulture(this HttpContext context)
        {
            IRequestCultureFeature rqf = context.Request.HttpContext.Features.Get<IRequestCultureFeature>();
            if (rqf == null) throw new Exception("IRequestCultureFeature is required for culture identification");
            CultureInfo culture = rqf.RequestCulture.Culture;
            return culture;
        }

        public static void AddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static IApplicationBuilder UseHttpContext(this IApplicationBuilder app)
        {
            HttpContextExtensions.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
            return app;
        }

    }
}
