﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UniverseSso.Models.Implementation
{
    public class SamlRequest
    {
        public bool ForceAuthn { get; set; }
        public string AcsUrl { get; set; }
        public string DestinationUrl { get; set; }
        public string ProtocolBinding { get; set; }
        public string ID { get; set; }
        public string SamlVersion { get; set; }
        public DateTime IssueInstant { get; set; }

        public bool IsHttpPostProtocolBinding()
        {
            // TODO: Fix this to include the entire name
            return ProtocolBinding.Contains("HTTP-POST");
        }

        public bool IsHttpRedirectProtocolBinding()
        {
            // TODO: Fix this to include the entire name
            return ProtocolBinding.Contains("HTTP-REDIRECT");
        }
    }
}
