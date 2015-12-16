using System;
using System.Collections.Generic;

namespace Cloney.Commands
{
	public class CommandProvider : ICommandProvider 
	{
		public IEnumerable<ICommand> GetAvailableCommands()
		{
			return new List<ICommand> {
				new HelpCommand(this)
			};
		}
	}
}