using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cloney.Commands
{
	public interface ICommand 
	{
		string Description { get; }
		string Name { get; }
		string Usage { get; }
		
		bool CanHandleArgs(IList<string> args);
        Task<bool> HandleArgs(IList<string> args);
	}
}