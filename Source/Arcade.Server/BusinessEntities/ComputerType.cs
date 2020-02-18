using Common.DataAccess;
using LinqToDB.Mapping;
using System.ComponentModel.DataAnnotations;

namespace BusinessEntities
{
    [Table("ComputerTypes")]
    public class ComputerType : IDEntityBase<string>
    {
        [Column, Required]
        public string Name { get; set; }
        [Column, Required]
        public float HourlyRate { get; set; }
    }
}
