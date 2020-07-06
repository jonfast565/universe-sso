using System.Collections.Generic;

namespace UniverseSso.Models.Interfaces
{
    public interface ILoginForm
    {
        string ProviderName { get; }
        byte[] ProviderLogo { get; }
        byte[] ProviderBackground { get; }
        List<string> GetFields();

    }
}
