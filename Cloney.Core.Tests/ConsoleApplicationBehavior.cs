using System.Collections.Generic;
using NExtra;
using NExtra.Diagnostics;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests
{
    [TestFixture]
    public class ConsoleApplicationBehavior
    {
        private ConsoleApplication program;


        [SetUp]
        public void SetUp()
        {
            program = new ConsoleApplication(Substitute.For<IConsole>());
        }
    }
}