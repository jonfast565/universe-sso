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
        public EmailConfiguration Email { get; private set; }
        public FormsAuthConfiguration FormsAuth { get; private set; }

        public BackendConfiguration(IConfiguration config)
        {
            _config = config;
            ResetValues();
        }

        private void ResetValues()
        {
            FormsAuth = new FormsAuthConfiguration();
            Email = new EmailConfiguration();

            AuthenticationDlls = _config
                .GetSection("AuthenticationDlls")
                .Get<string[]>()
                .ThrowOnNullOrEmpty();

            Email.SmtpHost = _config
                .GetValue<string>("Email:SmtpHost");

            Email.SmtpPort = _config
                .GetValue<int>("Email:SmtpPort");

            Email.EnableSsl = _config
                .GetValue<bool>("Email:EnableSsl");

            Email.UseDefaultCredentials = _config
                .GetValue<bool>("Email:UseDefaultCredentials");

            Email.Username = _config
                .GetValue<string>("Email:Username");

            Email.Password = _config
                .GetValue<string>("Email:Password").ToSecureString();
        }
    }
}
