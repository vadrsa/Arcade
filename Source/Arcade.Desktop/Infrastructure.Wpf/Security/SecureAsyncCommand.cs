using Infrastructure.Mvvm;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    /// <summary>
    /// Async command with secure implementation
    /// </summary>
    public class SecureAsyncCommand : AsyncCommand, IDisposable
    {
        public SecureAsyncCommand(Func<Task> executeMethod) : base(executeMethod, SecureCanExecute)
        {
            AppSecurityContext.AppPrincipalChanged += HandleAppPrincipalChanged;
        }

        public SecureAsyncCommand(Func<Task> executeMethod, Func<bool> canExecuteMethod) : base(executeMethod, () => canExecuteMethod() && SecureCanExecute())
        {
            AppSecurityContext.AppPrincipalChanged += HandleAppPrincipalChanged;
        }


        private void HandleAppPrincipalChanged(object sender, EventArgs e)
        {
            this.RaiseCanExecuteChanged();
        }

        private static bool SecureCanExecute()
        {
            return AppSecurityContext.CurrentPrincipal.Identity.IsAuthenticated;
        }

        public void Dispose()
        {
            AppSecurityContext.AppPrincipalChanged -= HandleAppPrincipalChanged;
        }
    }

    /// <summary>
    /// Async command with secure implementation
    /// </summary>
    public class SecureAsyncCommand<T> : AsyncCommand<T>, IDisposable
    {
        public SecureAsyncCommand(Func<T, Task> executeMethod) : base(executeMethod, SecureCanExecute)
        {
            AppSecurityContext.AppPrincipalChanged += HandleAppPrincipalChanged;
        }

        public SecureAsyncCommand(Func<T, Task> executeMethod, Func<T, bool> canExecuteMethod) : base(executeMethod, (e) => canExecuteMethod(e) && SecureCanExecute(e))
        {
            AppSecurityContext.AppPrincipalChanged += HandleAppPrincipalChanged;
        }


        private void HandleAppPrincipalChanged(object sender, EventArgs e)
        {
            this.RaiseCanExecuteChanged();
        }

        private static bool SecureCanExecute(T obj)
        {
            return AppSecurityContext.CurrentPrincipal.Identity.IsAuthenticated;
        }

        public void Dispose()
        {
            AppSecurityContext.AppPrincipalChanged -= HandleAppPrincipalChanged;
        }
    }
}
