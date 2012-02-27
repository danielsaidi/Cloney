using System.Collections.Generic;
using Cloney.Core.Cloners;
using Cloney.Core.Console;
using Cloney.Core.Localization;
using Cloney.Core.SubRoutines;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests.SubRoutines
{
    [TestFixture]
    public class CloneRoutineBehavior
    {
        private IEnumerable<string> args;

        private ISubRoutine routine;
        private IConsole console;
        private ICommandLineArgumentParser argumentParser;
        private ITranslator translator;
        private ISolutionCloner solutionCloner;


        [SetUp]
        public void SetUp()
        {
            args = new List<string>();

            console = Substitute.For<IConsole>();
            console.ReadLine().Returns("val");
            translator = Substitute.For<ITranslator>();
            translator.Translate("EnterFolderPath").Returns("foo");
            argumentParser = Substitute.For<ICommandLineArgumentParser>();
            argumentParser.ParseCommandLineArguments(args).Returns(new Dictionary<string, string>());
            solutionCloner = Substitute.For<ISolutionCloner>();

            routine = new CloneRoutine(console, translator, argumentParser, solutionCloner);
        }


        [Test]
        public void Run_ShouldAbortForNoArguments()
        {
            var result = routine.Run(args);

            Assert.That(result, Is.False);
            solutionCloner.DidNotReceive().CloneSolution(Arg.Any<string>(), Arg.Any<string>());
        }
        
        [Test]
        public void Run_ShouldAbortForIrrelevantArguments()
        {
            var arguments = new Dictionary<string, string> {{"foo", "bar"}};
            argumentParser.ParseCommandLineArguments(args).Returns(arguments);

            var result = routine.Run(args);

            Assert.That(result, Is.False);
            solutionCloner.DidNotReceive().CloneSolution(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldRequireConsoleInputForMissingSourcePath()
        {
            var arguments = new Dictionary<string, string> { { "clone", "true" }, { "target", "bar" } };
            argumentParser.ParseCommandLineArguments(args).Returns(arguments);

            routine.Run(args);

            translator.Received().Translate("EnterFolderPath");
            console.Received().Write("foo");
        }

        [Test]
        public void Run_ShouldCloneWithConsoleInputForMissingSourcePath()
        {
            var arguments = new Dictionary<string, string> { { "clone", "true" }, { "target", "bar" } };
            argumentParser.ParseCommandLineArguments(args).Returns(arguments);

            var result = routine.Run(args);

            Assert.That(result, Is.True);
            console.Received().ReadLine();
            solutionCloner.Received().CloneSolution("val", "bar");
        }

        [Test]
        public void Run_ShouldRequireConsoleInputForMissingTargetPath()
        {
            var arguments = new Dictionary<string, string> { { "clone", "true" }, { "source", "foo" } };
            argumentParser.ParseCommandLineArguments(args).Returns(arguments);

            routine.Run(args);

            translator.Received().Translate("EnterFolderPath");
            console.Received().Write("foo");
        }

        [Test]
        public void Run_ShouldCloneWithConsoleInputForMissingTargetPath()
        {
            var arguments = new Dictionary<string, string> { { "clone", "true" }, { "source", "foo" } };
            argumentParser.ParseCommandLineArguments(args).Returns(arguments);

            var result = routine.Run(args);

            Assert.That(result, Is.True);
            console.Received().ReadLine();
            solutionCloner.Received().CloneSolution("foo", "val");
        }
        
        [Test]
        public void Run_ShouldCloneWithProvidedSourceAndTargetPaths()
        {
            var arguments = new Dictionary<string, string> { { "clone", "true" }, { "source", "foo" }, { "target", "bar" } };
            argumentParser.ParseCommandLineArguments(args).Returns(arguments);

            var result = routine.Run(args);

            Assert.That(result, Is.True);
            translator.DidNotReceive().Translate(Arg.Any<string>());
            console.DidNotReceive().WriteLine(Arg.Any<string>());
            solutionCloner.Received().CloneSolution("foo", "bar");
        }
    }
}