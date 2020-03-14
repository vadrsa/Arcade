using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedEntities
{
    public class EmployeeReportDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsTerminated { get; set; }
        public List<ActivityDto> Activities { get; set; }
        public double AmountWorked { get; set; }
        public DateTime Date{ get; set; }
    }
}
