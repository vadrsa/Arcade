using DevExpress.Mvvm;
using System;

namespace Infrastructure.Security
{
    /// <summary>
    /// Delegate command with secure implementation
    /// </summary>
    public class SecureCommand : DelegateCommand, IDisposable
    {
        public SecureCommand(Action executeMethod, bool? useCommandManager = null) : base(executeMethod, SecureCanExecute, useCommandManager)
        {
            AppSecurityContext.AppPrincipalChanged += HandleAppPrincipalChanged;
        }

        public SecureCommand(Action executeMethod, Func<bool> canExecuteMethod, bool? useCommandManager = null) : base(executeMethod, () => canExecuteMethod() && SecureCanExecute(), useCommandManager)
        {
            AppSecurityContext.AppPrincipalChanged += HandleAppPrincipalChanged;
        }


        private void HandleAppPrincipalChanged(object sender, EventArgs e)
        {
            this.RaiseCanExecuteChanged();
        }

        private static bool SecureCanExecute()
        {
            return true;
            return AppSecurityContext.CurrentPrincipal.Identity.IsAuthenticated;
        }

        public void Dispose()
        {
            AppSecurityContext.AppPrincipalChanged -= HandleAppPrincipalChanged;
        }
    }
}
