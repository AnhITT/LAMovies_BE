using System.ComponentModel.DataAnnotations;

namespace Libs.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<MovieGenre>? MovieGenre { get; set; }

    }
}
