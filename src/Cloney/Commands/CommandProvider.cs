using System;
using System.Collections.Generic;
using Cloney.CommandLine;

namespace Cloney.Commands
{
	public class CommandProvider : ICommandProvider 
	{
		public IEnumerable<ICommand> GetAvailableCommands()
		{
			var console = new ConsoleWrapper();	//TODO: IoC
			var helpTextProvider = (IHelpTextProvider)null;	//TODO: Real
			return new List<ICommand> {
				new HelpCommand(this, console, helpTextProvider)
			};
		}
	}
}