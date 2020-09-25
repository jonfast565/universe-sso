using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using saml_schema_protocol_2_0.samlp;
using UniverseSso.Models.Implementation;

namespace UniverseSso.Backend.Controllers.Mvc
{
    [Route("sso")]
    public class SamlPostController : Controller
    {
        public ActionResult Index(string samlRequest, string relayState)
        {
            var inflatedSaml = DecodeInflateSaml(samlRequest);
            var parsedSamlRequest = ParseSamlRequest(inflatedSaml);
            
            var samlResponseDocument = BuildSamlResponse(parsedSamlRequest, new Dictionary<string, string>()
            {
                {"FirstName", "Jon"},
                {"LastName", "Fast"},
                {"SessionInfos", "Something"}
            });

            var encodedSaml = EncodeDeflateSaml(samlResponseDocument);
            SetSamlInViewBag(parsedSamlRequest, encodedSaml, relayState);
            
            return View();
        }

        private void SetSamlInViewBag(SamlRequest parsedSamlRequest, string encodedSaml, string relayState)
        {
            ViewBag.ACSUrl = parsedSamlRequest.AcsUrl;
            ViewBag.SamlParameterName = "SAMLResponse";
            ViewBag.EncodedSaml = encodedSaml;
            ViewBag.RelayState = relayState;
        }

        private static string BuildSamlResponse(SamlRequest r, Dictionary<string, string> attributes)
        {
            var samlResponseDocument = saml_schema_protocol_2_02.CreateDocument();
            var response = samlResponseDocument.Response.Append();

            // TODO: Remove hardcoded window (clock skew)
            var now = DateTime.Now;
            var nowWindowBegin = DateTime.Now.AddMinutes(-5);
            var nowWindowEnd = DateTime.Now.AddMinutes(5);

            // TODO: Remove hardcoded url
            var issuer = response.Issuer.Append();
            issuer.Value = "https://localhost:5000/md";

            var status = response.Status.Append();
            var statusCode = status.StatusCode.Append();
            statusCode.Value2.Value = "urn:oasis:names:tc:SAML:2.0:status:Success";

            var assertion = response.Assertion.Append();
            assertion.ID.Value = Utilities.ExtensionMethods.ComputeSha256Hash(DateTime.Now.ToString(CultureInfo.InvariantCulture));
            assertion.Version.Value = "2.0";
            assertion.IssueInstant.Value = new Altova.Types.DateTime(now);

            // TODO: Remove hardcoded url
            var assertionIssuer = assertion.Issuer.Append();
            assertionIssuer.Value = "https://localhost:5000/md";

            var subject = assertion.Subject.Append();
            var subjectNameId = subject.NameID.Append();
            subjectNameId.Format.Value = "urn:oasis:names:tc:SAML:2.0:nameid-format:transient";
            subjectNameId.Value = Guid.NewGuid().ToString();

            // TODO: Ensure subject confirmation is correct
            var subjectConfirmation = subject.SubjectConfirmation.Append();
            subjectConfirmation.Method.Value = "urn:oasis:names:tc:SAML:2.0:cm:bearer";

            var subjectConfirmationData = subjectConfirmation.SubjectConfirmationData.Append();
            subjectConfirmationData.InResponseTo.Value = r.ID;
            subjectConfirmationData.Recipient.Value = r.DestinationUrl;
            subjectConfirmationData.NotBefore.Value = new Altova.Types.DateTime(nowWindowBegin);
            subjectConfirmationData.NotOnOrAfter.Value = new Altova.Types.DateTime(nowWindowEnd);

            var conditions = assertion.Conditions.Append();
            conditions.NotBefore.Value = new Altova.Types.DateTime(nowWindowBegin);
            conditions.NotOnOrAfter.Value = new Altova.Types.DateTime(nowWindowEnd);
            conditions.AudienceRestriction.Append().Audience.Append().Value = r.AcsUrl;

            // TODO: Add Authz statement

            // TODO: Add session id
            var authnStatement = assertion.AuthnStatement.Append();
            authnStatement.AuthnInstant.Value = Altova.Types.DateTime.Now;
            authnStatement.SessionIndex.Value = "Session ID";
            var attributeStatement = assertion.AttributeStatement.Append();

            // TODO: Determine whether this is necessary, and if PasswordProtectedTransport is the correct value
            var authnContext = authnStatement.AuthnContext.Append();
            var authnContextClass = authnContext.AuthnContextClassRef.Append();
            authnContextClass.Value = "urn:oasis:names:tc:SAML:2.0:ac:classes:PasswordProtectedTransport";

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

            var document = samlResponseDocument.SaveToString(false, true);
            return document;
        }

        private static SamlRequest ParseSamlRequest(string inflatedSaml)
        {
            var xml = saml_schema_protocol_2_02.LoadFromString(inflatedSaml);
            var authnRequest = xml.AuthnRequest.First;
            var forceAuthn = authnRequest.ForceAuthn.Value;
            var acsUrl = authnRequest.AssertionConsumerServiceURL.Value;
            var destinationUrl = authnRequest.Destination.Value;
            var protocolBinding = authnRequest.ProtocolBinding.Value;
            var id = authnRequest.ID.Value;
            var samlVersion = authnRequest.Version.Value;
            var issueInstant = authnRequest.IssueInstant.Value.GetDateTime(true);

            var result = new SamlRequest
            {
                ForceAuthn = forceAuthn,
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

        private static string EncodeDeflateSaml(string xmlString)
        {
            // var deflatedSaml = Utilities.ExtensionMethods.ZipStr(xmlString);
            // var encodedSaml = Utilities.ExtensionMethods.Base64Encode(deflatedSaml);
            var encodedSaml = Utilities.ExtensionMethods.Base64Encode(Encoding.Default.GetBytes(xmlString));
            return encodedSaml;
        }
    }
}
