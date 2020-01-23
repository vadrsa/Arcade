using Common.Middleware;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Core
{
    public static class AppBuilderExtansions
    {
        public static IApplicationBuilder UseSimulatedLatency(
            this IApplicationBuilder app, TimeSpan min, TimeSpan max)
        {
            return app.UseMiddleware(
                typeof(SimulatedLatencyMiddleware),
                min,
                max
            );
        }
    }
}
