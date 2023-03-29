using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewShoreData.Data
{
    public class JourneyData
    {
        public List<Journey> GetJourney(string origin, string destination)
        {
            using (var context = new NewShoreBDContext())
            {
                return context.Journeys.Where(x => x.Origin == origin && x.Destination == destination).ToList();
            }
        }

        public void InsertJourney(Journey journey)
        {
            using (var context = new NewShoreBDContext())
            {
                context.Journeys.Add(journey);
                context.SaveChanges();
            }
        }
    }
}
