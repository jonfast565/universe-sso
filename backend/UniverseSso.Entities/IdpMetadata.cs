using System;
using System.Collections.Generic;

namespace UniverseSso.Entities
{
    public partial class IdpMetadata
    {
        public int IdpMetadataId { get; set; }
        public string EntityId { get; set; }
        public bool WantAuthnRequestsSigned { get; set; }
        public string ProtocolSupportEnumeration { get; set; }
        public byte[] SigningCertificate { get; set; }
        public byte[] EncryptionCertificate { get; set; }
        public string NameIdFormats { get; set; }
        public string SsoBinding { get; set; }
        public string SsoLocation { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDatetime { get; set; }
        public string UpdatedBy { get; set; }
    }
}
