using System;
using System.Collections.Generic;
using System.Text;
using UniverseSso.Models.Implementation;

namespace UniverseSso.Models.Interfaces
{
    public interface IAuthenticationTokenBuilder
    {
        AuthenticationClaim Parse(string token);
        string BuildString(AuthenticationClaim claim);
    }
}
