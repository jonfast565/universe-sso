using System;
using System.Collections.Generic;
using System.Linq;
using AspNetCore.LegacyAuthCookieCompat;
using UniverseSso.Configuration.Interfaces;
using UniverseSso.Models;
using UniverseSso.Models.Implementation;
using UniverseSso.Models.Interfaces;

namespace UniverseSso.TokenBuilder.LegacyFormsAuthentication
{
    public class LegacyFormsAuthTokenBuilder : IAuthenticationTokenBuilder
    {
        private readonly LegacyFormsAuthenticationTicketEncryptor _encryptor;

        public LegacyFormsAuthTokenBuilder(IBackendConfiguration backendConfiguration)
        {
            _encryptor = new LegacyFormsAuthenticationTicketEncryptor(
                backendConfiguration.FormsAuth.DecryptionKey,
                backendConfiguration.FormsAuth.ValidationKey, 
                ShaVersion.Sha1, 
                CompatibilityMode.Framework45);
        }
        public AuthenticationClaim Parse(string token)
        {
            var formsAuthenticationTicket = _encryptor.DecryptCookie(token);

            var claim = new AuthenticationClaim
            {
                Claims = new Dictionary<string, object>()
            };

            claim.Claims.Add("Name", formsAuthenticationTicket.Name);
            claim.Claims.Add("CookiePath", formsAuthenticationTicket.CookiePath);
            claim.Claims.Add("UserData", formsAuthenticationTicket.UserData);
            claim.Claims.Add("Expiration", formsAuthenticationTicket.Expiration);
            claim.Claims.Add("Expired", formsAuthenticationTicket.Expired);
            claim.Claims.Add("IsPersistent", formsAuthenticationTicket.IsPersistent);
            claim.Claims.Add("IssueDate", formsAuthenticationTicket.IssueDate);
            claim.Claims.Add("Version", formsAuthenticationTicket.Version);

            return claim;
        }

        public string BuildString(AuthenticationClaim claim)
        {
            var name = claim.Claims["Name"];
            var cookiePath = claim.Claims["CookiePath"];
            var userData = claim.Claims["UserData"];
            var expiration = claim.Claims["Expiration"];
            // var expired = claim.Claims["Expired"];
            var isPersistent = claim.Claims["IsPersistent"];
            var issueDate = claim.Claims["IssueDate"];
            var version = claim.Claims["Version"];

            var formsTicket = new FormsAuthenticationTicket(
                (int)version, 
                (string)name, 
                (DateTime)issueDate, 
                (DateTime)expiration, 
                (bool)isPersistent,
                (string)userData, 
                (string)cookiePath);

            var encrypted = _encryptor.Encrypt(formsTicket);
            return encrypted;
        }
    }
}
