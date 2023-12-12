using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Dtos
{
    public class ListUserPricingDTO
    {
        public string UserNameUser { get; set; }
        public string FullNameUser { get; set; }
        public string NamePricing { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan RemainingTime { get; set; }
    }
}
