using System;
using System.Collections.Generic;
using System.Text;

namespace UniverseSso.Models.Implementation
{
    public class AuthenticationReasons
    {
        public bool Authenticated { get; set; }
        public string[] SuccessReasons { get; set; }
        public string[] FailureReasons { get; set; }
        public AuthenticationFlags Flags { get; set; }
    }
}
