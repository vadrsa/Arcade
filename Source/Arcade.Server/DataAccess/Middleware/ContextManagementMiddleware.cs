using Common.Faults;
using Common.ResponseHandling;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DataAccess.Middleware
{
    public class ContextManagementMiddleware
    {
        private readonly RequestDelegate _next;

        public ContextManagementMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            bool error = false;
            try
            {
                await _next(httpContext);
            }
            catch(Exception e)
            {
                error = true;
                ErrorhandlingMiddleware errorHandling = new ErrorhandlingMiddleware(null, httpContext.RequestServices.GetService<ILogger<ErrorhandlingMiddleware>>());
                await errorHandling.HandleExceptionAsync(httpContext, e, httpContext.RequestServices.GetService<IFaultManager>());
            }
            finally
            {
                var context = httpContext.RequestServices.GetService<ArcadeContext>();
                try
                {
                    if (!error)
                        await context.SaveChangesAsync();
                }
                catch(Exception e)
                {
                    ErrorhandlingMiddleware errorHandling = new ErrorhandlingMiddleware(null, httpContext.RequestServices.GetService<ILogger<ErrorhandlingMiddleware>>());
                    await errorHandling.HandleExceptionAsync(httpContext, e, httpContext.RequestServices.GetService<IFaultManager>());
                }
                finally
                {
                    context.Dispose();

                }
            }
        }
    }
}
