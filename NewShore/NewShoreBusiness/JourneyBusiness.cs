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

        public List<Journey> GetFligths()
        {
            //Consultar
            List<Journey> journeys = GetJourney();
            if (journeys.Count == 0)
            {
                //Algoritmo
                journeys = CalculateJourneys();

                //Guardar Resultado
                SaveJourneys(journeys);
            }
            else
            {

            }
            return journeys;
        }


        private List<Journey> GetJourney()
        {
            return new JourneyData().GetJourney(Origin, Destination);
        }

        private void SaveJourneys(List<Journey> journeys)
        {

        }

        private List<Journey> CalculateJourneys()
        {
            List<Journey> journeys = new List<Journey>();

            AllFlights = GetFligthsFromAPI();

            List<FlightApi> directFlights = AllFlights.Where(x => x.departureStation == Origin && x.arrivalStation == Destination).ToList();
            if (directFlights.Count > 0)
                journeys.Add(ConvertFlightToJourney(directFlights[0]));

            List<FlightApi> flightsFromOrigin = AllFlights.Where(x => x.departureStation == Origin).ToList();

            journeys = GetFligthsFromOrigin(flightsFromOrigin);

            return journeys;
        }

        private List<Journey> GetFligthsFromOrigin(List<FlightApi> flightsFromOrigin)
        {
            List<Journey> journeys = new List<Journey>();

            foreach (FlightApi flightFromOrigin in flightsFromOrigin)
            {
                List<FlightApi> newFlightsFromOrigin = AllFlights.Where(x => x.departureStation == flightFromOrigin.arrivalStation
                && x.arrivalStation == Destination).ToList();
                if (newFlightsFromOrigin.Count > 0)
                {
                    journeys.Add(ConvertFlightToJourney(flightFromOrigin));
                    journeys.Add(ConvertFlightToJourney(newFlightsFromOrigin[0]));
                    return journeys;
                }
            }

            foreach (FlightApi flightFromOrigin in flightsFromOrigin)
            {
                List<FlightApi> newFlightsFromOrigin = AllFlights.Where(x => x.departureStation == flightFromOrigin.arrivalStation).ToList();
                List<Journey> newJourneys = GetFligthsFromOrigin(newFlightsFromOrigin);
                if(newJourneys.Count > 0)
                {
                    journeys.Add(ConvertFlightToJourney(flightFromOrigin));
                    journeys.AddRange(newJourneys);
                    return journeys;
                }
            }

            return journeys;
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

        private Journey ConvertFlightToJourney(FlightApi flight)
        {
            return new Journey()
            {
                Destination = flight.arrivalStation,
                Origin = flight.departureStation,
                Price = flight.price
            };
        }

    }
}
