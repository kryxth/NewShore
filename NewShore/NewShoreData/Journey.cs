using System;
using System.Collections.Generic;

#nullable disable

namespace NewShoreData
{
    public partial class Journey
    {
        public int IdJourney { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public long Price { get; set; }
    }
}
