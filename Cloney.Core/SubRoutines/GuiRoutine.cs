using System.Collections.Generic;
using Cloney.Core.Console;
using Cloney.Core.Diagnostics;
using Cloney.Core.Localization;

namespace Cloney.Core.SubRoutines
{
    /// <summary>
    /// This sub routine will trigger if no arguments are
    /// provided. It starts the Cloney GUI and displays a
    /// short text in the console.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public class GuiRoutine : SubRoutineBase, ISubRoutine
    {
        private readonly IConsole console;
        private readonly ITranslator translator;
        private readonly IProcess process;


        public GuiRoutine()
            :this(Default.Console, Default.Translator, Default.Process)
        {
        }

        public GuiRoutine(IConsole console, ITranslator translator, IProcess process)
        {
            this.console = console;
            this.translator = translator;
            this.process = process;
        }


        public bool Run(IDictionary<string, string> args)
        {
            if (args.Count != 0)
                return false;

            console.WriteLine(translator.Translate("GuiStartMessage"));
            process.Start("Cloney.Wizard.exe");
            return true;
        }
    }
}
