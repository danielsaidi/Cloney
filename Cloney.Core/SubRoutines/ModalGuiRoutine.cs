using System.Collections.Generic;
using Cloney.Core.Console;
using Cloney.Core.Diagnostics;
using Cloney.Core.Localization;

namespace Cloney.Core.SubRoutines
{
    /// <summary>
    /// This sub routine triggers on this console command:
    /// cloney --modal
    /// When triggered, it starts the Cloney GUI in modal
    /// mode, which only displays a modal window.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public class ModalGuiRoutine : SubRoutineBase, ISubRoutine
    {
        private readonly IProcess process;


        public ModalGuiRoutine()
            :this(Default.Process, Default.CommandLineArgumentParser, Default.Console, Default.Translator)
        {
        }

        public ModalGuiRoutine(IProcess process, ICommandLineArgumentParser argumentParser, IConsole console, ITranslator translator)
            : base(argumentParser, console, translator)
        {
            this.process = process;
        }


        public bool Run(IEnumerable<string> args)
        {
            return Run(ArgumentParser.ParseCommandLineArguments(args));
        }

        private bool Run(CommandLineArguments args)
        {
            if (!args.HasArgument("modal", "true"))
                return false;

            var source = args.HasArgument("source") ? args.Raw["source"] : null;
            var sourceString = source == null ? null : " --source=" + source;

            var message = Translator.Translate("GuiModalStartMessage");
            Console.WriteLine(message);
            process.Start(WizardApplicationPath, "--modal" + sourceString);

            return true;
        }
    }
}
