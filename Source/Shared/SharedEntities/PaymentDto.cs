using System;

namespace SharedEntities
{
    public class PaymentDto
    {
        public string Id { get; set; }
        public float Amount { get; set; }
        public string EmployeeId { get; set; }
        public EmployeeDto Employee { get; set; }
        public DateTime Date { get; set; }
    }
}
