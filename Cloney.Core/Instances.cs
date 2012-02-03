using System.Linq;
using Cloney.Core.Namespace;
using Cloney.Core.SolutionCloners;
using NExtra;
using NExtra.Diagnostics;
using NExtra.IO;
using NExtra.Localization;

namespace Cloney.Core
{
    public static class Instances
    {
        public static IConsole Console
        {
            get { return new ConsoleFacade(); }
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
                var sourceResolver = new SolutionBasedNamespaceResolver(new DirectoryFacade());
                var targetResolver = new FolderBasedNamespaceResolver();
                var patternMatcher = new PathPatternMatcher();
                var excludeFolderPatterns = Settings.ExcludeFolderPatterns.Cast<string>();
                var excludeFilePatterns = Settings.ExcludeFilePatterns.Cast<string>();
                var plainCopyFilePatterns = Settings.PlainCopyFilePatterns.Cast<string>();

                return new SolutionCloner(sourceResolver, targetResolver, patternMatcher, excludeFolderPatterns, excludeFilePatterns, plainCopyFilePatterns);
            }
        }

        public static ITranslator Translator
        {
            get { return new LanguageProvider(); }
        }
    }
}
