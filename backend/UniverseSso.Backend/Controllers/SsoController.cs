using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using saml_schema_assertion_2_0.ds;
using saml_schema_assertion_2_0.saml;
using saml_schema_protocol_2_0.samlp;

namespace UniverseSso.Backend.Controllers
{
    [Route("sso")]
    [ApiController]
    public class SsoController : ControllerBase
    {
        [HttpGet]
        public void SamlRedirect(string samlRequest, string relayState)
        {
            var decodedSaml = Utilities.ExtensionMethods.Base64Decode(samlRequest);
            var inflatedSaml = Utilities.ExtensionMethods.UnZipBytes(decodedSaml);
            var xml = saml_schema_protocol_2_02.LoadFromString(inflatedSaml);

            var authnRequest = xml.AuthnRequest.First;
            var forceAuthn = authnRequest.ForceAuthn.Value;
            // var isPassive = authnRequest.IsPassive.Value;
            var acsUrl = authnRequest.AssertionConsumerServiceURL.Value;
            var destinationUrl = authnRequest.Destination.Value;
            var protocolBinding = authnRequest.ProtocolBinding.Value;
            var id = authnRequest.ID.Value;
            var samlVersion = authnRequest.Version.Value;
            var issueInstant = authnRequest.IssueInstant.Value;

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

            var subject = assertion.Subject.Append();
            var conditions = assertion.Conditions.Append();
            var authnStatement = assertion.AuthnStatement.Append();
            var attributeStatement = assertion.AttributeStatement.Append();

            var firstNameAttribute = attributeStatement.Attribute.Append();
            firstNameAttribute.NameFormat.Value = "urn:oasis:names:tc:SAML:2.0:attrname-format:basic";
            firstNameAttribute.Name.Value = "FirstName";
            firstNameAttribute.AttributeValue.Append().Value = "Jon";

            var lastNameAttribute = attributeStatement.Attribute.Append();
            lastNameAttribute.NameFormat.Value = "urn:oasis:names:tc:SAML:2.0:attrname-format:basic";
            lastNameAttribute.Name.Value = "LastName";
            lastNameAttribute.AttributeValue.Append().Value = "Fast";

            // TODO: Add stuff here
            var xmlString = samlResponseDocument.SaveToString(true);
            var deflatedSaml = Utilities.ExtensionMethods.ZipStr(xmlString);
            var encodedSaml = Utilities.ExtensionMethods.Base64Encode(Encoding.UTF8.GetString(deflatedSaml));
            
            Console.WriteLine(xmlString);
            Console.WriteLine(encodedSaml);


        }

        [HttpPost]
        public void SamlPost([FromBody] string samlRequest)
        {
            var decodedSaml = Utilities.ExtensionMethods.Base64Decode(samlRequest);
            var inflatedSaml = Utilities.ExtensionMethods.UnZipBytes(decodedSaml);
            var xml = saml_schema_assertion_2_02.LoadFromString(inflatedSaml);
            Console.WriteLine(inflatedSaml);
        }
    }
}