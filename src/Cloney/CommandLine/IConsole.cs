using System;

namespace Cloney.CommandLine
{
	public interface IConsole 
	{
		void Write(string text);
		void WriteLine(string text);
	}
}