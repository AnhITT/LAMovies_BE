using System.ComponentModel.DataAnnotations;

namespace Libs.Models
{
    public class OddMovie
    {
        [Key]
        public int Id { get; set; }
        public int IdMovie { get; set; }
        [Required]
        public string Url { get; set; }
        public Movie Movie { get; set; }
    }
}
