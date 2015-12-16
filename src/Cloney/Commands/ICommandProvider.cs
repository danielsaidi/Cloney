using System;
using System.Collections.Generic;

namespace Cloney.Commands
{
	public interface ICommandProvider 
	{
		IEnumerable<ICommand> GetAvailableCommands();
	}
}