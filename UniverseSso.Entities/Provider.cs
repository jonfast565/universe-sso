using System;
using System.Collections.Generic;

namespace UniverseSso.Entities
{
    public partial class Provider
    {
        public Provider()
        {
            Field = new HashSet<Field>();
        }

        public int ProviderId { get; set; }
        public string ProviderName { get; set; }
        public byte[] ProviderLogo { get; set; }
        public byte[] ProviderBackground { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDatetime { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<Field> Field { get; set; }
    }
}
