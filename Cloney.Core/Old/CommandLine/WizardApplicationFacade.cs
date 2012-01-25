using System.Collections.Generic;
using Cloney.Core.Old.CommandLine.Abstractions;
using NExtra.Abstractions;
using NExtra.Diagnostics.Abstractions;

namespace Cloney.Core.Old.CommandLine
{
    public class WizardApplicationFacade : IWizardApplication
    {
        private readonly IConsole console;
        private readonly IProcess process;
        private readonly string executable;
        private readonly IDictionary<string, string> startingArguments;
        private readonly string startingMessage;


        public WizardApplicationFacade(IConsole console, IProcess process, string executable, IDictionary<string,string> startingArguments, string startingMessage)
        {
            this.console = console;
            this.process = process;
            this.executable = executable;
            this.startingArguments = startingArguments;
            this.startingMessage = startingMessage;
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
            process.Start(executable);
            return true;
        }
    }
}
