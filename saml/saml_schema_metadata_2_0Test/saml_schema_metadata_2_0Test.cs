//
// saml_schema_metadata_2_0Test.cs
//
// This file was generated by XMLSpy 2020r2sp1 Enterprise Edition.
//
// YOU SHOULD NOT MODIFY THIS FILE, BECAUSE IT WILL BE
// OVERWRITTEN WHEN YOU RE-RUN CODE GENERATION.
//
// Refer to the XMLSpy Documentation for further details.
// http://www.altova.com/xmlspy
//


using System;
using Altova.Types;

namespace saml_schema_metadata_2_0
{
	/// <summary>
	/// Summary description for saml_schema_metadata_2_0Test.
	/// </summary>
	class saml_schema_metadata_2_0Test
	{
		protected static void Example()
		{
			//
			// TODO:
			//   Insert your code here...
			//
			// Example code to create and save a structure:
			//   md.saml_schema_metadata_2_02 doc = md.saml_schema_metadata_2_02.CreateDocument();
			//   // Append root element
			//   xs.stringType root = doc.TelephoneNumber.Append();
			//   // Append root element with prefix
			//   // xs.stringType root = doc.TelephoneNumber.AppendWithPrefix("p");
			//   // Declare all namespaces from schema on root element 
			//   md.saml_schema_metadata_2_02.DeclareAllNamespacesFromSchema(root);
			//   // Declare namespaces on element 
			//   root.DeclareNamespace("ns1", "http://NamespaceTest.com/ns1");
			//   ...
			//   doc.SaveToFile("saml_schema_metadata_2_01.xml", true);
			//
			// Example code to load and save a structure:
			//   md.saml_schema_metadata_2_02 doc = md.saml_schema_metadata_2_02.LoadFromFile("saml_schema_metadata_2_01.xml");
			//   xs.stringType root = doc.TelephoneNumber.First;
			//   ...
			//   doc.SaveToFile("saml_schema_metadata_2_01.xml", true);
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		public static int Main(string[] args)
		{
			try
			{
				Console.WriteLine("saml_schema_metadata_2_0 Test Application");
				Example();
				Console.WriteLine("OK");
				return 0;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return 1;
			}
		}
	}
}