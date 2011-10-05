using System.Collections.Specialized;
using Cloney.Abstractions;
using NExtra.Abstractions;
using NExtra.Diagnostics.Abstractions;

namespace Cloney
{
    class WizardFacade : IWizardFacade
    {
        private readonly IConsole console;
        private readonly IProcess process;
        private readonly string executable;
        private readonly string startingMessage;


        public WizardFacade(IConsole console, IProcess process, string executable, string startingMessage)
        {
            this.console = console;
            this.process = process;
            this.executable = executable;
            this.startingMessage = startingMessage;
        }


        public bool ShouldStart(StringDictionary applicationArguments)
        {
            return applicationArguments.Count <= 0;
        }

        public bool Start(StringDictionary applicationArguments)
        {
            if (!ShouldStart(applicationArguments))
                return false;

            console.WriteLine(startingMessage);
            process.Start(executable);
            return true;
        }
    }
}
