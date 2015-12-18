using System;
using Cloney.CommandLine;

namespace Cloney.Tests.Fakes
{
	public class FakeConsole : IConsole
	{
		private string _written;
		private string _writtenLine;
		
		
		public void Write(string text)
		{
			_written = text;
		}
	
		public void WriteLine(string text)
		{
			_writtenLine = text;			
		}
		
		
		public string Written => _written;
		
		public string WrittenLine => _writtenLine;
	}
}