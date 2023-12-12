using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Dtos
{
    public class SeriesMoviesDTO
    {
        public int ID { get; set; }
        public string Url { get; set; }
        public int Tap { get; set; }
        public int TotalTap { get; set; }
    }
}
