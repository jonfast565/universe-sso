using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace UniverseSso.Configuration.Interfaces
{
    public interface IBackendConfiguration
    {
        string[] AuthenticationDlls { get; }
        string EmailSmtpHost { get; }
        bool EmailEnableSsl { get; }
        int EmailSmtpPort { get; }
        bool EmailUseDefaultCredentials { get; }
        string EmailUsername { get; }
        SecureString EmailPassword { get; }
    }
}
