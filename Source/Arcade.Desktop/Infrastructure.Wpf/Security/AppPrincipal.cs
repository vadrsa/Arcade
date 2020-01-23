namespace Infrastructure.Security
{
    public class AppPrincipal
    {
        public static AppPrincipal Anonymous
        {
            get
            {
                return new AppPrincipal(new AnonymousIdentity());
            }
        }

        private AppIdentity _identity;

        public AppIdentity Identity
        {
            get { return _identity; }
            private set { _identity = value; }
        }
        public AppPrincipal(AppIdentity identity)
        {
            _identity = identity;
        }

    }
}
