using Common.Enums;
using Common.ResponseHandling;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Newtonsoft.Json;
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
        public static Task WriteErrorDataAsync(this HttpContext context, string message, CancellationToken token = default(CancellationToken))
        {
            string traceId = context.TraceIdentifier;
            int status = context.Response.StatusCode;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(new { message, traceId, status }), token);
        }

        public static CultureInfo GetCulture(this HttpContext context)
        {
            IRequestCultureFeature rqf = context.Request.HttpContext.Features.Get<IRequestCultureFeature>();
            if (rqf == null) throw new Exception("IRequestCultureFeature is required for culture identification");
            CultureInfo culture = rqf.RequestCulture.Culture;
            return culture;
        }
    }
}
