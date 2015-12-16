using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cloney.Commands
{
	public abstract class CommandBase 
	{
        protected CommandBase(string name)
        {
			Name = name;
        }


        public string Name { get; }
		
		
		public virtual bool CanHandleArgs(IList<string> args)
		{
			if (args == null || args.Count == 0) {
				return false;
			}
			
			var firstArg = args[0].ToLower();
			return firstArg == Name.ToLower();
		}
	}
}