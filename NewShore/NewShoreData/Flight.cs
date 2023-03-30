using System;
using System.Collections.Generic;

#nullable disable

namespace NewShoreData
{
    public partial class Flight
    {
        public int IdFlight { get; set; }
        public int Transport { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public long Price { get; set; }

        public virtual Transport TransportNavigation { get; set; }
    }
}
