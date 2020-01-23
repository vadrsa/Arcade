using System;
using System.Collections.Generic;

namespace Common.Services
{
    public class NotificationOptions
    {
        IServiceProvider ServiceProvider;

        internal NotificationOptions(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        internal List<INotificationHandler> Handlers = new List<INotificationHandler>();

        public void AddHandler(INotificationHandler handler)
        {
            Handlers.Add(handler);
        }

        public void AddHandler<T>() where T : INotificationHandler
        {
            Handlers.Add((T)ServiceProvider.GetService(typeof(T)));
        }
    }
}
