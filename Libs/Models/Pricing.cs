using System.ComponentModel.DataAnnotations;

namespace Libs.Models
{
    public class Pricing
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Time { get; set; }
     
        public ICollection<UserPricing>? UserPricing { get; set; }

    }
}

