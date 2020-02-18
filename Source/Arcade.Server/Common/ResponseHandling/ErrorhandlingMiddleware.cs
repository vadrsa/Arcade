using Common.Core;
using Common.Faults;
using Common.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SharedEntities;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Common.ResponseHandling
{
    public class ErrorhandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger _logger;

        public ErrorhandlingMiddleware(RequestDelegate next, ILogger<ErrorhandlingMiddleware> logger)
        {
            this.next = next;
            this._logger = logger;
        }

        public async Task Invoke(HttpContext context, IFaultManager faultManager)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, faultManager);
            }
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception exception, IFaultManager faultManager)
        {
            try
            {
                Type type = exception.GetType();
                context.Response.ContentType = "application/json";
                FaultType faultType;
                string exceptionMessage = null;
                object descriptor = null;
                if (exception is FaultException)
                {
                    exceptionMessage = (exception as FaultException).Message;
                    faultType = (exception as FaultException).Type;
                    descriptor = (exception as FaultException).Descriptor;
                    _logger.LogFault(context.TraceIdentifier, exception as FaultException);
                }
                else
                {
                    _logger.LogCritical(exception, context.TraceIdentifier + " Internal Error");
                    faultType = FaultType.UnexpectedError;
                }
                Fault fault = await faultManager.GetByType(faultType);

                if (String.IsNullOrEmpty(exceptionMessage))
                    exceptionMessage = fault.Description;
                context.Response.StatusCode = (int)fault.HttpStatusCode;

                await context.WriteErrorDataAsync(new FaultResponse(fault.Code, exceptionMessage, context.TraceIdentifier, fault.HttpStatusCode, descriptor));
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, context.TraceIdentifier + " Internal Error");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
