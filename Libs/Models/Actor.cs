using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Libs.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Avarta { get; set; }
        public string Description { get; set; }
        public ICollection<MovieActor>? MovieActors { get; set; }

    }
}
