using System.Collections.Generic;
using System.Linq;
using Cloney.Core.Cloners;
using Cloney.Core.Namespace;
using NExtra;
using NExtra.Diagnostics;
using NExtra.IO;
using NExtra.Localization;

namespace Cloney.Core
{
    /// <summary>
    /// This class provides default implementations and
    /// settings for Cloney.
    /// </summary>
    public static class Default
    {
        public static IConsole Console
        {
            get { return new ConsoleFacade(); }
        }

        public static IEnumerable<string> ExcludeFilePatterns
        {
            get { return Settings.ExcludeFilePatterns.Cast<string>(); }
        }

        public static IEnumerable<string> ExcludeFolderPatterns
        {
            get { return Settings.ExcludeFolderPatterns.Cast<string>(); }
        }

        public static IPathPatternMatcher PathPatternMatcher
        {
            get { return new PathPatternMatcher(); }
        }

        public static IEnumerable<string> PlainCopyFilePatterns
        {
            get { return Settings.PlainCopyFilePatterns.Cast<string>(); }
        }

        public static IProcess Process
        {
            get { return new ProcessFacade(); }
        }

        internal static Properties.Settings Settings
        {
            get { return Properties.Settings.Default; }
        }


        public static ISolutionCloner SolutionCloner
        {
            get
            {
                var excludeFolderPatterns = Settings.ExcludeFolderPatterns.Cast<string>();
                var excludeFilePatterns = Settings.ExcludeFilePatterns.Cast<string>();
                var plainCopyFilePatterns = Settings.PlainCopyFilePatterns.Cast<string>();

                return new SolutionCloner(SourceNamespaceResolver, TargetNamespaceResolver, PathPatternMatcher, excludeFolderPatterns, excludeFilePatterns, plainCopyFilePatterns);
            }
        }

        public static INamespaceResolver SourceNamespaceResolver
        {
            get { return new SolutionBasedNamespaceResolver(new DirectoryFacade()); }
        }

        public static INamespaceResolver TargetNamespaceResolver
        {
            get { return new FolderBasedNamespaceResolver(); }
        }

        public static ITranslator Translator
        {
            get { return new LanguageProvider(); }
        }
    }
}
