using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cloney.CommandLine;

namespace Cloney.Commands
{
	public class HelpCommand : CommandBase, ICommand 
	{
		public HelpCommand(ICommandProvider commandProvider, IConsole console)
			: base("help")
		{
			_commandProvider = commandProvider;
			_console = console;
		}
		
		
		private ICommandProvider _commandProvider;
		private IConsole _console;
		
		
		public string Description 
		{ 
			get 
			{
				return "Get help on how to use the application or a specific command";
			} 
		}
		
		public string Usage 
		{ 
			get 
			{
				return @"
cloney
cloney help
cloney help <command>
";
			} 
		}
		
		
		public override bool CanHandleArgs(IList<string> args)
		{
			return args.Count <= 2 || base.CanHandleArgs(args);
		}
		
        public Task<bool> HandleArgs(IList<string> args)
		{
			return null;
		}
	}
}