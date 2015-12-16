using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Cloney.Commands;
using Cloney.Tests.Commands.Fakes;

namespace Cloney.Tests.Commands
{
	public class HelpCommandTests 
	{
		private ICommand _command;
		private ICommandProvider _commandProvider;
		
		
		public HelpCommandTests()
		{
			_commandProvider = new FakeCommandProvider();
			_command = new HelpCommand(_commandProvider);
		}
		
		
		[Fact]
        public void CommandHasUniqueName()
        {
			Assert.Equal(_command.Name, "help");
        }
		
		[Fact]
        public void CommandCanHandleEmptyArguments()
        {
			var args = new List<string>();
			var result = _command.CanHandleArgs(args);
			
			Assert.True(result);
        }
	}
}