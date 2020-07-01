using System;
using System.Collections.Generic;
using System.Text;
using UniverseSso.Models.Implementation;

namespace UniverseSso.Models.Interfaces
{
    public interface IAuthenticationStrategy
    {
        string ProviderName { get; set; }

        AuthenticationReasons Authenticate(IDictionary<string, object> loginFields);
    }
}
