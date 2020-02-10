using System;

namespace Infrastructure.Security
{
    public class AppIdentity
    {
        public AppIdentity(string name, string token)
        {
            Name = name;
            Token = token;
        }

        public string Name { get; private set; }

        public string AuthenticationType => "Admin";

        public bool IsAuthenticated => !String.IsNullOrEmpty(Name);

        public string Token { get; private set; }
    }
}
