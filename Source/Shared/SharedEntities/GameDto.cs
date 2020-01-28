using System.ComponentModel.DataAnnotations;

namespace SharedEntities
{

    public class GameDto
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Image { get; set; }
        [Required]
        public string Category { get; set; }
    }

    public class GameDetailsDto
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Image { get; set; }
        [Required]
        public string Category { get; set; }
        public int AgeLimit { get; set; }
    }

    public class GameUploadDto
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public byte[] Image { get; set; }
        [Required]
        public string Category { get; set; }
        public int AgeLimit { get; set; }
    }
}
