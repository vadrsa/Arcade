using Common.DataAccess;
using LinqToDB.Mapping;
using System;

namespace BusinessEntities
{
    [Table("Employees")]
    public class Employee : IDEntityBase<string>
    {
        [Column]
        public string FirstName { get; set; }
        [Column]
        public string LastName { get; set; }

        [Association(ThisKey = nameof(Id), OtherKey = nameof(Id))]
        public User User { get; set; }
    }
}
