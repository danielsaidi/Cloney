using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cloney.Commands
{
	public interface IHelpTextProvider 
	{
		string GetHelpTextForApplication();
		string GetHelpTextForCommand(string command);
	}
}