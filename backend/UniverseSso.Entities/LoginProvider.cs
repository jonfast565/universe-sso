﻿using System;
using System.Collections.Generic;

namespace UniverseSso.Entities
{
    public partial class LoginProvider
    {
        public LoginProvider()
        {
            LoginField = new HashSet<LoginField>();
        }

        public int LoginProviderId { get; set; }
        public string ProviderName { get; set; }
        public byte[] ProviderLogo { get; set; }
        public byte[] ProviderBackground { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDatetime { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<LoginField> LoginField { get; set; }
    }
}
