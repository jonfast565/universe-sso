using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UniverseSso.Models.Interfaces;

namespace UniverseSso.Utilities
{
    public class AuthenticationLoader
    {
        private string[] DllPaths { get; }

        private Type[] DllTypes { get; set; }

        private static string AuthenticationStrategyInterfaceName =
                "UniverseSso.Models.Interfaces.IAuthenticationStrategy";

        public AuthenticationLoader(string[] dllPaths)
        {
            DllPaths = dllPaths;
            LoadDlls();
        }

        private void LoadDlls()
        {
            var fullDllPaths = DllPaths
                .Select(x => new FileInfo(x).FullName)
                .ToArray();

            var types = LoaderUtilities.LoadDlls(fullDllPaths);

            DllTypes = types.ToArray();
        }

        public IAuthenticationStrategy GetStrategyByProvider(string provider)
        {
            var strategies = DllTypes.Where(x => 
                x.GetInterface(AuthenticationStrategyInterfaceName) != null)
                .ToList();

            foreach (var type in strategies)
            {
                var instance = Activator.CreateInstance(type) as IAuthenticationStrategy;

                if (instance == null)
                {
                    continue;
                }

                if (string.Equals(instance.ProviderName, provider, StringComparison.CurrentCultureIgnoreCase))
                {
                    return instance;
                }
            }

            throw new Exception($"No authentication strategy found for {provider}");
        }
    }
}
