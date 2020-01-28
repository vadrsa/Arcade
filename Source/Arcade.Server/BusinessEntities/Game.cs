using System.ComponentModel.DataAnnotations;

namespace BusinessEntities
{
    public class Game
    {
        public string Id { get; set; }
        
        public string Name { get; set; }

        public Image Image { get; set; }

        public string Category { get; set; }

        public int AgeLimit { get; set; }
    }
}
