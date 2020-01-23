using System.Collections.Generic;

namespace SharedEntities.Users
{

    public class LoginDto
    {
        public string Username { get; set; } = "";

        public string Password { get; set; } = "";

    }

    public class UserDto
    {
        public string UserName { get; set; }

        public List<string> Roles { get; set; }

        public string Token { get; set; }
    }

    public class RegisterDto
    {
        public string Username { get; set; } = "";

        public string Password { get; set; } = "";
    }
}
