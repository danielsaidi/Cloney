﻿using System.Collections.Generic;
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
        private readonly IConsole console;
        private readonly ITranslator translator;
        private readonly IArgumentParser<IDictionary<string, string>> argumentParser;


        public SettingsRoutine()
            :this(Default.Console, Default.Translator)
        {
        }

        public SettingsRoutine(IConsole console, ITranslator translator)
        {
            this.console = console;
            this.translator = translator;

            argumentParser = Default.DictionaryArgumentParser;
        }


        public bool Run(IEnumerable<string> args)
        {
            return Run(argumentParser.ParseArguments(args));
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
