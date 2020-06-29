using System;
using System.Collections.Generic;
using System.Text;
using UniverseSso.Backend.Models;

namespace UniverseSso.Models
{
    public class LoginFieldModel
    {
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public string OptionalFieldValues { get; set; }
    }
}
