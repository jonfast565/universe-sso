using System;
using System.Collections.Generic;
using System.Text;

namespace UniverseSso.Models.Interfaces
{
    public interface IAuthenticationTokenBuilder
    {
        AuthenticationClaim Parse(string token);
        AuthenticationClaim BuildClaim(IDictionary<string, string> claims);
        string BuildString(IDictionary<string, string> claims);
    }
}
