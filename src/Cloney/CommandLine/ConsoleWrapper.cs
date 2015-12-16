using System;

namespace Cloney.CommandLine
{
	public class ConsoleWrapper : IConsole
	{
		public void Write(string text) 
		{
			Console.Write(text);
		}
		
		public void WriteLine(string text)
		{
			Console.WriteLine(text);
		}
	}
}