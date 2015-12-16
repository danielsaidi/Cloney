using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Cloney.Commands;

namespace Cloney.Tests.Commands.Fakes
{
	public class FakeCommandProvider : ICommandProvider
	{
		public IEnumerable<ICommand> GetAvailableCommands()
		{
			return new List<ICommand> {
				new FakeCommand()
			};
		}
	}
}