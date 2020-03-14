using Common.DataAccess;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;

namespace BusinessEntities
{
    [Table("Employees")]
    public class Employee : IDEntityBase<string>
    {
        [Column]
        public string FirstName { get; set; }
        [Column]
        public string LastName { get; set; }
        [Column]
        public bool IsTerminated { get; set; }

        [Association(ThisKey = nameof(Id), OtherKey = nameof(Id))]
        public User User { get; set; }


        [Association(ThisKey = nameof(Id), OtherKey = nameof(EmployeeActivity.EmployeeId))]
        public IEnumerable<EmployeeActivity> Activities { get; set; }
    }
}
