using SharedEntities.Users;
using System.Collections.Generic;

namespace SharedEntities
{
    public class EmployeeDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public ApplicationRole Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class EmployeeUploadDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public ApplicationRole Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }


    public class EmployeeAddResultDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public ApplicationRole Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
