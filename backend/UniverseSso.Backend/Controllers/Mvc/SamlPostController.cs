using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using saml_schema_protocol_2_0.samlp;
using UniverseSso.Models.Implementation;
using UniverseSso.Saml;
using UniverseSso.Saml.Implementation;

namespace UniverseSso.Backend.Controllers.Mvc
{
    [Route("sso")]
    public class SamlPostController : Controller
    {
        public ActionResult Index(string samlRequest, string relayState)
        {
            var samlRequestObj = SamlBuilder.FromRequestString(samlRequest, relayState);

            var samlResponse = SamlBuilder.FromRequestAttributes(
                samlRequestObj, 
                new Dictionary<string, string> {{ "Some attribute", "Some value" }}, 
                relayState);

            if (samlRequestObj.IsHttpPostProtocolBinding())
            {
                SetSamlPostTicketInViewBag(samlResponse.PostTicket);
                return View();
            } 
            
            if (samlRequestObj.IsHttpRedirectProtocolBinding())
            {
                throw new NotImplementedException("HTTP-REDIRECT not implemented");
            }
            
            throw new Exception($"Invalid SAML protocol binding: {samlRequestObj.ProtocolBinding}");
        }

        private void SetSamlPostTicketInViewBag(SamlPostTicket postTicket)
        {
            ViewBag.ACSUrl = postTicket.AcsUrl;
            ViewBag.SamlParameterName = postTicket.SamlParameterName;
            ViewBag.EncodedSaml = postTicket.EncodedSaml;
            ViewBag.RelayState = postTicket.RelayState;
        }
    }
}
