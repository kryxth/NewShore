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
        

        public void InsertJourneys(List<FlightDTO> flights , string origin , string destination)
        {
            using (var context = new NewShoreBDContext())
            {
                Journey journey = new Journey();
                journey.Origin = origin;
                journey.Destination = destination;
                journey.Price = flights.Sum(x => x.Price);
                context.Journeys.Add(journey);

                foreach (FlightDTO flightDTO in flights)
                {
                    Transport existingTransport = context.Transports.Where(x => x.FlightNumber == flightDTO.Transport.FlightNumber && x.FlightCarrier == flightDTO.Transport.FlightCarrier).FirstOrDefault();
                    int idTransport = 0;
                    if (existingTransport != null)
                    {
                        Transport newTransport = new Transport();
                        newTransport.FlightCarrier = flightDTO.Transport.FlightCarrier;
                        newTransport.FlightNumber = flightDTO.Transport.FlightNumber;
                        context.Transports.Add(newTransport);
                        idTransport = newTransport.IdTransport;
                    }
                    else
                        idTransport = existingTransport.IdTransport;

                    Flight existingFlight = context.Flights.Where(x => x.Number == flightDTO.Number).FirstOrDefault();
                    int idFlight = 0;
                    if (existingFlight != null)
                    {
                        Flight newFlight = new Flight();
                        newFlight.Number = flightDTO.Number;
                        newFlight.Origin = flightDTO.Origin;
                        newFlight.Destination = flightDTO.Destination;
                        newFlight.Price = flightDTO.Price;
                        context.Flights.Add(newFlight);
                        idFlight = newFlight.IdFlight;
                    }
                    else
                        idFlight = existingFlight.IdFlight;

                    JourneyFlight journeyFlight = new JourneyFlight();
                    journeyFlight.Flight = idFlight;
                    journeyFlight.Journey = journey.IdJourney;
                    context.JourneyFlights.Add(journeyFlight);
                }               

                context.Journeys.Add(journey);
                context.SaveChanges();
            }
        }
    }
}
