﻿using Microsoft.AspNetCore.Mvc;
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
        public IEnumerable<ResponseDTO> Get()
        {
            new JourneyBusiness().GetFligths("", "");
            return (IEnumerable<ResponseDTO>)(new ResponseDTO());
        }
    }
}
