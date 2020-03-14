using Common.DataAccess;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    [Table("EmployeeActivity")]
    public class EmployeeActivity : IDEntityBase<string>
    {
        [Column]
        public string EmployeeId { get; set; }
        [Column]
        public ActivityType Type { get; set; }
        [Column]
        public DateTime Date { get; set; }
    }
}
