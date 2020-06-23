using System;
using System.Collections.Generic;

namespace UniverseSso.Entities
{
    public partial class ProviderKey
    {
        public int ProviderKeyId { get; set; }
        public int ProviderId { get; set; }
        public string Key { get; set; }
        public int? IntValue { get; set; }
        public decimal? DecimalValue { get; set; }
        public string StringValue { get; set; }
        public byte[] BinaryValue { get; set; }

        public virtual Provider Provider { get; set; }
    }
}
