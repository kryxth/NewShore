using Microsoft.AspNetCore.Mvc;
using NewShoreBusiness;
using NewShoreDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewShoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Journey : ControllerBase
    {

        [HttpGet]
        public IEnumerable<ResponseDTO> Get(string origin , string destination)
        {
            //string origin = "PEI";
            //string destination = "CAN";
            
            JourneyBusiness journeyBusiness = new JourneyBusiness(origin, destination);
            List<FlightDTO> flights = journeyBusiness.GetFligths();
            ResponseDTO response = new ResponseDTO();
            response.Journey = new JourneyDTO();
            response.Journey.Origin = origin;
            response.Journey.Destination = destination;
            response.Journey.Flights = flights;
            return (IEnumerable<ResponseDTO>)(response);
        }
    }
}
