using System.Collections.Generic;
using NExtra;
using NExtra.Localization;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests
{
    [TestFixture]
    public class ProgramBehavior
    {
        private Program program;
        private List<string> arguments;

        private IConsole console;
        private ITranslator translator;
        private ISubRoutineLocator subRoutineLocator;
        private ICommandLineArgumentParser argumentParser;
 

        [SetUp]
        public void SetUp()
        {
            console = Substitute.For<IConsole>();
            translator = Substitute.For<ITranslator>();
            argumentParser = Substitute.For<ICommandLineArgumentParser>();
            subRoutineLocator = Substitute.For<ISubRoutineLocator>();

            program = new Program(console, translator, argumentParser, subRoutineLocator);
            arguments = new List<string> { "foo", "bar" };

            SetUpLanguage();
        }

        private void SetUpLanguage()
        {
            translator.Translate("StartErrorMessage").Returns("StartErrorMessage");
        }


        [Test]
        public void Start_ShouldFailWithPrettyErrorMessage()
        {
            program = new Program(console, translator, null, null);

            program.Start(arguments);

            translator.Received().Translate("StartErrorMessage");
            console.Received().WriteLine("StartErrorMessage");
            console.Received().WriteLine("Object reference not set to an instance of an object.");
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