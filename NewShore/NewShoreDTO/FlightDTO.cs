using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewShoreDTO
{
    public class FlightDTO
    {
        public int IdFlight { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public long Price { get; set; }
        public Transport Transport { get; set; }
    }
}
