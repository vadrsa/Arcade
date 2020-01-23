using Microsoft.AspNetCore.Identity;
using System;

namespace BusinessEntities
{
    public class Role : IdentityRole<Guid>
    {

        public Role() { }
        public Role(string name) : base(name) { }
    }
}
