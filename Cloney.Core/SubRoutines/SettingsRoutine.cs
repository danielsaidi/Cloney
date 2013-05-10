using System.Collections.Generic;
using Cloney.Core.Console;
using Cloney.Core.Localization;

namespace Cloney.Core.SubRoutines
{
    /// <summary>
    /// This sub routine triggers on this console command:
    /// cloney --settings
    /// When triggered, it prints current Cloney settings.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public class SettingsRoutine : SubRoutineBase, ISubRoutine
    {
        public SettingsRoutine()
            :this(Default.CommandLineArgumentParser, Default.Console, Default.Translator)
        {
        }

        public SettingsRoutine(ICommandLineArgumentParser argumentParser, IConsole console, ITranslator translator)
            : base(argumentParser, console, translator)
        {
        }


        public bool Run(IEnumerable<string> args)
        {
            return Run(ArgumentParser.ParseCommandLineArguments(args));
        }

        public bool Run(IDictionary<string, string> args)
        {
            if (!HasSingleArg(args, "settings", "true"))
                return false;

            var settingsMessage = Translator.Translate("SettingsMessage");
            var excludeFolderMessage = string.Join(", ", Default.ExcludeFolderPatterns);
            var excludeFileMessage = string.Join(", ", Default.ExcludeFilePatterns);
            var plainCopyFileMessage = string.Join(", ", Default.PlainCopyFilePatterns);
            var message = string.Format(settingsMessage, excludeFolderMessage, excludeFileMessage, plainCopyFileMessage);
            
            Console.WriteLine(message);
            return true;
        }
    }
}
