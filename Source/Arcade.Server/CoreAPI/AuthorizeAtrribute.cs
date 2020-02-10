using SharedEntities;

namespace CoreAPI
{
    public class AuthorizeAttribute : Microsoft.AspNetCore.Authorization.AuthorizeAttribute
    {
        public AuthorizeAttribute()
        {

        }

        public AuthorizeAttribute(ApplicationRole role)
        {
            Roles = role.ToString();
        }
    }
}
