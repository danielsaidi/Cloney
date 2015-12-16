using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Cloney.CommandLine;
using Cloney.Commands;
using Cloney.Tests.Commands.Fakes;

namespace Cloney.Tests.Commands
{
	public class HelpCommandTests 
	{
		private ICommand _command;
		private ICommandProvider _commandProvider;
		private IConsole _console;
		
		
		public HelpCommandTests()
		{
			_commandProvider = new FakeCommandProvider();
			_console = new ConsoleWrapper(); //TODO: Fake
			_command = new HelpCommand(_commandProvider, _console);
		}
		
		
		[Fact]
        public void CommandHasAName()
        {
			Assert.Equal(_command.Name, "help");
        }
		
		[Fact]
        public void CommandHasADescription()
        {
			Assert.Equal(_command.Description, "Get help on how to use the application or a specific command");
        }
		
		[Fact]
        public void CommandHasAUsage()
        {
			Assert.Equal(_command.Usage, @"
cloney
cloney help
cloney help <command>
");
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