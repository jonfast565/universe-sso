﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UniverseSso.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcsController : ControllerBase
    {
        [Route("accept")]
        public void AcceptSaml()
        {

        }
    }
}