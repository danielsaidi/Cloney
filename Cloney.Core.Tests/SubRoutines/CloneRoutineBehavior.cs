using System.Collections.Generic;
using Cloney.Core.SolutionCloners;
using Cloney.Core.SubRoutines;
using Cloney.Core.Tests.SolutionCloners;
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
            translator = Substitute.For<ITranslator>();
            translator.Translate("MissingSourcePathArgumentErrorMessage").Returns("foo");
            translator.Translate("MissingTargetPathArgumentErrorMessage").Returns("bar");
            solutionCloner = Substitute.For<ISolutionCloner>();

            routine = new CloneRoutine(console, translator, solutionCloner);
        }


        [Test]
        public void Run_ShouldAbortForNoArguments()
        {
            routine.Run(new Dictionary<string, string>());

            solutionCloner.DidNotReceive().CloneSolution(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldAbortForIrrelevantArguments()
        {
            routine.Run(new Dictionary<string, string> { { "foo", "bar" } });

            solutionCloner.DidNotReceive().CloneSolution(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldWarnForMissingSourcePath()
        {
            routine.Run(new Dictionary<string, string> { { "clone", "true" } });

            translator.Received().Translate("MissingSourcePathArgumentErrorMessage");
            console.Received().WriteLine("foo");
        }

        [Test]
        public void Run_ShouldWarnForMissingTargetPath()
        {
            routine.Run(new Dictionary<string, string> { { "clone", "true" }, { "source", "c:\\source" } });

            translator.Received().Translate("MissingTargetPathArgumentErrorMessage");
            console.Received().WriteLine("bar");
        }

        [Test]
        public void Run_ShouldStartCloningOperationForValidArguments()
        {
            routine.Run(new Dictionary<string, string> { { "clone", "true" }, { "source", "c:\\source" }, { "target", "c:\\target" } });

            translator.DidNotReceive().Translate(Arg.Any<string>());
            console.DidNotReceive().WriteLine(Arg.Any<string>());
            solutionCloner.Received().CloneSolution("c:\\source", "c:\\target");
        }

        [Test]
        public void Run_ShouldFinishWhenCloningOperationEnds()
        {
            solutionCloner = new FakeSolutionCloner();
            routine = new CloneRoutine(console, translator, solutionCloner);

            routine.Run(new Dictionary<string, string> { { "clone", "true" }, { "source", "c:\\source" }, { "target", "c:\\target" } });
        }
    }
}