using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Services
{
    public static class NotificationExtensions
    {
        public static IServiceCollection AddNotifications(this IServiceCollection services)
        {
            return services.AddSingleton<INotificationService, NotificationService>();
        }

        private static IApplicationBuilder UseNotifications(this IApplicationBuilder app, NotificationOptions options)
        {
            var notificationService = app.ApplicationServices.GetService<INotificationService>();
            foreach (var handler in options.Handlers)
                (notificationService as NotificationService).AddHandler(handler);
            return app;
        }

        public static IApplicationBuilder UseNotifications(this IApplicationBuilder app, Action<NotificationOptions> optionsAction)
        {
            var options = new NotificationOptions(app.ApplicationServices);
            optionsAction.Invoke(options);
            return app.UseNotifications(options);
        }
    }
}
