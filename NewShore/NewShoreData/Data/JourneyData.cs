using NewShoreDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewShoreData.Data
{
    public class JourneyData
    {

        public int InsertJourneys(string origin , string destination , long price)
        {
            using (var context = new NewShoreBDContext())
            {
                Journey journey = new Journey();
                journey.Origin = origin;
                journey.Destination = destination;
                journey.Price = price;
                context.Journeys.Add(journey);
                context.SaveChanges();
                return journey.IdJourney;
            }
        }

    }
}
