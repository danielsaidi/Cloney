using System.Collections.Generic;
using Cloney.Core.SolutionCloners;
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
            translator = Substitute.For<ITranslator>();
            solutionCloner = Substitute.For<ISolutionCloner>();

            routine = new CloneRoutine(console, translator, solutionCloner);
        }


        [Test]
        public void Run_ShouldAbortForNoArguments()
        {
            routine.Run(new Dictionary<string, string>());

            solutionCloner.DidNotReceive().CloneSolution(Arg.Any<string>(), Arg.Any<string>());

            Assert.That(routine.Finished, Is.True);
        }

        [Test]
        public void Run_ShouldAbortForIrrelevantArguments()
        {
            routine.Run(new Dictionary<string, string> { { "foo", "bar" } });

            solutionCloner.DidNotReceive().CloneSolution(Arg.Any<string>(), Arg.Any<string>());

            Assert.That(routine.Finished, Is.True);
        }
        /*
        [Test]
        public void Run_ShouldWarnForMissingSourcePath()
        {
            routine.Run(new Dictionary<string, string> { { "clone", "true" } });

            translator.Received().Translate("GeneralHelpMessage");
            console.Received().WriteLine("foo");

            Assert.That(routine.Finished, Is.True);
        }

        [Test]
        public void Run_ShouldProceedForRelevantArgument()
        {
            routine.Run(new Dictionary<string, string> { { "help", "true" } });

            translator.Received().Translate("GeneralHelpMessage");
            console.Received().WriteLine("foo");

            Assert.That(routine.Finished, Is.True);
        }*/
    }
}