using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libs.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string UrlTrailer { get; set; }
        [Required]
        public string UrlImg { get; set; }
        [Required]
        public string UrlImgCover { get; set; }
        [Required]
        public string SubLanguage { get; set; }
        [Required]
        public int MinAge { get; set; }
        [Required]
        public string Quality { get; set; }
        [Required]
        public string Time { get; set; }
        [Required]
        public string YearCreate { get; set; }
        [Required]
        public string Type { get; set; }
        public int View { get; set; }

        [Required]
        public int Episodes { get; set; }

        public ICollection<MovieGenre>? MovieGenre { get; set; }
        [NotMapped]
        [Required]
        public List<int>? Genres { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem>? GenreList { get; set; }
        [NotMapped]
        public List<string>? GenreNames { get; set; }
        [NotMapped]
        public MultiSelectList? MultiGenreList { get; set; }

        public ICollection<MovieHistory>? MovieHistory { get; set; }

        public OddMovie? OddMovie { get; set; }
        public ICollection<SeriesMovie>? SeriesMovie { get; set; }
        public ICollection<MovieActor>? MovieActors { get; set; }
        [NotMapped]
        [Required]
        public List<int>? Actor { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem>? ActorList { get; set; }
        [NotMapped]
        public List<string>? ActorNames { get; set; }
        [NotMapped]
        public MultiSelectList? MultiActorList { get; set; }
    }
}
