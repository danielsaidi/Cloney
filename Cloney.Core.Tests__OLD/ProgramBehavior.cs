using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests
{
    [TestFixture]
    public class ProgramBehavior
    {
        private Program program;


        [SetUp]
        public void SetUp()
        {
            program = new Program();
            program.Console = Substitute.For<IConsole>();
        }


        [Test]
        public void Start_ShouldOutputErrorMessageWhenProgramCannotBeStarted()
        {
            program.Start(new List<string>());
        }
    }
}