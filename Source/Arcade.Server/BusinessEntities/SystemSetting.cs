using Common.DataAccess;
using LinqToDB.Mapping;
using System.ComponentModel.DataAnnotations;

namespace BusinessEntities
{
    [Table("SystemSettings")]
    public class SystemSetting : IDEntityBase<int>
    {
        [Column, Required]
        public string Name { get; set; }
        [Column, Required]
        public string Value { get; set; }
        [Column, Required]
        public string Type { get; set; }
    }
}
