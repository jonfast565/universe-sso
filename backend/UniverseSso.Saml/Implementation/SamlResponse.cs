using System;
using System.Collections.Generic;
using System.Text;
using UniverseSso.Saml.Implementation;

namespace UniverseSso.Models.Implementation
{
    public class SamlResponse
    {
        public SamlPostTicket PostTicket { get; set; }
        public SamlRequest Request { get; internal set; }
    }
}
