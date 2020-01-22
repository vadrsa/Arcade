using DataAccess;
using LinqToDB.Mapping;

namespace BusinessEntities
{
    [Table("Games")]
    public class Game : IDEntityBase<int>
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public int AgeLimit { get; set; }
    }
}
