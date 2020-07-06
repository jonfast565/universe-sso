using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace UniverseSso.Configuration.Implementation
{
    public class EmailConfiguration
    {
        public string SmtpHost { get; internal set; }
        public bool EnableSsl { get; internal set; }
        public int SmtpPort { get; internal set; }
        public bool UseDefaultCredentials { get; internal set; }
        public string Username { get; internal set; }
        public SecureString Password { get; internal set; }
    }
}
