using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Libs.Models
{
    public class MovieGenre
    {
        [Key]
        public int Id { get; set; }
        public int IdMovie { get; set; }
        public int IdGenre { get; set; }
        [JsonIgnore]
        public Movie Movie { get; set; }
        public Genre Genre { get; set; }
    }
}
