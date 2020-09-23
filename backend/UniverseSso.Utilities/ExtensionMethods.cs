using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace UniverseSso.Utilities
{
    public static class ExtensionMethods
    {
        /// <summary>
        ///     throw if a string is null or empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requiredObject"></param>
        /// <returns></returns>
        public static T ThrowOnNullOrEmpty<T>(this T requiredObject)
        {
            switch (requiredObject)
            {
                case null:
                    throw new Exception("The object is null when it should not be.");
                case string s when string.IsNullOrEmpty(s):
                    throw new Exception("The string is empty when it should not be.");
            }

            return requiredObject;
        }

        /// <summary>
        ///     Convert a password to a secure string
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static SecureString ToSecureString(this string password)
        {
            var result = new SecureString();

            if (string.IsNullOrEmpty(password))
            {
                return result;
            }

            foreach (var character in password)
            {
                result.AppendChar(character);
            }

            return result;
        }

        public static byte[] ZipStr(string str)
        {
            using var output = new MemoryStream();
            using (var gzip = new DeflateStream(output, CompressionMode.Compress))
            {
                using var writer = new StreamWriter(gzip, Encoding.UTF8);
                writer.Write(str);
            }

            return output.ToArray();
        }

        public static string UnZipBytes(byte[] input)
        {
            using var inputStream = new MemoryStream(input);
            using var gzip = new DeflateStream(inputStream, CompressionMode.Decompress);
            using var reader = new StreamReader(gzip, Encoding.UTF8);
            return reader.ReadToEnd();
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static byte[] Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return base64EncodedBytes;
        }
    }
}
