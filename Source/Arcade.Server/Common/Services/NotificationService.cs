using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services
{
    public class NotificationService : INotificationService
    {
        private List<INotificationHandler> Handlers = new List<INotificationHandler>();

        public void Trigger(string name, object data = null)
        {
            Task.Run(() => 
                Handlers.ForEach(h => {
                    try
                    {

                        h.Handle(name, data);
                    }
                    catch(Exception ex)
                    {

                    }
                })
            );
        }

        public void AddHandler(INotificationHandler handler)
        {
            if (handler == null) throw new ArgumentNullException();
            Handlers.Add(handler);
        }
    }
}
