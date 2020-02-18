using Common.DataAccess;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    [Table("Sessions")]
    public class Session : IDEntityBase<string>
    {
        [Column]
        public string ComputerId { get; set; }
        [Column]
        public int Duration { get; set; }
        [Column, Nullable]
        public DateTime StartDate { get; set; }
        [Column, Nullable]
        public DateTime EndDate { get; set; }
        [Column, Nullable]
        public string PaymentId { get; set; }
        [Column, Nullable]
        public int QueueNumber { get; set; }
        [Column, Nullable]
        public DateTime QueueDate { get; set; }

        public SessionState State
        {
            get
            {
                if (EndDate != DateTime.MinValue)
                    return SessionState.Finished;
                if (StartDate != DateTime.MinValue && StartDate.AddMinutes(Duration) < DateTime.UtcNow)
                    return SessionState.Finished;
                if (StartDate == DateTime.MinValue && EndDate == DateTime.MinValue)
                    return SessionState.InQueue;
                return SessionState.Current;
            }
        }

        [Association(ThisKey = nameof(PaymentId), OtherKey = nameof(Id))]
        public Payment Payment { get; set; }

        [Association(ThisKey = nameof(ComputerId), OtherKey = nameof(Id))]
        public Computer Computer { get; set; }
    }
}
