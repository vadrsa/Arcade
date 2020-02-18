using Common.DataAccess;
using LinqToDB.Mapping;
using System;

namespace BusinessEntities
{
    [Table("Payments")]
    public class Payment : IDEntityBase<string>
    {
        [Column]
        public float Amount { get; set; }
        [Column]
        public string EmployeeId { get; set; }
        [Column]
        public DateTime Date { get; set; }

        [Association(ThisKey = nameof(EmployeeId), OtherKey = nameof(Id))]
        public Employee Employee { get; set; }
    }
}
