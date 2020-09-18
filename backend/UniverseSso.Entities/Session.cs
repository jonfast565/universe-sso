using System;
using System.Collections.Generic;

namespace UniverseSso.Entities
{
    public partial class Session
    {
        public int SessionId { get; set; }
        public int ProviderId { get; set; }
        public string SessionToken { get; set; }
        public string SessionData { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDatetime { get; set; }
        public string UpdatedBy { get; set; }

        public virtual Provider Provider { get; set; }
    }
}
