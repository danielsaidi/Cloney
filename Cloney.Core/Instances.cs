using Cloney.Core.IO;
using Cloney.Core.Namespace;
using Cloney.Core.SolutionCloners;
using NExtra;
using NExtra.Diagnostics;
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

        public static ISolutionCloner SolutionCloner
        {
            get { return new SolutionCloner(new SolutionBasedNamespaceResolver(new DirectoryFacade()), new FolderBasedNamespaceResolver(), new PathPatternMatcher()); }
        }

        public static ITranslator Translator
        {
            get { return new LanguageProvider(); }
        }
    }
}
