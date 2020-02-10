using Common.DataAccess;
using LinqToDB.Mapping;
using System.ComponentModel.DataAnnotations;

namespace BusinessEntities
{

    [Table("Images")]
    public class Image : IDEntityBase<string>
    {
        [Column]
        public string Path { get; set; }
    }
}
