using System.Collections.Generic;
using NExtra;
using NExtra.Localization;

namespace Cloney.Core.SubRoutines
{
    /// <summary>
    /// This routine will display application settings. It
    /// cannot be used to edit the application settings as
    /// of now.
    /// </summary>
    public class SettingsRoutine : SubRoutineBase, ISubRoutine
    {
        private readonly IConsole console;
        private readonly ITranslator translator;


        public SettingsRoutine()
            :this(Default.Console, Default.Translator)
        {
        }

        public SettingsRoutine(IConsole console, ITranslator translator)
        {
            this.console = console;
            this.translator = translator;
        }


        public bool Run(IDictionary<string, string> args)
        {
            if (!ArgsHaveSingleKeyValue(args, "settings", "true"))
                return false;

            var settingsMessage = translator.Translate("SettingsMessage");
            var excludeFolderMessage = string.Join(", ", Default.ExcludeFolderPatterns);
            var excludeFileMessage = string.Join(", ", Default.ExcludeFilePatterns);
            var plainCopyFileMessage = string.Join(", ", Default.PlainCopyFilePatterns);
            var message = string.Format(settingsMessage, excludeFolderMessage, excludeFileMessage, plainCopyFileMessage);
            
            console.WriteLine(message);
            return true;
        }
    }
}
