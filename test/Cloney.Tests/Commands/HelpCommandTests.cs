using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Cloney.Commands;

namespace Cloney.Tests.Commands
{
	public class HelpCommandTests 
	{
		private ICommand _command;
		
		
		public HelpCommandTests()
		{
			_command = new HelpCommand();
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