using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Dtos
{
    public class PayPalSettings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public bool IsSandbox { get; set; }
    }

}
