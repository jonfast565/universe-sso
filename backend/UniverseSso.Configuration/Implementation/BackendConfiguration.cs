using System.Security;
using Microsoft.Extensions.Configuration;
using UniverseSso.Configuration.Interfaces;
using UniverseSso.Utilities;

namespace UniverseSso.Configuration.Implementation
{
    public class BackendConfiguration : IBackendConfiguration
    {
        private readonly IConfiguration _config;

        public string[] AuthenticationDlls { get; private set; }
        public string EmailSmtpHost { get; private set; }
        public bool EmailEnableSsl { get; private set; }
        public int EmailSmtpPort { get; private set; }
        public bool EmailUseDefaultCredentials { get; private set; }
        public string EmailUsername { get; private set; }
        public SecureString EmailPassword { get; private set; }

        public BackendConfiguration(IConfiguration config)
        {
            _config = config;
            ResetValues();
        }

        private void ResetValues()
        {
            AuthenticationDlls = _config
                .GetSection("AuthenticationDlls")
                .Get<string[]>()
                .ThrowOnNullOrEmpty();

            EmailSmtpHost = _config
                .GetValue<string>("Email:SmtpHost");

            EmailSmtpPort = _config
                .GetValue<int>("Email:SmtpPort");

            EmailEnableSsl = _config
                .GetValue<bool>("Email:EnableSsl");

            EmailUseDefaultCredentials = _config
                .GetValue<bool>("Email:UseDefaultCredentials");

            EmailUsername = _config
                .GetValue<string>("Email:Username");

            EmailPassword = _config
                .GetValue<string>("Email:Password").ToSecureString();
        }
    }
}
