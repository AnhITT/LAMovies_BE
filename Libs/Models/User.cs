using Microsoft.AspNetCore.Identity;

namespace Libs.Models
{
    public class User : IdentityUser
    {
        public string? FullName { set; get; }
        public DateTime DateBirthday { get; set; }
        public bool Status { get; set; }

        public ICollection<UserPricing>? UserPricing { get; set; }
        public ICollection<MovieHistory>? MovieHistory { get; set; }


    }
}
