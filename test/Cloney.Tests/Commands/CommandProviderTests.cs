using System;
using System.Linq;
using Xunit;
using Cloney.Commands;

namespace Cloney.Tests.Commands
{
	public class CommandProviderTests 
	{
		private ICommandProvider _provider;
		
		
		public CommandProviderTests()
		{
			_provider = new CommandProvider();
		}
		
		
		[Fact]
        public void ReturnsAllAvailableCommands()
        {
			var result = _provider.GetAvailableCommands();
		
			Assert.Equal(result.Count(), 1);
			Assert.True(result.Any(x => x.Name == "help"));
        }
	}
}