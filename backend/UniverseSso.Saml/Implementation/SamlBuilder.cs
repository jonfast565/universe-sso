using saml_schema_protocol_2_0.samlp;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Encodings.Web;
using System.Xml;
using Altova.TypeInfo;
using saml_schema_metadata_2_0.md;
using saml_schema_protocol_2_0.saml;
using UniverseSso.Models.Implementation;
using UniverseSso.Saml.Implementation;
using UniverseSso.Utilities;
using System.Security.Cryptography.Xml;
using Org.BouncyCastle.Security;

namespace UniverseSso.Saml
{
    public static class SamlBuilder
    {
        private static string _langValue;

        public static SamlRequest GetSamlRequest(string samlRequest, string relayState)
        {
            var inflatedSaml = DecodeInflateSaml(samlRequest);
            var parsedSamlRequest = ParseSamlRequest(inflatedSaml);
            return parsedSamlRequest;
        }

        public static SamlResponse GetSamlResponse(SamlRequest samlRequest, Dictionary<string, string> attributes, string relayState, string provider, byte[] signingCertificate, byte[] signingPrivateKey)
        {
            var samlResponseDocument = BuildSamlResponse(samlRequest, attributes, provider, signingCertificate, signingPrivateKey);
            var encodedSaml = EncodeSaml(samlResponseDocument);
            return new SamlResponse
            {
                Request = samlRequest,
                PostTicket = new SamlPostTicket
                {
                    SamlParameterName = "SAMLResponse",
                    AcsUrl = samlRequest.AcsUrl,
                    EncodedSaml = encodedSaml,
                    RelayState = relayState
                }
            };
        }

        private static string BuildSamlResponse(SamlRequest r, Dictionary<string, string> attributes, string provider, byte[] signingCertificate, byte[] signingPrivateKey)
        {
            // TODO: Remove hardcoded window (clock skew)
            var now = DateTime.Now.ToUniversalTime();
            var nowWindowBegin = DateTime.Now.AddMinutes(-5).ToUniversalTime();
            var nowWindowEnd = DateTime.Now.AddMinutes(5).ToUniversalTime();

            var samlResponseDocument = saml_schema_protocol_2_02.CreateDocument();
            var response = samlResponseDocument.Response.Append();
            response.ID.Value = ExtensionMethods.RandomHash();
            response.Version.Value = "2.0";
            response.IssueInstant.Value = new Altova.Types.DateTime(now);

            // TODO: Remove hardcoded url
            var issuer = response.Issuer.Append();
            issuer.Value = provider;

            var status = response.Status.Append();
            var statusCode = status.StatusCode.Append();
            statusCode.Value2.Value = "urn:oasis:names:tc:SAML:2.0:status:Success";

            var assertion = response.Assertion.Append();
            assertion.ID.Value = ExtensionMethods.RandomHash();
            assertion.Version.Value = "2.0";
            assertion.IssueInstant.Value = new Altova.Types.DateTime(now);

            // TODO: Remove hardcoded url
            var assertionIssuer = assertion.Issuer.Append();
            assertionIssuer.Value = provider;

            var subject = assertion.Subject.Append();
            var subjectNameId = subject.NameID.Append();
            subjectNameId.Format.Value = "urn:oasis:names:tc:SAML:2.0:nameid-format:transient";
            subjectNameId.Value = Guid.NewGuid().ToString();

            // TODO: Ensure subject confirmation is correct
            var subjectConfirmation = subject.SubjectConfirmation.Append();
            subjectConfirmation.Method.Value = "urn:oasis:names:tc:SAML:2.0:cm:bearer";

            var subjectConfirmationData = subjectConfirmation.SubjectConfirmationData.Append();
            subjectConfirmationData.InResponseTo.Value = r.ID;
            subjectConfirmationData.Recipient.Value = r.AcsUrl;
            subjectConfirmationData.NotBefore.Value = new Altova.Types.DateTime(nowWindowBegin);
            subjectConfirmationData.NotOnOrAfter.Value = new Altova.Types.DateTime(nowWindowEnd);

            var conditions = assertion.Conditions.Append();
            conditions.NotBefore.Value = new Altova.Types.DateTime(nowWindowBegin);
            conditions.NotOnOrAfter.Value = new Altova.Types.DateTime(nowWindowEnd);

            // TODO: Removed audience restrictions b/c Shibboleth doesn't respect it on samltest.id 
            // figure out why
            // conditions.AudienceRestriction.Append().Audience.Append().Value = r.AcsUrl;

            // TODO: Add Authz statement

            // TODO: Add session id
            var authnStatement = assertion.AuthnStatement.Append();
            authnStatement.AuthnInstant.Value = new Altova.Types.DateTime(now);
            authnStatement.SessionIndex.Value = "Session ID";
            var attributeStatement = assertion.AttributeStatement.Append();

            // TODO: Determine whether this is necessary, and if PasswordProtectedTransport is the correct value
            var authnContext = authnStatement.AuthnContext.Append();
            var authnContextClass = authnContext.AuthnContextClassRef.Append();
            authnContextClass.Value = "urn:oasis:names:tc:SAML:2.0:ac:classes:PasswordProtectedTransport";

            AppendSamlAttributesToAssertion(attributes, attributeStatement);

            var document = samlResponseDocument.SaveToString(false, true);
            document = SignSaml(document, signingCertificate, signingPrivateKey);
            return document;
        }

        private static void AppendSamlAttributesToAssertion(Dictionary<string, string> attributes, AttributeStatementType attributeStatement)
        {
            void AddAttribute(string key, string value)
            {
                var attribute = attributeStatement.Attribute.Append();
                attribute.NameFormat.Value = "urn:oasis:names:tc:SAML:2.0:attrname-format:basic";
                attribute.Name.Value = key;
                attribute.AttributeValue.Append().Value = value;
            }

            foreach (var (key, value) in attributes)
            {
                AddAttribute(key, value);
            }
        }

        private static SamlRequest ParseSamlRequest(string inflatedSaml)
        {
            var xml = saml_schema_protocol_2_02.LoadFromString(inflatedSaml);
            var authnRequest = xml.AuthnRequest.First;
            // var forceAuthn = authnRequest.ForceAuthn?.Value;
            var acsUrl = authnRequest.AssertionConsumerServiceURL.Value;
            var destinationUrl = authnRequest.Destination.Value;
            var protocolBinding = authnRequest.ProtocolBinding.Value;
            var id = authnRequest.ID.Value;
            var samlVersion = authnRequest.Version.Value;
            var issueInstant = authnRequest.IssueInstant.Value.GetDateTime(true);

            var result = new SamlRequest
            {
                ForceAuthn = /*forceAuthn ?? */ false,
                AcsUrl = acsUrl,
                DestinationUrl = destinationUrl,
                ProtocolBinding = protocolBinding,
                ID = id,
                SamlVersion = samlVersion,
                IssueInstant = issueInstant
            };

            return result;
        }

        private static string DecodeInflateSaml(string samlRequest)
        {
            var decodedSaml = Utilities.ExtensionMethods.Base64Decode(samlRequest);
            var inflatedSaml = Utilities.ExtensionMethods.UnZipBytes(decodedSaml);
            return inflatedSaml;
        }

        private static string EncodeSaml(string xmlString)
        {
            var encodedSaml = Convert.ToBase64String(Encoding.Default.GetBytes(xmlString));
            return encodedSaml;
        }

        private static string SignSaml(string xmlString, byte[] signingCertificate, byte[] signingPrivateKey)
        {
            var pfx = CertUtilities.CreatePfxFromBinary(signingCertificate, signingPrivateKey);
            var x509 = new X509Certificate2(pfx.PfxFile, pfx.Password.ToSecureString());
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlString);
            var signedXml = SignXml(xmlDocument, x509);
            var stringWriter = new StringWriter();
            var xmlTextWriter = new XmlTextWriter(stringWriter);
            signedXml.WriteTo(xmlTextWriter);
            return stringWriter.ToString();
        }

        public static string GetSamlMetadata(SamlMetadata metadata)
        {
            var samlMetadata = saml_schema_metadata_2_02.CreateDocument();
            var entityDescriptor = samlMetadata.EntityDescriptor.Append();

            entityDescriptor.validUntil.Value = new Altova.Types.DateTime(metadata.ValidUntil);
            entityDescriptor.entityID.Value = metadata.EntityId;

            var idpSsoDescriptor = entityDescriptor.IDPSSODescriptor.Append();
            idpSsoDescriptor.WantAuthnRequestsSigned.Value = metadata.WantAuthnRequestsSigned;
            idpSsoDescriptor.protocolSupportEnumeration.Value = "urn:oasis:names:tc:SAML:2.0:protocol";

            var signingKey = idpSsoDescriptor.KeyDescriptor.Append();
            signingKey.use.Value = "signing";
            signingKey.KeyInfo.Append().X509Data.Append().X509Certificate.Append().Value = metadata.SigningKey;

            var encryptionKey = idpSsoDescriptor.KeyDescriptor.Append();
            encryptionKey.use.Value = "encryption";
            encryptionKey.KeyInfo.Append().X509Data.Append().X509Certificate.Append().Value = metadata.EncryptionKey;

            idpSsoDescriptor.NameIDFormat.Append().Value =
                "urn:oasis:names:tc:SAML:1.1:nameid-format:unspecified";

            var idpSingleSignOnService = idpSsoDescriptor.SingleSignOnService.Append();
            idpSingleSignOnService.Binding.Value = metadata.SsoBinding;
            idpSingleSignOnService.Location.Value = metadata.SsoLocation;

            // TODO: Use SPSSO to bundle this
            // var acs = samlMetadata.AssertionConsumerService.Append();
            // acs.Binding.Value = metadata.AcsBinding;
            // acs.Location.Value = metadata.AcsLocation;

            var organization = entityDescriptor.Organization.Append();
            var organizationName = organization.OrganizationName.Append();
            _langValue = "en-US";
            organizationName.lang.Value = _langValue;
            organizationName.Value = metadata.OrganizationName;

            var organizationDisplayName = organization.OrganizationDisplayName.Append();
            organizationDisplayName.Value = metadata.OrganizationDisplayName;
            organizationDisplayName.lang.Value = _langValue;

            var organizationUrl = organization.OrganizationURL.Append();
            organizationUrl.lang.Value = _langValue;
            organizationUrl.Value = metadata.OrganizationUrl;

            var contactPersonTechnical = entityDescriptor.ContactPerson.Append();
            contactPersonTechnical.contactType2.Value = "technical";
            contactPersonTechnical.GivenName.Append().Value = metadata.TechnicalContactName;
            contactPersonTechnical.EmailAddress.Append().Value = metadata.TechnicalContactEmail;

            var contactPersonSupport = entityDescriptor.ContactPerson.Append();
            contactPersonSupport.contactType2.Value = "support";
            contactPersonSupport.GivenName.Append().Value = metadata.SupportContactName;
            contactPersonSupport.EmailAddress.Append().Value = metadata.SupportContactEmail;

            var metadataString = samlMetadata.SaveToString(false, true);
            return metadataString;
        }

        private static XmlDocument SignXml([NotNull]XmlDocument doc, X509Certificate2 cert)
        {
            var signedXml = new SignedXml(doc) {SigningKey = cert.PrivateKey};
            var reference = new Reference {Uri = ""};
            var env = new XmlDsigEnvelopedSignatureTransform();
            var c14N = new XmlDsigExcC14NTransform();

            reference.AddTransform(env);
            reference.AddTransform(c14N);

            signedXml.AddReference(reference);
            signedXml.ComputeSignature();

            var signature = signedXml.GetXml();
            doc.DocumentElement?.AppendChild(signature);
            return doc;
        }
    }
}
