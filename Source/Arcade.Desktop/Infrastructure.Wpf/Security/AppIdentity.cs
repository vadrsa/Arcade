using System;
using System.Collections.Generic;

namespace Infrastructure.Security
{
    public class AppIdentity
    {
        public AppIdentity(string name, string token, List<string> roles)
        {
            Name = name;
            Token = token;
            Roles = roles;
        }

        public string Name { get; private set; }

        public List<string> Roles { get; private set; }

        public string AuthenticationType => "Admin";

        public bool IsAuthenticated => !String.IsNullOrEmpty(Name);

        public string Token { get; private set; }
    }
}
