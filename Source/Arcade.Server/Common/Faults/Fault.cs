using SharedEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Faults
{
    public class Fault
    {
        public int Code { get; set; }
        public int HttpStatusCode { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public FaultType Type => (FaultType)Code;
    }
}
