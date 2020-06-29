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

            // loginDatabase.Provider.Clear();
            // loginDatabase.Field.Clear();

            var accProvider = new Provider
            {
                ProviderName = "ACC",
                ProviderLogo = await File.ReadAllBytesAsync("./Assets/logo-header-acc.png"),
                ProviderBackground = await File.ReadAllBytesAsync("./Assets/Conferences-Heart-House-3.png"),
                Enabled = true,
            };

            await loginDatabase.Provider.AddAsync(accProvider);

            var ncdrProvider = new Provider
            {
                ProviderName = "NCDR",
                ProviderLogo = await File.ReadAllBytesAsync("./Assets/logo-ncdr.png"),
                ProviderBackground = await File.ReadAllBytesAsync("./Assets/Conferences-Heart-House-3.png"),
                Enabled = true,
            };

            await loginDatabase.Provider.AddAsync(ncdrProvider);

            await loginDatabase.SaveChangesAsync();

            await loginDatabase.Field.AddAsync(new Field
            {
                ProviderId = ncdrProvider.ProviderId,
                FieldName = "Participant ID",
                FieldType = "Text",
                PageType = "Login",
                Pattern = "[0-9]+"
            });

            await loginDatabase.Field.AddAsync(new Field
            {
                ProviderId = ncdrProvider.ProviderId,
                FieldName = "Username",
                FieldType = "Text",
                PageType = "Login",
                Pattern = null
            });

            await loginDatabase.Field.AddAsync(new Field
            {
                ProviderId = ncdrProvider.ProviderId,
                FieldName = "Password",
                FieldType = "Password",
                PageType = "Login",
                Pattern = null
            });

            await loginDatabase.Field.AddAsync(new Field
            {
                ProviderId = accProvider.ProviderId,
                FieldName = "Email",
                FieldType = "Text",
                PageType = "Login",
                Pattern = null
            });

            await loginDatabase.Field.AddAsync(new Field
            {
                ProviderId = accProvider.ProviderId,
                FieldName = "Password",
                FieldType = "Password",
                PageType = "Login",
                Pattern = null
            });


            await loginDatabase.SaveChangesAsync();
        }
    }
}
