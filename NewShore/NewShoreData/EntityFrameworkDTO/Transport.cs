using System;
using System.Collections.Generic;

#nullable disable

namespace NewShoreData
{
    public partial class Transport
    {
        public Transport()
        {
            Flights = new HashSet<Flight>();
        }

        public int IdTransport { get; set; }
        public string FlightCarrier { get; set; }
        public string FlightNumber { get; set; }

        public virtual ICollection<Flight> Flights { get; set; }
    }
}
