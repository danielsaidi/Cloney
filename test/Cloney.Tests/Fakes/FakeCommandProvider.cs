using System;
using System.Collections.Generic;
using Cloney.Commands;

namespace Cloney.Tests.Fakes
{
	public class FakeCommandProvider : ICommandProvider
	{
		private IEnumerable<ICommand> _commands = new List<ICommand>();
		
		
		public IEnumerable<ICommand> GetAvailableCommands()
		{
			return _commands;
		}
		
		public void SetAvailableCommands(IEnumerable<ICommand> commands) {
			_commands = commands;
		}
	}
}