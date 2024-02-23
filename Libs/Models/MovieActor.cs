using System.Text.Json.Serialization;

namespace Libs.Models
{
    public class MovieActor
    {
        public int IdMovie { get; set; }
        public int IdActor { get; set; }
        [JsonIgnore]
        public Movie Movie { get; set; }
        public Actor Actor { get; set; }
    }
}
