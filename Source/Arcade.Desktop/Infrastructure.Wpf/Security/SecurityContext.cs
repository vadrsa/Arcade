using System;

namespace Infrastructure.Security
{
    public static class AppSecurityContext
    {
        private static AppPrincipal currentPrincipal;

        public static event EventHandler AppPrincipalChanged;

        public static AppPrincipal CurrentPrincipal
        {
            get
            {
                return currentPrincipal ?? AppPrincipal.Anonymous;
            }
        }

        public static void SetCurrentPrincipal(AppPrincipal principal)
        {
            if (principal != currentPrincipal)
            {
                currentPrincipal = principal;
                AppPrincipalChanged?.Invoke(null, EventArgs.Empty);
            }
        }
    }
}
