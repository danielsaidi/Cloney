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
        private ISubRoutine routine;
        private IConsole console;
        private ITranslator translator;


        [SetUp]
        public void SetUp()
        {
            console = Substitute.For<IConsole>();
            translator = Substitute.For<ITranslator>();
            translator.Translate("GeneralHelpMessage").Returns("foo");

            routine = new HelpRoutine(console, translator);
        }


        [Test]
        public void Run_ShouldAbortForNoArguments()
        {
            var result = routine.Run(new Dictionary<string, string>());

            Assert.That(result, Is.False);
            console.DidNotReceive().WriteLine(Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldAbortForMoreThanOneArgument()
        {
            var result = routine.Run(new Dictionary<string, string> { { "help", "true" }, { "foo", "bar" } });

            Assert.That(result, Is.False);
            console.DidNotReceive().WriteLine(Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldAbortForIrrelevantArguments()
        {
            var result = routine.Run(new Dictionary<string, string> { { "foo", "bar" } });

            Assert.That(result, Is.False);
            console.DidNotReceive().WriteLine(Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldProceedForRelevantArgument()
        {
            var result = routine.Run(new Dictionary<string, string> { { "help", "true" } });

            Assert.That(result, Is.True);
            translator.Received().Translate("GeneralHelpMessage");
            console.Received().WriteLine("foo");
        }
    }
}