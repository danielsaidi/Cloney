using System.Collections.Generic;
using System.Linq;
using Cloney.Core.Cloners;
using Cloney.Core.Console;
using Cloney.Core.Diagnostics;
using Cloney.Core.IO;
using Cloney.Core.Localization;
using Cloney.Core.Namespace;
using Cloney.Core.SubRoutines;

namespace Cloney.Core
{
    /// <summary>
    /// This class has default instances and settings for
    /// Cloney. It is used as a primitive IoC container.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public static class Default
    {
        public static IConsole Console
        {
            get { return new ConsoleFacade(); }
        }

        public static IArgumentParser<IDictionary<string, string>> DictionaryArgumentParser
        {
            get { return new DictionaryArgumentParser(); }
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

                return new SolutionCloner(SourceFolderNamespaceResolver, TargetNamespaceResolver, PathPatternMatcher, excludeFolderPatterns, excludeFilePatterns, plainCopyFilePatterns);
            }
        }

        public static INamespaceResolver SourceFolderNamespaceResolver
        {
            get { return new SolutionFolderNamespaceResolver(new DirectoryFacade()); }
        }

        public static ISubRoutineLocator SubRoutineLocator
        {
            get { return new LocalSubRoutineLocator(); }
        }

        public static INamespaceResolver TargetNamespaceResolver
        {
            get { return new FolderNamespaceResolver(); }
        }

        public static ITranslator Translator
        {
            get { return new ResourceManagerFacade(Language.ResourceManager); }
        }

        public static IArgumentParser<Wizard.ApplicationArguments> WizardApplicationArgumentsParser
        {
            get { return new Wizard.ApplicationArgumentParser(); }
        }

        
    }
}
