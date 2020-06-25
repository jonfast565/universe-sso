using System;
using System.Collections.Generic;
using UniverseSso.Models;
using UniverseSso.Models.Interfaces;

namespace UniverseSso.TokenBuilder.LegacyFormsAuthentication
{
    public class LegacyFormsAuthTokenBuilder : IAuthenticationTokenBuilder
    {
        public AuthenticationClaim Parse(string token)
        {
            throw new NotImplementedException();
        }

        public AuthenticationClaim BuildClaim(IDictionary<string, string> claims)
        {
            throw new NotImplementedException();
        }

        public string BuildString(IDictionary<string, string> claims)
        {
            throw new NotImplementedException();
        }
    }
}
