using Common.DataAccess;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    [Table("QueueNumberStorage")]
    public class QueueNumberStorage: IDEntityBase<int>
    {
        [Column]
        public int LatestNumber { get; set; }
    }
}
