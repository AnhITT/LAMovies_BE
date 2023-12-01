namespace Libs.Models
{
    public class UserPricing
    {
        public string IdUser { get; set; }
        public int IdPricing { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double TotalAmount { get; set; }
        public Pricing Pricing { get; set; }
        public User User { get; set; }
    }
}
