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

        public static ISolutionCloner SolutionCloner
        {
            get
            {
                var sourceResolver = new SolutionBasedNamespaceResolver(new DirectoryFacade());
                var targetResolver = new FolderBasedNamespaceResolver();
                var patternMatcher = new PathPatternMatcher();
                var cloner = new SolutionCloner(sourceResolver, targetResolver, patternMatcher);
                return cloner;
                return new ThreadedSolutionCloner(cloner);
            }
        }

        public static ITranslator Translator
        {
            get { return new LanguageProvider(); }
        }
    }
}
