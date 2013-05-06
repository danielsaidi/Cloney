using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Cloney.Core.Reflection
{
    /// <summary>
    /// Extension methods for System.Reflection.Assembly.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/nextra
    /// </remarks>
    public static class Assembly_Extensions
    {
        public static string GetCompanyName(this Assembly assembly)
        {
            var attributes = assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            return attributes.Length == 0 ? "" : ((AssemblyCompanyAttribute)attributes[0]).Company;
        }

        public static string GetCopyrightHolder(this Assembly assembly)
        {
            var attributes = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            return attributes.Length == 0 ? "" : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
        }

        public static string GetDescription(this Assembly assembly)
        {
            var attributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            return attributes.Length == 0 ? "" : ((AssemblyDescriptionAttribute)attributes[0]).Description;
        }

        public static IEnumerable<string> GetNamespaces(this Assembly assembly)
        {
            var types = assembly.GetTypes();
            var result = new List<string>();

            foreach (var type in types.Where(type => !result.Contains(type.Namespace)))
                result.Add(type.Namespace);

            return result.OrderBy(ns => ns).ToList();
        }

        public static IEnumerable<Type> GetNamespaceTypes(this Assembly assembly, string @namespace)
        {
            return (from type in assembly.GetTypes() where type.Namespace == @namespace select type).ToList();
        }

        public static string GetProductName(this Assembly assembly)
        {
            var attributes = assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            return attributes.Length == 0 ? "" : ((AssemblyProductAttribute)attributes[0]).Product;
        }

        public static string GetTitle(this Assembly assembly)
        {
            var attributes = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            return attributes.Length == 0 ? Path.GetFileNameWithoutExtension(assembly.CodeBase) : ((AssemblyTitleAttribute)attributes[0]).Title;
        }

        public static Version GetVersion(this Assembly assembly)
        {
            return assembly.GetName().Version;
        }
    }
}
