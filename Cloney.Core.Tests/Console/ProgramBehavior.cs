using System.Collections.Generic;
using NExtra;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests.Console
{
    [TestFixture]
    public class ProgramBehavior
    {
        private IProgram program;
        private IConsole console;
        private ICommandLineArgumentParser argumentParser;

        private IEnumerable<string> arguments;


        [SetUp]
        public void SetUp()
        {
            console = Substitute.For<IConsole>();
            argumentParser = Substitute.For<ICommandLineArgumentParser>();

            program = new Core.Console.Program(console, argumentParser);

            arguments = new List<string>();
        }


        [Test]
        public void Start_ShouldParseArguments()
        {
            program.Start(arguments);

            argumentParser.Received().ParseCommandLineArguments(arguments);
        }
    }
}