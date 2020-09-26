using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Flurl;
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
        [HttpGet]
        [HttpPost]
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
            
            // ReSharper disable once InvertIf
            if (samlRequestObj.IsHttpRedirectProtocolBinding())
            {
                var url = SetSamlRedirectInUrl(samlResponse.PostTicket);
                Redirect(url);
            }
            
            throw new Exception($"Invalid SAML protocol binding: {samlRequestObj.ProtocolBinding}");
        }

        private string SetSamlRedirectInUrl(SamlPostTicket postTicket)
        {
            var url = postTicket.AcsUrl.SetQueryParams(new
            {
                SAMLResponse = postTicket.EncodedSaml,
                postTicket.RelayState
            });

            return url;
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
