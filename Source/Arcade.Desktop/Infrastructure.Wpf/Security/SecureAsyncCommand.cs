using Infrastructure.Mvvm;
using SharedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    /// <summary>
    /// Async command with secure implementation
    /// </summary>
    public class SecureAsyncCommand : AsyncCommand, IDisposable
    {
        private ApplicationRole _role = ApplicationRole.Unset;

        public SecureAsyncCommand(Func<Task> executeMethod) : base(executeMethod)
        {
            AppSecurityContext.AppPrincipalChanged += HandleAppPrincipalChanged;
        }

        public SecureAsyncCommand(Func<Task> executeMethod, ApplicationRole role) : this(executeMethod)
        {
            _role = role;
        }

        public SecureAsyncCommand(Func<Task> executeMethod, Func<bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        {
            AppSecurityContext.AppPrincipalChanged += HandleAppPrincipalChanged;
        }

        public SecureAsyncCommand(Func<Task> executeMethod, Func<bool> canExecuteMethod, ApplicationRole role) : this(executeMethod, canExecuteMethod)
        {
            _role = role;
        }


        private void HandleAppPrincipalChanged(object sender, EventArgs e)
        {
            this.RaiseCanExecuteChanged();
        }

        public override bool CanExecute()
        {
            return base.CanExecute() && SecureCanExecute();
        }

        private bool SecureCanExecute()
        {
            if (_role != ApplicationRole.Unset && AppSecurityContext.CurrentPrincipal.Identity.IsAuthenticated)
                return AppSecurityContext.CurrentPrincipal.Identity.Roles.Any(r => r == _role.ToString());

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
        private List<string> _roles = null;

        public SecureAsyncCommand(Func<T, Task> executeMethod) : base(executeMethod)
        {
            AppSecurityContext.AppPrincipalChanged += HandleAppPrincipalChanged;
        }

        public SecureAsyncCommand(Func<T, Task> executeMethod, string roles) : this(executeMethod)
        {
            _roles = roles.Trim().Split(',').ToList();
        }

        public SecureAsyncCommand(Func<T, Task> executeMethod, Func<T, bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        {
            AppSecurityContext.AppPrincipalChanged += HandleAppPrincipalChanged;
        }

        public SecureAsyncCommand(Func<T, Task> executeMethod, Func<T, bool> canExecuteMethod, string roles) : this(executeMethod, canExecuteMethod)
        {
            _roles = roles.Trim().Split(',').ToList();
        }

        private void HandleAppPrincipalChanged(object sender, EventArgs e)
        {
            this.RaiseCanExecuteChanged();
        }

        public override bool CanExecute(T obj)
        {
            return base.CanExecute(obj) && SecureCanExecute();
        }

        private bool SecureCanExecute()
        {
            if (_roles != null && AppSecurityContext.CurrentPrincipal.Identity.IsAuthenticated)
                return AppSecurityContext.CurrentPrincipal.Identity.Roles.Any(r => _roles.Contains(r));

            return AppSecurityContext.CurrentPrincipal.Identity.IsAuthenticated;
        }

        public void Dispose()
        {
            AppSecurityContext.AppPrincipalChanged -= HandleAppPrincipalChanged;
        }
    }
}
