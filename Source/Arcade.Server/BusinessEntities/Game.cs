using Common.DataAccess;
using LinqToDB.Mapping;

namespace BusinessEntities
{

    [Table("Games")]
    public class Game : IDEntityBase<string>
    {
        [Column]
        public string Name { get; set; }

        [Column]
        public string ImageId { get; set; }

        [Column]
        public string Category { get; set; }

        [Column]
        public int AgeLimit { get; set; }

        [Association(ThisKey = nameof(ImageId), OtherKey = nameof(Id))]
        public Image Image { get; set; }
    }
}
