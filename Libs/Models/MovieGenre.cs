namespace Libs.Models
{
    public class MovieGenre
    {
        public int Id { get; set; }
        public int IdMovie { get; set; }
        public int IdGenre { get; set; }
        public Movie Movie { get; set; }
        public Genre Genre { get; set; }
    }
}
