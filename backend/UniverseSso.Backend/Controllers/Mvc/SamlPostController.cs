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
            var samlRequest = SamlBuilder.FromRequestString(samlRequest, relayState);
            var samlResponse = SamlBuilder.FromRequestAttributes(samlRequest, new Dictionary<string, string> {{ "Some attribute", "Some value" }}, relayState);

            if (samlRequest.ProtocolBinding.Contains("HTTP-POST"))
            {
                SetSamlPostTicketInViewBag(samlRequest.PostTicket);
                return View();
            } 
            else if (samlRequest.ProtocolBinding.Contains("HTTP-REDIRECT"))
            {
                throw new NotImplementedException("HTTP-REDIRECT not implemented");
            }
            else
            {
                throw new Exception($"Invalid SAML protocol binding: {samlRequest.ProtocolBinding}");
            }
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
