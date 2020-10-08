using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UniverseSso.Entities;
using UniverseSso.Utilities;

namespace CertSerializationTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Cert Tester");
            var loginContext = new LoginDbContext();
            var idpMetadata = loginContext.IdpMetadata.First(x => x.EntityId == "SAMLTestEpic");
            var cert = idpMetadata.SigningCertificate;
            var privateKey = idpMetadata.SigningPrivateKey;
            var result = CertUtilities.CreatePfxFromBinary(cert, privateKey);
            Console.WriteLine(result);
        }
    }
}
