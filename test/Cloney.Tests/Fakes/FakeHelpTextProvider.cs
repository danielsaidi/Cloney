using System;
using Cloney.Commands;

namespace Cloney.Tests.Fakes
{
	public class FakeHelpTextProvider : IHelpTextProvider
	{
		public string GetHelpTextForApplication()
		{
			return "application help";
		}
		
		public string GetHelpTextForApplicationForCommand(string command)
		{
			return "command help";
		}
	}
}