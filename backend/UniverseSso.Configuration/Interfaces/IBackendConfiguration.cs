using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using UniverseSso.Configuration.Implementation;

namespace UniverseSso.Configuration.Interfaces
{
    public interface IBackendConfiguration
    {
        string[] AuthenticationDlls { get; }
        EmailConfiguration Email { get; }
        FormsAuthConfiguration FormsAuth { get; }
    }
}
