using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Services
{
    /// <summary>
    /// Service used to trigger notification
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Trigger a notification
        /// </summary>
        /// <param name="name">name of the notification</param>
        void Trigger(string name, object data = null);

    }
}
