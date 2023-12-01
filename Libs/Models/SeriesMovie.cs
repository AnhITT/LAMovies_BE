using System.ComponentModel.DataAnnotations;

namespace Libs.Models
{
    public class SeriesMovie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int IdMovie { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public int Practice { get; set;}

        public Movie Movie { get; set; }
    }
}
