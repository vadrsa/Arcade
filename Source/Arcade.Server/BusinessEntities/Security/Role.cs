using LinqToDB.Identity;
using System;

namespace BusinessEntities
{
    public class Role : IdentityRole
    {

        public Role() { }
        public Role(string name) : base(name) { }
    }
}
