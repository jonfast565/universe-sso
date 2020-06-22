using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace UniverseSso.Backend.Models
{
    public enum LoginFieldType
    {
        Username, 
        Password,
        Dropdown,
        Text,
        None = 0
    }
}
