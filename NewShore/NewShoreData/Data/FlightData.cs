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
    }
}
