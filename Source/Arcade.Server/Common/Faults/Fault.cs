using LinqToDB.Mapping;
using SharedEntities;

namespace Common.Faults
{
    [Table("Faults")]
    public class Fault
    {
        [PrimaryKey]
        public int Code { get; set; }
        [Column]
        public int HttpStatusCode { get; set; }
        [Column]
        public string Description { get; set; }
        public FaultType Type => (FaultType)Code;
    }
}
