using System;
using System.IO;
using System.Threading.Tasks;
using UniverseSso.Entities;

namespace UniverseSso.DataLoader
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var loginDatabase = new LoginDbContext();

            loginDatabase.Provider.Clear();
            loginDatabase.Field.Clear();

            await loginDatabase.Provider.AddAsync(new Provider
            {
                ProviderName = "ACC",
                ProviderLogo = await File.ReadAllBytesAsync("./Assets/logo-header-acc.png"),
                ProviderBackground = await File.ReadAllBytesAsync("./Assets/Conferences-Heart-House-3.png"),
                Enabled = true,
            });

            await loginDatabase.Provider.AddAsync(new Provider
            {
                ProviderName = "NCDR",
                ProviderLogo = await File.ReadAllBytesAsync("./Assets/logo-ncdr.png"),
                ProviderBackground = await File.ReadAllBytesAsync("./Assets/Conferences-Heart-House-3.png"),
                Enabled = true,
            });

            await loginDatabase.SaveChangesAsync();
        }
    }
}
