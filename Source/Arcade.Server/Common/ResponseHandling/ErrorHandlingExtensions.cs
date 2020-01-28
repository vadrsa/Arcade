using Microsoft.AspNetCore.Builder;

namespace Common.ResponseHandling
{
    public static class ErrorHandlingExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorhandlingMiddleware>();
        }
    }
}
