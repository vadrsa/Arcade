using Prism.Commands;
using System;

namespace Infrastructure.Security
{
    /// <summary>
    /// Delegate command with secure implementation
    /// </summary>
    public class SecureCommand : DelegateCommand, IDisposable
    {
        public SecureCommand(Action executeMethod) : base(executeMethod, SecureCanExecute)
        {
            AppSecurityContext.AppPrincipalChanged += HandleAppPrincipalChanged;
        }

        public SecureCommand(Action executeMethod, Func<bool> canExecuteMethod) : base(executeMethod, () => canExecuteMethod() && SecureCanExecute())
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
}
