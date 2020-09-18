using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using saml_schema_assertion_2_0.saml;
using saml_schema_metadata_2_0.md;
using UniverseSso.Entities;

namespace UniverseSso.DataLoader
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // await LoadProvidersAndFields();
            await LoadSpMetadata();
        }

        private static async Task LoadSpMetadata()
        {
            var loginDatabase = new LoginDbContext();
            var client = new HttpClient();
            var response = await client.GetAsync(new Uri("https://sptest.iamshowcase.com/testsp_metadata.xml"));
            var xmlResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine(xmlResponse);
            var saml2Metadata = saml_schema_metadata_2_02.LoadFromString(xmlResponse);
            foreach (EntityDescriptorType entityDescriptor in saml2Metadata.EntityDescriptor)
            {
                foreach (SPSSODescriptorType spSsoDescriptor in entityDescriptor.SPSSODescriptor)
                {
                    var entityId = entityDescriptor.entityID.Value;
                    var validUntil = entityDescriptor.validUntil.Value;
                    var protocolAssertion = spSsoDescriptor.protocolSupportEnumeration.Value;
                    var wantAssertionsSigned = spSsoDescriptor.WantAssertionsSigned.Value;
                    var signAuthnRequests = spSsoDescriptor.AuthnRequestsSigned.Value;
                    var acsBinding = spSsoDescriptor.AssertionConsumerService[0].Binding.Value;
                    var acsLocation = spSsoDescriptor.AssertionConsumerService[0].Location.Value;
                    var nameIdFormats = spSsoDescriptor.NameIDFormat.First.Value;

                    var idpMetadata = new SpMetadata()
                    {
                        AcsBinding = acsBinding,
                        AcsLocation = acsLocation,
                        AuthnRequestsSigned = signAuthnRequests,
                        WantAssertionsSigned = wantAssertionsSigned,
                        EntityId = entityId,
                        NameIdFormats = nameIdFormats,
                        ProtocolSupportEnumeration = protocolAssertion,
                        ValidUntil = validUntil.GetDateTime(true)
                    };

                    await loginDatabase.SpMetadata.AddAsync(idpMetadata);

                    await loginDatabase.SaveChangesAsync();
                }
            }
            Console.WriteLine("Done!");
        }

        private static async Task LoadIdpMetadata()
        {
            var loginDatabase = new LoginDbContext();
            var xml = await File.ReadAllTextAsync("./idp_metadata.xml");
            Console.WriteLine(xml);
            var saml2Metadata = saml_schema_metadata_2_02.LoadFromString(xml);
            foreach (EntityDescriptorType entityDescriptor in saml2Metadata.EntityDescriptor)
            {
                foreach (IDPSSODescriptorType idpSsoDescriptor in entityDescriptor.IDPSSODescriptor)
                {
                    foreach (KeyDescriptorType keyDescriptor in idpSsoDescriptor.KeyDescriptor)
                    {
                        var descriptorType = keyDescriptor.use.Value;
                        keyDescriptor.KeyInfo.First.X509Data.First.X509Certificate.First.Value;
                    }
                }
            }
        }

private static async Task LoadProvidersAndFields()
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
                Pattern = "[0-9]+",
                Order = 1
            });

            await loginDatabase.Field.AddAsync(new Field
            {
                ProviderId = ncdrProvider.ProviderId,
                FieldName = "Username",
                FieldType = "Text",
                PageType = "Login",
                Pattern = null,
                Order = 2
            });

            await loginDatabase.Field.AddAsync(new Field
            {
                ProviderId = ncdrProvider.ProviderId,
                FieldName = "Password",
                FieldType = "Password",
                PageType = "Login",
                Pattern = null,
                Order = 3
            });

            await loginDatabase.Field.AddAsync(new Field
            {
                ProviderId = accProvider.ProviderId,
                FieldName = "Email",
                FieldType = "Text",
                PageType = "Login",
                Pattern = null,
                Order = 1
            });

            await loginDatabase.Field.AddAsync(new Field
            {
                ProviderId = accProvider.ProviderId,
                FieldName = "Password",
                FieldType = "Password",
                PageType = "Login",
                Pattern = null,
                Order = 2
            });


            await loginDatabase.SaveChangesAsync();
        }
    }
}
