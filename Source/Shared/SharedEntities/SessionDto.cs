using System;

namespace SharedEntities
{
    public class SessionDto
    {
        public string Id { get; set; }
        public string ComputerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public SessionState State { get; set; }
        public int Duration { get; set; }
        public string PaymentId { get; set; }
        public int QueueNumber { get; set; }
        public PaymentDto Payment { get; set; }
        public ComputerDto Computer { get; set; }
    }

    public class SessionUploadDto
    {
        public string ComputerId { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public float PaymentDue { get; set; }
        //public SessionState State { get; set; }
    }
}
