using Common.DataAccess;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    [Table("Computers")]
    public class Computer : IDEntityBase<string>
    {
        [Column]
        public string TypeId { get; set; }

        [Column]
        public int Number { get; set; }

        [Column]
        public bool IsTerminated { get; set; }

        [Association(ThisKey = nameof(TypeId), OtherKey = nameof(Id))]
        public ComputerType Type { get; set; }

        [Association(ThisKey = nameof(Id), OtherKey = "ComputerId")]
        public IEnumerable<Session> Sessions { get; set; }
    }
}
