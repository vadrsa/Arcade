using Common.Core;
using Common.Enums;
using Common.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;

namespace Common.ResponseHandling
{
    public class ErrorHandlingMiddleware
    {
        private ResponseProvider responseProvider;
        private readonly RequestDelegate next;
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ResponseProvider responseProvider, ILoggerFactory loggerFactory, ILogger<ErrorHandlingMiddleware> logger)
        {
            this.responseProvider = responseProvider;
            this.next = next;
            this._logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            try
            {
                Type type = exception.GetType();
                IRequestCultureFeature rqf = context.Request.HttpContext.Features.Get<IRequestCultureFeature>();
                CultureInfo culture = rqf.RequestCulture.Culture;
                context.Response.ContentType = "application/json";
                FaultCode fault;
                string overrideMessage = null;
                if (exception is ApiException)
                {
                    overrideMessage = (exception as ApiException).OverrideMessage;
                    fault = (exception as ApiException).FaultCode;
                    _logger.LogFault(exception as ApiException);
                }
                else
                {
                    _logger.LogCritical(exception, "Internal Error");
                    fault = FaultCode.Undefined;
                }
                FaultResponse error = responseProvider.GetFaultResponse(fault, culture);

                context.Response.StatusCode = (int)error.HttpStatusCode;
                if(!String.IsNullOrEmpty(overrideMessage))
                    return context.WriteErrorDataAsync(overrideMessage);
                else if (!String.IsNullOrEmpty(error.Message))
                    return context.WriteErrorDataAsync(error.Message);
                return Task.FromResult(0);
            }
            catch(Exception e)
            {
                _logger.LogCritical(e, "Internal Error");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Task.FromResult(0);
            }
        }
    }
}
