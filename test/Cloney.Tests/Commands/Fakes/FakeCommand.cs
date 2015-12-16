using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Cloney.Commands;

namespace Cloney.Tests.Commands.Fakes
{
	public class FakeCommand : ICommand
	{
		public string Description 
		{ 
			get { return "fake"; } 
		}
		
		public string Name 
		{
			get { return "A fake command"; }
		}
		
		public string Usage 
		{ 
			get { return "None"; } 
		}
		
		
		public bool CanHandleArgs(IList<string> args)
		{
			return false;
		}
		
        public Task<bool> HandleArgs(IList<string> args)
		{
			return null;
		}
	}
}