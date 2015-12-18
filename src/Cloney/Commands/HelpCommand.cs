using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cloney.CommandLine;

namespace Cloney.Commands
{
	public class HelpCommand : CommandBase, ICommand 
	{
		public HelpCommand(ICommandProvider commandProvider, IConsole console, IHelpTextProvider helpTextProvider)
			: base("help")
		{
			_commandProvider = commandProvider;
			_console = console;
			_helpTextProvider = helpTextProvider;
		}
		
		
		private ICommandProvider _commandProvider;
		private IConsole _console;
		private IHelpTextProvider _helpTextProvider;
		
		
		public string Description 
		{ 
			get { return "Get help on a specific command"; } 
		}
		
		public string Usage 
		{ 
			get { return @"cloney help <command>"; } 
		}

		
		public override bool CanHandleArgs(IList<string> args)
		{
			if (IsApplicationHelpArguments(args)) 
			{
				return true;
			}
			
			if (!IsCommandHelpArguments(args)) 
			{
				return false;
			}
			
			return IsExistingCommandArguments(args);
		}
		
        public Task<bool> HandleArgs(IList<string> args)
		{
			if (IsApplicationHelpArguments(args)) 
			{
				var helpText = _helpTextProvider.GetHelpTextForApplication();
				_console.WriteLine(helpText); 
				return Task.FromResult(true);
			}
			
			if (IsExistingCommandArguments(args)) 
			{
				var helpText = _helpTextProvider.GetHelpTextForCommand(args[1]);
				_console.WriteLine(helpText); 
				return Task.FromResult(true);
			}
			
			return Task.FromResult(false);
		}
		
		
		private bool IsApplicationHelpArguments(IList<string> args)
		{
			return (args == null || args.Count == 0 || (args.Count == 1 && base.CanHandleArgs(args)));
		}
		
		private bool IsCommandHelpArguments(IList<string> args)
		{
			return (args != null && args.Count == 2 && base.CanHandleArgs(args));
		}
		
		private bool IsExistingCommandArguments(IList<string> args)
		{
			if (!IsCommandHelpArguments(args)) 
			{
				return false;
			}
			
			var commandArg = args[1].ToLower();
			var commands = _commandProvider.GetAvailableCommands();
			return commands.Any(x => x.Name.ToLower() == commandArg);
		}
	}
}