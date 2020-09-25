using System;
using System.Collections.Generic;
using System.Text;

namespace UniverseSso.Saml.Implementation
{
    public class SamlPostTicket
    {
        public string AcsUrl { get; set; }
        public string SamlParameterName { get; set; }
        public string EncodedSaml { get; set; }
        public string RelayState { get; set; }
    }
}
