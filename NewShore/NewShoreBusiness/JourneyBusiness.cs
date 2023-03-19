using NewShoreDTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace NewShoreBusiness
{
    public class JourneyBusiness
    {
        public void GetFligths(string origin, string destination)
        {
            GetFligthsFromAPI();
        }

        private void GetFligthsFromAPI()
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

                List<FlightApi> tmp = JsonConvert.DeserializeObject<List<FlightApi>>(json);


                //foreach (var d in dataObjects)
                //{
                //    Console.WriteLine("{0}", d.Name);
                //}
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode,
                              response.ReasonPhrase);
            }
        }
    }
}
