using System.Collections.Generic;
using UniverseSso.Models.Implementation;

namespace UniverseSso.Models.Interfaces
{
    public interface IAuthenticationStrategy
    {
        string ProviderName { get; set; }

        AuthenticationReasons Authenticate(IDictionary<string, object> loginFields);

        void RecoverPassword(IDictionary<string, object> recoveryFields);
    }
}
