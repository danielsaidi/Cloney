using System.Collections.Generic;
using Cloney.Core.Diagnostics;
using Cloney.Core.SubRoutines;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests.SubRoutines
{
    [TestFixture]
    public class ModalGuiRoutineBehavior : SubRoutineTestBase
    {
        private ISubRoutine routine;
        private IProcess process;


        [SetUp]
        public void SetUp()
        {
            var validArgs = new Dictionary<string, string> {{"modal", "true"}};
            ArgumentParser.ParseCommandLineArguments(args).Returns(GetArgs(validArgs));

            process = Substitute.For<IProcess>();
            routine = new ModalGuiRoutine(process, ArgumentParser, Console, Translator);
        }


        [Test]
        public void Run_ShouldParseIEnumerableToDictionary()
        {
            routine.Run(args);

            ArgumentParser.Received().ParseCommandLineArguments(args);
        }

        [Test]
        public void Run_ShouldAbortForNoArguments()
        {
            var emptyArgs = new Dictionary<string, string>();
            ArgumentParser.ParseCommandLineArguments(args).Returns(GetArgs(emptyArgs));
            var result = routine.Run(args);

            AssertSubRoutineStopped(result);
        }

        [Test]
        public void Run_ShouldAbortForMoreThanOneArgument()
        {
            var tooManyArgs = new Dictionary<string, string> {{"modal", "true"}, {"foo", "bar"}};
            ArgumentParser.ParseCommandLineArguments(args).Returns(GetArgs(tooManyArgs));
            var result = routine.Run(args);

            AssertSubRoutineStopped(result);
        }

        [Test]
        public void Run_ShouldAbortForIrrelevantArgument()
        {
            var irrelevantArgs = new Dictionary<string, string> { { "foo", "bar" } };
            ArgumentParser.ParseCommandLineArguments(args).Returns(GetArgs(irrelevantArgs));
            var result = routine.Run(args);

            AssertSubRoutineStopped(result);
        }

        [Test]
        public void Run_ShouldProceedForRelevantArgument()
        {
            var result = routine.Run(args);

            Assert.That(result, Is.True);
        }

        [Test]
        public void Run_ShouldWriteToConsoleForRelevantArgument()
        {
            routine.Run(args);

            Translator.Received().Translate("GuiModalStartMessage");
            Console.Received().WriteLine("GuiModalStartMessage");
        }

        [Test]
        public void Run_ShouldLaunchModalWizardForRelevantArgument()
        {
            routine.Run(args);

            process.Received().Start(Arg.Is<string>(x => x.Contains("Cloney.Wizard.exe")), "--modal");
        }


        private void AssertSubRoutineStopped(bool result)
        {
            Assert.That(result, Is.False);
            process.DidNotReceive().Start(Arg.Any<string>());
            Console.DidNotReceive().WriteLine(Arg.Any<string>());
        }
    }
}