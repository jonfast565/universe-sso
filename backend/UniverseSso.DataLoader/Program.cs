using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using saml_schema_metadata_2_0.md;
using UniverseSso.Entities;
using DateTime = Altova.Types.DateTime;

namespace UniverseSso.DataLoader
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // await LoadProvidersAndFields();
            await LoadSpMetadata();
            await LoadIdpMetadata();
        }

        private static async Task LoadSpMetadata()
        {
            var loginDatabase = new LoginDbContext();
            var client = new HttpClient();

            loginDatabase.SpMetadata.Clear();
            await loginDatabase.SaveChangesAsync();

            var response = await client.GetAsync(new Uri("https://sptest.iamshowcase.com/testsp_metadata.xml"));
            var xmlResponse = await response.Content.ReadAsStringAsync();
            await LoadSpMetadataToDatabase(xmlResponse, loginDatabase);

            response = await client.GetAsync(new Uri("https://samltest.id/saml/sp"));
            xmlResponse = await response.Content.ReadAsStringAsync();
            await LoadSpMetadataToDatabase(xmlResponse, loginDatabase);

            Console.WriteLine("Done!");
        }

        private static async Task LoadSpMetadataToDatabase(string xmlResponse, LoginDbContext loginDatabase)
        {
            Console.WriteLine(xmlResponse);

            var saml2Metadata = saml_schema_metadata_2_02.LoadFromString(xmlResponse);

            foreach (EntityDescriptorType entityDescriptor in saml2Metadata.EntityDescriptor)
            {
                foreach (SPSSODescriptorType spSsoDescriptor in entityDescriptor.SPSSODescriptor)
                {
                    var entityId = entityDescriptor.entityID.Value;
                    Altova.Types.DateTime validUntil;
                    try
                    {
                        validUntil = entityDescriptor.validUntil.Value;
                    }
                    catch (Exception)
                    {
                        validUntil = new Altova.Types.DateTime(System.DateTime.Now.AddYears(1));
                    }

                    var protocolAssertion = spSsoDescriptor.protocolSupportEnumeration.Value;
                    bool wantAssertionsSigned;
                    bool signAuthnRequests;

                    try
                    {
                        wantAssertionsSigned = spSsoDescriptor.WantAssertionsSigned.Value;
                        signAuthnRequests = spSsoDescriptor.AuthnRequestsSigned.Value;
                    }
                    catch (Exception)
                    {
                        wantAssertionsSigned = false;
                        signAuthnRequests = false;
                    }

                    var acsBinding = spSsoDescriptor.AssertionConsumerService.First.Binding.Value;
                    var acsLocation = spSsoDescriptor.AssertionConsumerService.First.Location.Value;

                    string nameIdFormats;
                    try
                    {
                        nameIdFormats = spSsoDescriptor.NameIDFormat.First.Value;
                    }
                    catch (Exception)
                    {
                        nameIdFormats = "urn:oasis:names:tc:SAML:1.1:nameid-format:unspecified";
                    }

                    var spMetadata = new SpMetadata
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

                    await loginDatabase.SpMetadata.AddAsync(spMetadata);

                    await loginDatabase.SaveChangesAsync();
                }
            }
        }

        private static async Task LoadIdpMetadata()
        {
            var loginDatabase = new LoginDbContext();

            loginDatabase.IdpMetadata.Clear();
            await loginDatabase.SaveChangesAsync();

            var xml = await File.ReadAllTextAsync("./idp_metadata.xml");
            Console.WriteLine(xml);
            var saml2Metadata = saml_schema_metadata_2_02.LoadFromString(xml);
            foreach (EntityDescriptorType entityDescriptor in saml2Metadata.EntityDescriptor)
            {
                var entityId = entityDescriptor.entityID.Value;
                // var validUntil = entityDescriptor.validUntil.Value;

                foreach (IDPSSODescriptorType idpSsoDescriptor in entityDescriptor.IDPSSODescriptor)
                {
                    var protocolAssertion = idpSsoDescriptor.protocolSupportEnumeration.Value;
                    var wantAuthnRequestsSigned = idpSsoDescriptor.WantAuthnRequestsSigned.Value;
                    var ssoBinding = idpSsoDescriptor.SingleSignOnService[0].Binding.Value;
                    var ssoLocation = idpSsoDescriptor.SingleSignOnService[0].Location.Value;
                    var nameIdFormats = idpSsoDescriptor.NameIDFormat.First.Value;

                    var certs = new Dictionary<string, byte[]>();
                    foreach (KeyDescriptorType keyDescriptor in idpSsoDescriptor.KeyDescriptor)
                    {
                        var descriptorType = keyDescriptor.use.Value;
                        var certificate = keyDescriptor.KeyInfo.First.X509Data.First.X509Certificate.First.Value;
                        certs[descriptorType] = certificate;
                    }

                    var idpMetadata = new IdpMetadata
                    {
                        EntityId = entityId,
                        WantAuthnRequestsSigned = wantAuthnRequestsSigned,
                        ProtocolSupportEnumeration = protocolAssertion,
                        SigningCertificate = certs["signing"],
                        EncryptionCertificate = certs["encryption"],
                        NameIdFormats = nameIdFormats,
                        SsoBinding = ssoBinding,
                        SsoLocation = ssoLocation
                    };

                    await loginDatabase.IdpMetadata.AddAsync(idpMetadata);
                    await loginDatabase.SaveChangesAsync();
                }
            }
        }

        private static async Task LoadProvidersAndFields()
        {
            var loginDatabase = new LoginDbContext();

            loginDatabase.Provider.Clear();
            loginDatabase.Field.Clear();
            await loginDatabase.SaveChangesAsync();

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
