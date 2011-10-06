using System.Collections.Specialized;
using Cloney.Core.CommandLine.Abstractions;
using NExtra.Abstractions;
using NExtra.Diagnostics.Abstractions;

namespace Cloney.Core.CommandLine
{
    public class WizardApplicationFacade : IWizardApplication
    {
        private readonly IConsole console;
        private readonly IProcess process;
        private readonly StringDictionary startingArguments;
        private readonly string startingMessage;


        public WizardApplicationFacade(IConsole console, IProcess process, StringDictionary startingArguments, string startingMessage)
        {
            this.console = console;
            this.process = process;
            this.startingArguments = startingArguments;
            this.startingMessage = startingMessage;
        }


        public string Executable
        {
            get { return "Cloney.Wizard.exe"; }
        }

        public bool ShouldStart
        {
            get{ return startingArguments.Count <= 0; }
        }


        public bool Start()
        {
            if (!ShouldStart)
                return false;

            console.WriteLine(startingMessage);
            process.Start(Executable);
            return true;
        }
    }
}
