using System;
using System.Collections.Generic;

namespace UniverseSso.Entities
{
    public partial class SpMetadata
    {
        public int IdpMetadataId { get; set; }
        public string EntityId { get; set; }
        public DateTime ValidUntil { get; set; }
        public bool AuthnRequestsSigned { get; set; }
        public bool WantAssertionsSigned { get; set; }
        public string ProtocolSupportEnumeration { get; set; }
        public string NameIdFormats { get; set; }
        public string AcsBinding { get; set; }
        public string AcsLocation { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDatetime { get; set; }
        public string UpdatedBy { get; set; }
    }
}
