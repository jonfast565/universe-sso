using System;
using System.Collections.Generic;
using System.Text;
using saml_schema_metadata_2_0.md;

namespace UniverseSso.Saml.Implementation
{
    public class SamlMetadata
    {
        public byte[] SigningKey { get; set; }
        public byte[] EncryptionKey { get; set; }
        public string SsoBinding { get; set; }
        public string SsoLocation { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationDisplayName { get; set; }
        public string OrganizationUrl { get; set; }
        public string TechnicalContactName { get; set; }
        public string TechnicalContactEmail { get; set; }
        public string SupportContactName { get; set; }
        public string SupportContactEmail { get; set; }
        public string AcsBinding { get; set; }
        public string AcsLocation { get; set; }
        public DateTime ValidUntil { get; set; }
        public string EntityId { get; set; }
        public bool WantAuthnRequestsSigned { get; set; }
    }
}
