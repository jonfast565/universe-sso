using System;
using System.Collections.Generic;
using System.Text;

namespace UniverseSso.Models
{
    public interface ILoginForm
    {
        string ProviderName { get; }
        byte[] ProviderLogo { get; }
        byte[] ProviderBackground { get; }
        List<string> GetFields();

    }
}
