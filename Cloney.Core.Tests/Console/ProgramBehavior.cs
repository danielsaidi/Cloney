using System.Collections.Generic;
using Cloney.Core.Console.SubRoutines;
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
        private ISubRoutineLocator subRoutineLocator;
        private ICommandLineArgumentParser argumentParser;

        private IEnumerable<string> arguments;


        [SetUp]
        public void SetUp()
        {
            console = Substitute.For<IConsole>();
            argumentParser = Substitute.For<ICommandLineArgumentParser>();
            subRoutineLocator = Substitute.For<ISubRoutineLocator>();

            program = new Core.Console.Program(console, argumentParser, subRoutineLocator);

            arguments = new List<string>();
        }


        [Test]
        public void Start_ShouldParseArguments()
        {
            program.Start(arguments);

            argumentParser.Received().ParseCommandLineArguments(arguments);
        }

        [Test]
        public void Start_ShouldLookForSubRoutines()
        {
            program.Start(arguments);

            subRoutineLocator.Received().FindAll();
        }

        [Test]
        public void Start_ShouldNotFailForMissingSubRoutines()
        {
            program.Start(arguments);
        }
    }
}