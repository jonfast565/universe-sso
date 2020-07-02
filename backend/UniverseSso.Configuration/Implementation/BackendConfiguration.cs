using Microsoft.Extensions.Configuration;
using UniverseSso.Configuration.Interfaces;
using UniverseSso.Utilities;

namespace UniverseSso.Configuration.Implementation
{
    public class BackendConfiguration : IBackendConfiguration
    {
        private readonly IConfiguration _config;

        public string[] AuthenticationDlls { get; private set; }

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
        }
    }
}
