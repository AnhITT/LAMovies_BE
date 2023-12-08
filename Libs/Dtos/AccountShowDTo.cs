using Libs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Dtos
{
    public class AccountShowDTo : User
    {
        public List<string> Role { get; set; }
    }
}
