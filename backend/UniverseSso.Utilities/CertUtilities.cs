using Org.BouncyCastle.Pkcs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace UniverseSso.Utilities
{
    public static class CertUtilities
    {
        public static byte[] CreatePfxFromBinary(byte[] certificate, byte[] privateKey)
        {
            var parser = new X509CertificateParser();
            var x509 = parser.ReadCertificate(certificate);
            var pRead = (AsymmetricKeyParameter) new PemReader(new StreamReader(new MemoryStream(privateKey))).ReadObject();
            var certSerializer = CreatePfxFile(x509, pRead);
            return certSerializer;
        }
        private static byte[] CreatePfxFile(X509Certificate certificate, AsymmetricKeyParameter privateKey)
        {
            // create certificate entry
            var certEntry = new X509CertificateEntry(certificate);
            var friendlyName = certificate.SubjectDN.ToString();

            // get bytes of private key.
            var keyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(privateKey);
            // var keyBytes = keyInfo.ToAsn1Object().GetEncoded();

            var builder = new Pkcs12StoreBuilder();
            builder.SetUseDerEncoding(true);
            var store = builder.Build();

            // create store entry
            store.SetKeyEntry("PK", new AsymmetricKeyEntry(privateKey), new[] { certEntry });

            byte[] pfxBytes = null;

            var password = Guid.NewGuid().ToString("N");

            using (var stream = new MemoryStream())
            {
                store.Save(stream, password.ToCharArray(), new SecureRandom());
                pfxBytes = stream.ToArray();
            }

            var result = Pkcs12Utilities.ConvertToDefiniteLength(pfxBytes);
            return result;
        }
    }
}
