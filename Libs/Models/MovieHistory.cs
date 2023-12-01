using System.ComponentModel.DataAnnotations.Schema;

namespace Libs.Models
{
    public class MovieHistory
    {
        public int IdMovie { get; set; }
        public string IdUser { get; set; }
        public DateTime DateTimeWatch { get; set; }
        public int? Episodes { get; set; }
        public int? Minutes { get; set; }
        public bool? Status { get; set; }    
        public Movie Movie { get; set; }
        public User User { get; set; }

    }
}
