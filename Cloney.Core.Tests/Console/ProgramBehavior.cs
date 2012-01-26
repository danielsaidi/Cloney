using NExtra;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests.Console
{
    [TestFixture]
    public class ProgramBehavior
    {
        private IProgram program;


        [SetUp]
        public void SetUp()
        {
            program = new Core.Console.Program(Substitute.For<IConsole>());
        }
    }
}