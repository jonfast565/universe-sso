using System;
using System.Collections.Generic;

namespace UniverseSso.Entities
{
    public partial class Field
    {
        public int FieldId { get; set; }
        public int ProviderId { get; set; }
        public string PageType { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public bool Required { get; set; }
        public string Pattern { get; set; }
        public string OptionalFieldValues { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDatetime { get; set; }
        public string UpdatedBy { get; set; }

        public virtual Provider Provider { get; set; }
    }
}
