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
        private ISubRoutine routine;

        private ISolutionCloner solutionCloner;
        private ICommandLineArgumentParser argumentParser;
        private IConsole console;
        private ITranslator translator;


        [SetUp]
        public void SetUp()
        {
            solutionCloner = Substitute.For<ISolutionCloner>();
            argumentParser = Substitute.For<ICommandLineArgumentParser>();
            console = Substitute.For<IConsole>();
            console.ReadLine().Returns("val");
            translator = Substitute.For<ITranslator>();
            translator.Translate("EnterFolderPath").Returns("foo");

            routine = new CloneRoutine(solutionCloner, argumentParser, console, translator);
        }


        [Test]
        public void Run_ShouldAbortForNoArguments()
        {
            var result = routine.Run(new string[0]);

            Assert.That(result, Is.False);
            translator.DidNotReceive().Translate(Arg.Any<string>());
            solutionCloner.DidNotReceive().CloneSolution(Arg.Any<string>(), Arg.Any<string>());
        }
        
        [Test]
        public void Run_ShouldAbortForIrrelevantArguments()
        {
            var result = routine.Run(new []{"--foo=bar"});

            Assert.That(result, Is.False);
            translator.DidNotReceive().Translate(Arg.Any<string>());
            solutionCloner.DidNotReceive().CloneSolution(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldRequireConsoleInputForMissingSourcePath()
        {
            routine.Run(new[] { "--clone", "--target=bar" });

            translator.Received().Translate("EnterFolderPath");
            console.Received().Write("foo");
        }

        [Test]
        public void Run_ShouldCloneWithConsoleInputForMissingSourcePath()
        {
            var result = routine.Run(new[] { "--clone", "--target=bar" });

            Assert.That(result, Is.True);
            console.Received().ReadLine();
            solutionCloner.Received().CloneSolution("val", "bar");
        }

        [Test]
        public void Run_ShouldRequireConsoleInputForMissingTargetPath()
        {
            routine.Run(new[] { "--clone", "--source=foo" });

            translator.Received().Translate("EnterFolderPath");
            console.Received().Write("foo");
        }

        [Test]
        public void Run_ShouldCloneWithConsoleInputForMissingTargetPath()
        {
            var result = routine.Run(new[] { "--clone", "--source=foo" });

            Assert.That(result, Is.True);
            console.Received().ReadLine();
            solutionCloner.Received().CloneSolution("foo", "val");
        }
        
        [Test]
        public void Run_ShouldCloneWithProvidedSourceAndTargetPaths()
        {
            var result = routine.Run(new[] { "--clone", "--source=foo", "--target=bar" });

            Assert.That(result, Is.True);
            translator.DidNotReceive().Translate(Arg.Any<string>());
            console.DidNotReceive().WriteLine(Arg.Any<string>());
            solutionCloner.Received().CloneSolution("foo", "bar");
        }
    }
}