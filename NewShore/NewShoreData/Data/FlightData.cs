using NewShoreDTO;
using System.Collections.Generic;
using System.Linq;

namespace NewShoreData.Data
{
    public class FlightData
    {
        public List<FlightDTO> GetFlights(string origin, string destination)
        {
            List<FlightDTO> result = new List<FlightDTO>();
            using (var context = new NewShoreBDContext())
            {
                Journey journey = context.Journeys.Where(x => x.Origin == origin && x.Destination == destination).FirstOrDefault();
                if (journey == null)
                    return null;

                List<JourneyFlight> journeyFlights = context.JourneyFlights.Where(x => x.Journey == journey.IdJourney).ToList();

                foreach (JourneyFlight journeyFlight in journeyFlights)
                {
                    result.Add(ConvertToflightDTO(journeyFlight.FlightNavigation));
                }
                return result;
            }
        }

        private FlightDTO ConvertToflightDTO(Flight flight)
        {
            return new FlightDTO()
            {
                Origin = flight.Origin,
                Destination = flight.Destination,
                Price = flight.Price,
                Transport = new TransportDTO()
                {
                    FlightCarrier = flight.TransportNavigation.FlightCarrier,
                    FlightNumber = flight.TransportNavigation.FlightNumber
                }
            };
        }

        public int CreateFlight(FlightDTO flight , int idTransport)
        {
            using (var context = new NewShoreBDContext())
            {
                Flight existingFlight = context.Flights.Where(x => x.Origin == flight.Origin && x.Destination == flight.Destination && x.Transport == idTransport).FirstOrDefault();
                int idFlight = 0;
                if (existingFlight != null)
                {
                    Flight newFlight = new Flight();
                    newFlight.Origin = flight.Origin;
                    newFlight.Destination = flight.Destination;
                    newFlight.Price = flight.Price;
                    newFlight.Transport = idTransport;
                    context.Flights.Add(newFlight);
                    idFlight = newFlight.IdFlight;
                }
                else
                    idFlight = existingFlight.IdFlight;
                return idFlight;
            }
        }

        public void CreateJourneyFlight(int idFlight, int idJourney)
        {
            using (var context = new NewShoreBDContext())
            {
                JourneyFlight journeyFlight = new JourneyFlight();
                journeyFlight.Flight = idFlight;
                journeyFlight.Journey = idJourney;
                context.JourneyFlights.Add(journeyFlight);
                context.SaveChanges();
            }
        }
    }
}
