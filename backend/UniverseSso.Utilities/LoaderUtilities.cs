using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UniverseSso.Models.Interfaces;

namespace UniverseSso.Utilities
{
    public static class LoaderUtilities
    {
        public static List<Type> LoadDlls(string[] dllPaths)
        {
            var types = new List<Type>();
            foreach (var dllPath in dllPaths)
            {
                var dll = Assembly.LoadFile(dllPath);
                var exportedTypes = dll.GetExportedTypes();
                types.AddRange(exportedTypes);
            }

            return types;
        }
    }
}
