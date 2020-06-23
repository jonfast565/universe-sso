using System;
using System.Collections.Generic;

namespace UniverseSso.Entities
{
    public partial class LoginField
    {
        public int LoginFieldId { get; set; }
        public int LoginProviderId { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public string OptionalFieldValues { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDatetime { get; set; }
        public string UpdatedBy { get; set; }

        public virtual LoginProvider LoginProvider { get; set; }
    }
}
