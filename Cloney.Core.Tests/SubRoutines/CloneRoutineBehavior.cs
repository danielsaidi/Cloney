using System.Collections.Generic;
using Cloney.Core.Cloners;
using Cloney.Core.SubRoutines;
using NExtra;
using NExtra.Localization;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests.SubRoutines
{
    [TestFixture]
    public class CloneRoutineBehavior
    {
        private ISubRoutine routine;
        private IConsole console;
        private ITranslator translator;
        private ISolutionCloner solutionCloner;


        [SetUp]
        public void SetUp()
        {
            console = Substitute.For<IConsole>();
            console.ReadLine().Returns("input value");

            translator = Substitute.For<ITranslator>();
            translator.Translate("EnterFolderPath").Returns("{0}");

            solutionCloner = Substitute.For<ISolutionCloner>();

            routine = new CloneRoutine(console, translator, solutionCloner);
        }


        [Test]
        public void Run_ShouldAbortForNoArguments()
        {
            var result = routine.Run(new Dictionary<string, string>());

            Assert.That(result, Is.False);
            solutionCloner.DidNotReceive().CloneSolution(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldAbortForIrrelevantArguments()
        {
            var result = routine.Run(new Dictionary<string, string> { { "foo", "bar" } });

            Assert.That(result, Is.False);
            solutionCloner.DidNotReceive().CloneSolution(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldRequireConsoleInputForMissingSourcePath()
        {
            routine.Run(new Dictionary<string, string> { { "clone", "true" }, { "target", "bar" } });

            translator.Received().Translate("EnterFolderPath");
            console.Received().Write("source");
        }

        [Test]
        public void Run_ShouldCloneWithConsoleInputForMissingSourcePath()
        {
            var result = routine.Run(new Dictionary<string, string> { { "clone", "true" }, { "target", "bar" } });

            Assert.That(result, Is.True);
            console.Received().ReadLine();
            solutionCloner.Received().CloneSolution("input value", "bar");
        }

        [Test]
        public void Run_ShouldRequireConsoleInputForMissingTargetPath()
        {
            routine.Run(new Dictionary<string, string> { { "clone", "true" }, { "source", "foo" } });

            translator.Received().Translate("EnterFolderPath");
            console.Received().Write("target");
        }

        [Test]
        public void Run_ShouldCloneWithConsoleInputForMissingTargetPath()
        {
            var result = routine.Run(new Dictionary<string, string> { { "clone", "true" }, { "source", "foo" } });

            Assert.That(result, Is.True);
            console.Received().ReadLine();
            solutionCloner.Received().CloneSolution("foo", "input value");
        }

        [Test]
        public void Run_ShouldStartCloningOperationWithProvidedFolders()
        {
            var result = routine.Run(new Dictionary<string, string> { { "clone", "true" }, { "source", "foo" }, { "target", "bar" } });

            Assert.That(result, Is.True);
            translator.DidNotReceive().Translate(Arg.Any<string>());
            console.DidNotReceive().WriteLine(Arg.Any<string>());
            solutionCloner.Received().CloneSolution("foo", "bar");
        }
    }
}