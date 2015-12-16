using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cloney.Commands
{
	public class HelpCommand : CommandBase, ICommand 
	{
		public HelpCommand(ICommandProvider commandProvider)
			: base("help")
		{
		}
		
		
		private ICommandProvider _commandProvider;
		
		
		public string Description 
		{ 
			get 
			{
				return "Use help";
			} 
		}
		
		public string Usage 
		{ 
			get 
			{
				return "cloney help <command>";
			} 
		}
		
		
		public override bool CanHandleArgs(IList<string> args)
		{
			return args.Count == 0 || base.CanHandleArgs(args);
		}
		
        public Task<bool> HandleArgs(IList<string> args)
		{
			return null;
		}
	}
}