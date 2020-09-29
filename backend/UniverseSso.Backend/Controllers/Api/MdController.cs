using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniverseSso.Entities;
using UniverseSso.Saml;
using UniverseSso.Saml.Implementation;

namespace UniverseSso.Backend.Controllers.Api
{
    [Route("md")]
    [ApiController]
    public class MdController : ControllerBase
    {
        public LoginDbContext LoginDbContext { get; }

        public MdController(LoginDbContext loginDbContext)
        {
            LoginDbContext = loginDbContext;
        }

        [HttpGet]
        [Route("{provider}")]
        public string Get(string provider)
        {
            var loginData = LoginDbContext.IdpMetadata.First(x => x.EntityId == provider);
            var samlMetadata = new SamlMetadata
            {
                SigningKey = loginData.SigningCertificate,
                EncryptionKey = loginData.EncryptionCertificate,
                SsoBinding = loginData.SsoBinding,
                SsoLocation = loginData.SsoLocation,
                OrganizationName = "ACC",
                OrganizationDisplayName = "American College of Cardiology",
                OrganizationUrl = "https://www.acc.org",
                TechnicalContactName = "Jon Fast",
                TechnicalContactEmail = "jfast@acc.org",
                SupportContactName = "Jon Fast",
                SupportContactEmail = "jfast@acc.org",
                /*
                AcsBinding = null,
                AcsLocation = null,
                */
                ValidUntil = DateTime.Now.AddYears(1),
                EntityId = loginData.EntityId,
                WantAuthnRequestsSigned = loginData.WantAuthnRequestsSigned
            };

            var result = SamlBuilder.GetSamlMetadata(samlMetadata);
            return result;
        }
    }
}
