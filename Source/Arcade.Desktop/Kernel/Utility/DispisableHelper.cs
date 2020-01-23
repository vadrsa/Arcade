using System;

namespace Kernel.Utility
{
    public static class DispisableHelper
    {
        public static void DisposeEvent<TEventArgs>(EventHandler<TEventArgs> eventHandler)
        {
            if (eventHandler != null)
            {
                foreach (Delegate d in eventHandler.GetInvocationList())
                {
                    eventHandler -= (EventHandler<TEventArgs>)d;
                }
            }
        }

        public static void DisposeEvent(EventHandler<EventArgs> eventHandler)
        {
            DisposeEvent<EventArgs>(eventHandler);
        }
    }
}
