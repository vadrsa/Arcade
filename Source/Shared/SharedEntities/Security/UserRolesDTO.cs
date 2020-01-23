using System.Collections.Generic;

namespace SharedEntities.Users
{
    public class UserRolesDTO
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }
}
