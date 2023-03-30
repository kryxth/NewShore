using NewShoreData;
using NewShoreData.Data;
using NewShoreDTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace NewShoreBusiness
{
    public class JourneyBusiness
    {
        string Origin;
        string Destination;
        List<FlightApi> AllFlights;

        public JourneyBusiness(string origin, string destination)
        {
            Origin = origin;
            Destination = destination;
        }

        public List<FlightDTO> GetFligths()
        {
            //Consultar
            List<FlightDTO> flights = GetJourney();
            if (flights == null)
            {
                //Algoritmo
                flights = CalculateJourneys();

                //Guardar Resultado
                SaveJourneys(flights);
            }
            return flights;
        }

        private List<FlightDTO> GetJourney()
        {
            return new FlightData().GetFlights(Origin, Destination);
        }

        private void SaveJourneys(List<FlightDTO> flights)
        {
            int idJourney = new JourneyData().InsertJourneys(Origin, Destination, flights.Sum(x => x.Price));

            foreach (FlightDTO flightDTO in flights)
            {
                int idTransport = new TransportData().CreateTransport(flightDTO.Transport);
                FlightData flightData = new FlightData();
                int idFlight = flightData.CreateFlight(flightDTO , idTransport);
                flightData.CreateJourneyFlight(idFlight, idJourney);
            }
        }

        private List<FlightDTO> CalculateJourneys()
        {
            List<FlightDTO> flights = new List<FlightDTO>();

            AllFlights = GetFligthsFromAPI();

            List<FlightApi> directFlights = AllFlights.Where(x => x.departureStation == Origin && x.arrivalStation == Destination).ToList();
            if (directFlights.Count > 0)
                flights.Add(ConvertFlightAPIToFlightDTO(directFlights[0]));

            List<FlightApi> flightsFromOrigin = AllFlights.Where(x => x.departureStation == Origin).ToList();

            flights = GetFligthsFromOrigin(flightsFromOrigin);

            return flights;
        }

        private List<FlightDTO> GetFligthsFromOrigin(List<FlightApi> flightsFromOrigin)
        {
            List<FlightDTO> result = new List<FlightDTO>();

            foreach (FlightApi flightFromOrigin in flightsFromOrigin)
            {
                List<FlightApi> newFlightsFromOrigin = AllFlights.Where(x => x.departureStation == flightFromOrigin.arrivalStation
                && x.arrivalStation == Destination).ToList();
                if (newFlightsFromOrigin.Count > 0)
                {
                    result.Add(ConvertFlightAPIToFlightDTO(flightFromOrigin));
                    result.Add(ConvertFlightAPIToFlightDTO(newFlightsFromOrigin[0]));
                    return result;
                }
            }

            foreach (FlightApi flightFromOrigin in flightsFromOrigin)
            {
                List<FlightApi> newFlightsFromOrigin = AllFlights.Where(x => x.departureStation == flightFromOrigin.arrivalStation).ToList();
                List<FlightDTO> nextFlights = GetFligthsFromOrigin(newFlightsFromOrigin);
                if(nextFlights.Count > 0)
                {
                    result.Add(ConvertFlightAPIToFlightDTO(flightFromOrigin));
                    result.AddRange(nextFlights);
                    return result;
                }
            }

            return result;
        }

        private List<FlightApi> GetFligthsFromAPI()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://recruiting-api.newshore.es/api/flights/2");
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            // Get data response
            var response = client.GetAsync("").Result;
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;

                List<FlightApi> flights = JsonConvert.DeserializeObject<List<FlightApi>>(json);

                return flights;
            }
            else
                return null;
        }

        private FlightDTO ConvertFlightAPIToFlightDTO(FlightApi flight)
        {
            return new FlightDTO()
            {
                Destination = flight.arrivalStation,
                Origin = flight.departureStation,
                Price = flight.price,
                Transport = new TransportDTO()
                {
                    FlightCarrier = flight.flightCarrier,
                    FlightNumber = flight.flightNumber
                }
            };
        }

    }
}
