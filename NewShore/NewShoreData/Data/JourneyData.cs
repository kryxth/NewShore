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
        public List<Journey> GetJourney(string origin, string destination)
        {
            List<Journey> result = new List<Journey>();
            using (var context = new NewShoreBDContext())
            {
                Flight flight = context.Flights.Where(x => x.Origin == origin && x.Destination == destination).FirstOrDefault();
                if (flight == null)
                    return null;

                List<JourneyFlight> journeyFlights = context.JourneyFlights.Where(x => x.Flight == flight.IdFlight ).ToList();

                foreach(JourneyFlight journeyFlight in journeyFlights)
                {
                    result.Add(journeyFlight.JourneyNavigation);
                }
                return result;
            }
        }

        public void InsertJourneys(List<FlightDTO> flights , string origin , string destination)
        {
            using (var context = new NewShoreBDContext())
            {
                Journey journey = new Journey();
                journey.Origin = origin;
                journey.Destination = destination;
                journey.Price = flights.Sum(x => x.Price);
                context.Journeys.Add(journey);

                foreach(FlightDTO flightDTO in flights)
                {
                    Flight existingFlight = context.Flights.Where(x => x.IdFlight == flightDTO.IdFlight).ToList();

                }

                List<JourneyFlight> journeyFlights = context.JourneyFlights.Where(x => x.Flight == flight.IdFlight).ToList();

                //Flight flight = new Flight();
                //journey.Origin = origin;
                //journey.Destination = destination;
                //journey.Price = flights.Sum(x => x.Price);
                //journey.
                //context.Flights.Add(journey);

                context.Journeys.Add(journey);
                context.SaveChanges();
            }
        }
    }
}
