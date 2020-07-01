using System;
using System.Collections.Generic;
using System.Text;

namespace UniverseSso.Configuration.Interfaces
{
    public interface IBackendConfiguration
    {
        string[] AuthenticationDlls { get; }
    }
}
