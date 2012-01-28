using System.Collections.Generic;
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


        [SetUp]
        public void SetUp()
        {
            console = Substitute.For<IConsole>();
            translator = Substitute.For<ITranslator>();
            translator.Translate("GeneralHelpMessage").Returns("foo");

            routine = new GeneralHelpRoutine(console, translator);
        }


        [Test]
        public void Run_ShouldAbortForNoArguments()
        {
            routine.Run(new Dictionary<string, string>());

            console.DidNotReceive().WriteLine(Arg.Any<string>());

            Assert.That(routine.Finished, Is.True);
        }

        [Test]
        public void Run_ShouldAbortForIrrelevantArguments()
        {
            routine.Run(new Dictionary<string, string> { { "foo", "bar" } });

            console.DidNotReceive().WriteLine(Arg.Any<string>());

            Assert.That(routine.Finished, Is.True);
        }

        [Test]
        public void Run_ShouldProceedForRelevantArgument()
        {
            routine.Run(new Dictionary<string, string> { { "help", "true" } });

            translator.Received().Translate("GeneralHelpMessage");
            console.Received().WriteLine("foo");

            Assert.That(routine.Finished, Is.True);
        }
    }
}