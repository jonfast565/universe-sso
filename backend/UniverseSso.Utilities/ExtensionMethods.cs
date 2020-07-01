using System;
using System.Collections.Generic;
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
    }
}
