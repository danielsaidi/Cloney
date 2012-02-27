using System;
using System.Collections.Generic;
using Cloney.Core.Console;
using Cloney.Core.Localization;

namespace Cloney.Core.SubRoutines
{
    /// <summary>
    /// This sub routine triggers on the --settings arg.
    /// For now, it just prints the current settings to
    /// the console.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
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


        public bool Run(IEnumerable<string> args)
        {
            throw new NotImplementedException();
        }

        public bool Run(IDictionary<string, string> args)
        {
            if (!HasSingleArg(args, "settings", "true"))
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
