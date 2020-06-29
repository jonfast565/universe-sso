using System;
using System.Collections.Generic;

namespace UniverseSso.Entities
{
    public partial class AuthenticationStrategy
    {
        public int AuthenticationStrategyId { get; set; }
        public string StrategyName { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDatetime { get; set; }
        public string UpdatedBy { get; set; }
    }
}
