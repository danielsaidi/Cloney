using System.Collections.Generic;
using Cloney.Core.Console;
using Cloney.Core.Localization;
using Cloney.Core.SubRoutines;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests.SubRoutines
{
    [TestFixture]
    public class HelpRoutineBehavior
    {
        private IEnumerable<string> args;
        private ISubRoutine routine;
        private IConsole console;
        private ITranslator translator;
        private ICommandLineArgumentParser commandLineArgumentParser;


        [SetUp]
        public void SetUp()
        {
            args = new[] {"foo"};
            console = Substitute.For<IConsole>();
            translator = Substitute.For<ITranslator>();
            translator.Translate("GeneralHelpMessage").Returns("foo");
            commandLineArgumentParser = Substitute.For<ICommandLineArgumentParser>();

            routine = new HelpRoutine(console, translator, commandLineArgumentParser);
        }


        [Test]
        public void Run_ShouldParseIEnumerableToDictionary()
        {
            routine.Run(args);

            commandLineArgumentParser.Received().ParseCommandLineArguments(args);
        }

        [Test]
        public void Run_ShouldAbortForNoArguments()
        {
            commandLineArgumentParser.ParseCommandLineArguments(args).Returns(new Dictionary<string, string>());
            var result = routine.Run(args);

            Assert.That(result, Is.False);
            console.DidNotReceive().WriteLine(Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldAbortForMoreThanOneArgument()
        {
            commandLineArgumentParser.ParseCommandLineArguments(args).Returns(new Dictionary<string, string> { { "help", "true" }, { "foo", "bar" } });
            var result = routine.Run(args);

            Assert.That(result, Is.False);
            console.DidNotReceive().WriteLine(Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldAbortForIrrelevantArguments()
        {
            commandLineArgumentParser.ParseCommandLineArguments(args).Returns(new Dictionary<string, string> { { "foo", "bar" } });
            var result = routine.Run(args);

            Assert.That(result, Is.False);
            console.DidNotReceive().WriteLine(Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldProceedForRelevantArgument()
        {
            commandLineArgumentParser.ParseCommandLineArguments(args).Returns(new Dictionary<string, string> { { "help", "true" } });
            var result = routine.Run(args);

            Assert.That(result, Is.True);
            translator.Received().Translate("GeneralHelpMessage");
            console.Received().WriteLine("foo");
        }
    }
}