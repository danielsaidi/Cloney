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
			return new List<ICommand> {
				new HelpCommand(this, console)
			};
		}
	}
}