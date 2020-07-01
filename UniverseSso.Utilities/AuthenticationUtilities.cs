using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UniverseSso.Models.Interfaces;

namespace UniverseSso.Utilities
{
    public static class AuthenticationUtilities
    {
        private static string[] DllPaths = {
            "./bin/Debug/netcoreapp3.1/UniverseSso.Authenticators.Ncdr.dll",
            "./bin/Debug/netcoreapp3.1/UniverseSso.Authenticators.Acc.dll",
            "./bin/Debug/netcoreapp3.1/UniverseSso.Authenticators.Test.dll",
        };

        private static string AuthenticationStrategyInterfaceName =
                "UniverseSso.Models.Interfaces.IAuthenticationStrategy";

        public static IAuthenticationStrategy LoadAuthenticationStrategyByProvider(string provider)
        {
            var fullDllPaths = DllPaths
                .Select(x => new FileInfo(x).FullName)
                .ToArray();

            var types = LoaderUtilities.LoadDlls(fullDllPaths);

            var strategies = types.Where(x => 
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
