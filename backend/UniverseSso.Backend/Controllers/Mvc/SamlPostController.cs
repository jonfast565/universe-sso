using System;
using System.Collections.Generic;
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
            var samlResponseDocument = BuildSamlResponse();
            var encodedSaml = EncodeDeflateSaml(samlResponseDocument);

            ViewBag.ACSUrl = parsedSamlRequest.AcsUrl;
            ViewBag.SamlParameterName = "SAMLResponse";
            ViewBag.EncodedSaml = samlResponseDocument;

            return View();
        }

        private static string BuildSamlResponse()
        {
            var samlResponseDocument = saml_schema_protocol_2_02.CreateDocument();
            var response = samlResponseDocument.Response.Append();

            var issuer = response.Issuer.Append();
            issuer.Value = "https://localhost:5000/md";

            var status = response.Status.Append();
            var statusCode = status.StatusCode.Append();
            statusCode.Value2.Value = "urn:oasis:names:tc:SAML:2.0:status:Success";

            var assertion = response.Assertion.Append();
            var assertionIssuer = assertion.Issuer.Append();
            assertionIssuer.Value = "https://localhost:5000/md";

            //var subject = assertion.Subject.Append();
            //var conditions = assertion.Conditions.Append();
            //var authnStatement = assertion.AuthnStatement.Append();
            var attributeStatement = assertion.AttributeStatement.Append();

            var firstNameAttribute = attributeStatement.Attribute.Append();
            firstNameAttribute.NameFormat.Value = "urn:oasis:names:tc:SAML:2.0:attrname-format:basic";
            firstNameAttribute.Name.Value = "FirstName";
            firstNameAttribute.AttributeValue.Append().Value = "Jon";

            var lastNameAttribute = attributeStatement.Attribute.Append();
            lastNameAttribute.NameFormat.Value = "urn:oasis:names:tc:SAML:2.0:attrname-format:basic";
            lastNameAttribute.Name.Value = "LastName";
            lastNameAttribute.AttributeValue.Append().Value = "Fast";

            var document = samlResponseDocument.SaveToString(true);
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
            var deflatedSaml = Utilities.ExtensionMethods.ZipStr(xmlString);
            var encodedSaml = Utilities.ExtensionMethods.Base64Encode(deflatedSaml);
            return encodedSaml;
        }
    }
}
