using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Cloney.CommandLine;
using Cloney.Commands;
using Cloney.Tests.Fakes;

namespace Cloney.Tests.Commands
{
	public class HelpCommandTests 
	{
		private ICommand _command;
		
		private FakeCommandProvider _commandProvider;
		private FakeConsole _console;
		private FakeHelpTextProvider _helpTextProvider;
		
		
		public HelpCommandTests()
		{
			_commandProvider = new FakeCommandProvider();
			_console = new FakeConsole();
			_helpTextProvider = new FakeHelpTextProvider();
			
			_command = new HelpCommand(_commandProvider, _console, _helpTextProvider);
		}
		
		
		public class CommandInformation : HelpCommandTests
		{
			[Fact]
			public void HasName()
			{
				Assert.Equal(_command.Name, "help");
			}
			
			[Fact]
			public void HasDescription()
			{
				Assert.Equal(_command.Description, "Get help on a specific command");
			}
			
			[Fact]
			public void HasUsage()
			{
				Assert.Equal(_command.Usage, "cloney help <command>");
			}
		}
		
		public class NullArgument : HelpCommandTests
		{
			[Fact]
			public void CanBeHandled()
			{
				var result = _command.CanHandleArgs(null);
				
				Assert.True(result);
			}
			
			[Fact]
			public async Task IsHandled()
			{
				var result = await _command.HandleArgs(null);
				
				Assert.True(result);
			}
			
			[Fact]
			public async Task PrintsApplicationHelpWhenHandled()
			{
				var expectedOutput = _helpTextProvider.GetHelpTextForApplication();
				
				await _command.HandleArgs(null);
				
				Assert.Equal(_console.WrittenLine, expectedOutput);
			}
		}
		
		public class EmptyArgument : HelpCommandTests
		{
			[Fact]
			public void CanBeHandled()
			{
				var args = new List<string>();
				
				var result = _command.CanHandleArgs(args);
				
				Assert.True(result);
			}
			
			[Fact]
			public async Task IsHandled()
			{
				var args = new List<string>();
				
				var result = await _command.HandleArgs(args);
				
				Assert.True(result);
			}
			
			[Fact]
			public async Task PrintsApplicationHelpWhenHandled()
			{
				var args = new List<string>();
				var expectedOutput = _helpTextProvider.GetHelpTextForApplication();
				
				await _command.HandleArgs(args);
				
				Assert.Equal(_console.WrittenLine, expectedOutput);
			}
		}
		
		
		public class SingleInvalidArgument : HelpCommandTests
		{
			[Fact]
			public void CanNotBeHandled()
			{
				var args = new List<string> { "foo" };
				
				var result = _command.CanHandleArgs(args);
				
				Assert.False(result);
			}
			
			[Fact]
			public async Task IsNotHandled()
			{
				var args = new List<string> { "foo" };
				
				var result = await _command.HandleArgs(args);
				
				Assert.False(result);
			}
			
			[Fact]
			public async Task DoesNotPrintApplicationHelpWhenHandled()
			{
				var args = new List<string> { "foo" };
				var expectedOutput = _helpTextProvider.GetHelpTextForApplication();
				
				await _command.HandleArgs(args);
				
				Assert.NotEqual(_console.WrittenLine, expectedOutput);
			}
		}
		
		public class SingleValidArgument : HelpCommandTests
		{
			[Fact]
			public void CanBeHandled()
			{
				var args = new List<string> { "help" };
				
				var result = _command.CanHandleArgs(args);
				
				Assert.True(result);
			}
			
			[Fact]
			public async Task IsHandled()
			{
				var args = new List<string> { "help" };
				
				var result = await _command.HandleArgs(args);
				
				Assert.True(result);
			}
			
			[Fact]
			public async Task PrintsApplicationHelpWhenHandled()
			{
				var args = new List<string> { "help" };
				var expectedOutput = _helpTextProvider.GetHelpTextForApplication();
				
				await _command.HandleArgs(args);
				
				Assert.Equal(_console.WrittenLine, expectedOutput);
			}
		}
	}
}