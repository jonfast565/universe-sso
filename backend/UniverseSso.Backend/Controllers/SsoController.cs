using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UniverseSso.Backend.Controllers
{
    [Route("sso")]
    [ApiController]
    public class SsoController : ControllerBase
    {
        [HttpGet]
        public void SamlRedirect(string samlRequest)
        {
            var decodedSaml = Utilities.ExtensionMethods.Base64Decode(samlRequest);
            var inflatedSaml = Utilities.ExtensionMethods.UnZipBytes(decodedSaml);

            Console.WriteLine(inflatedSaml);
        }

        [HttpPost]
        public void SamlPost()
        {

        }
    }
}