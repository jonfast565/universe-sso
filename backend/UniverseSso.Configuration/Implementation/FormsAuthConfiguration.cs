using System;
using System.Collections.Generic;
using System.Text;

namespace UniverseSso.Configuration.Implementation
{
    public class FormsAuthConfiguration
    {
        public string ValidationKey { get; internal set; }
        public string DecryptionKey { get; internal set; }
    }
}
