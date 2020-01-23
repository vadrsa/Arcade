using Kernel.Workitems;
using System;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Kernel.Utility
{
    public static class UIExtensions
    {
        public static void InvokeIfNeeded(this Dispatcher dispatcher, Action action)
        {
            if (dispatcher.CheckAccess())
            {
                action?.Invoke();
            }
            else
            {
                dispatcher.Invoke(action);
            }

        }


        public static Task InvokeAsyncIfNeeded(this Dispatcher dispatcher, Func<Task> action)
        {
            if (dispatcher.CheckAccess())
            {
                return action?.Invoke();
            }
            else
            {
                return dispatcher.Invoke(action);
            }

        }

        public static void InvokeIfNeeded<T>(this Dispatcher dispatcher, Action<T> action, T param)
        {
            if (dispatcher.CheckAccess())
            {
                action?.Invoke(param);
            }
            else
            {
                dispatcher.Invoke(action, param);
            }

        }

        public static bool TryClose(this IModalWindow window)
        {

            try
            {
                window.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
