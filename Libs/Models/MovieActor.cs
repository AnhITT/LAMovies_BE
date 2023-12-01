namespace Libs.Models
{
    public class MovieActor
    {
        public int IdMovie { get; set; }
        public int IdActor { get; set; }
        public Movie Movie { get; set; }
        public Actor Actor { get; set; }
    }
}
