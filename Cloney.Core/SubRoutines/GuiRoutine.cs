using System;
using System.Collections.Generic;
using NExtra;
using NExtra.Diagnostics;
using NExtra.Localization;

namespace Cloney.Core.SubRoutines
{
    /// <summary>
    /// This sub routine will trigger if no arguments
    /// are provided. It will start the Cloney wizard
    /// and display some start instructions.
    /// </summary>
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
