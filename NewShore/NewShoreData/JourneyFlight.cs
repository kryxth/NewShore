using System;
using System.Collections.Generic;

#nullable disable

namespace NewShoreData
{
    public partial class JourneyFlight
    {
        public int Journey { get; set; }
        public int Flight { get; set; }

        public virtual Flight FlightNavigation { get; set; }
        public virtual Journey JourneyNavigation { get; set; }
    }
}
