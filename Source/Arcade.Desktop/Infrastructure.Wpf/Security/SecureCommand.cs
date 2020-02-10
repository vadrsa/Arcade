using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Security
{
    /// <summary>
    /// Delegate command with secure implementation
    /// </summary>
    public class SecureCommand : DelegateCommand, IDisposable
    {
        private List<string> _roles = null;

        public SecureCommand(Action executeMethod) : base(executeMethod)
        {
            AppSecurityContext.AppPrincipalChanged += HandleAppPrincipalChanged;
        }

        public SecureCommand(Action executeMethod, string roles) : this(executeMethod)
        {
            _roles = roles.Trim().Split(',').ToList();
        }

        public SecureCommand(Action executeMethod, Func<bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        {
            AppSecurityContext.AppPrincipalChanged += HandleAppPrincipalChanged;
        }

        public SecureCommand(Action executeMethod, Func<bool> canExecuteMethod, string roles) : this(executeMethod, canExecuteMethod)
        {
            _roles = roles.Trim().Split(',').ToList();
        }


        private void HandleAppPrincipalChanged(object sender, EventArgs e)
        {
            this.RaiseCanExecuteChanged();
        }

        protected override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter) && SecureCanExecute();
        }

        private bool SecureCanExecute()
        {
            if(_roles != null && AppSecurityContext.CurrentPrincipal.Identity.IsAuthenticated)
                return AppSecurityContext.CurrentPrincipal.Identity.Roles.Any(r => _roles.Contains(r));

            return AppSecurityContext.CurrentPrincipal.Identity.IsAuthenticated;
        }

        public void Dispose()
        {
            AppSecurityContext.AppPrincipalChanged -= HandleAppPrincipalChanged;
        }
    }
}
