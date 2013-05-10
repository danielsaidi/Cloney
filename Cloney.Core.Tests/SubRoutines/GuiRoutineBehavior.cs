using Cloney.Core.Diagnostics;
using Cloney.Core.SubRoutines;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests.SubRoutines
{
    [TestFixture]
    public class GuiRoutineBehavior : SubRoutineTestBase
    {
        private ISubRoutine routine;
        private IProcess process;


        [SetUp]
        public void SetUp()
        {
            process = Substitute.For<IProcess>();
            routine = new GuiRoutine(process, Console, Translator);
        }


        [Test]
        public void Run_ShouldReturnFalseForArguments()
        {
            var result = routine.Run(new[]{"foo"});

            Assert.That(result, Is.False);
        }

        [Test]
        public void Run_ShouldAbortForArguments()
        {
            routine.Run(new[] { "foo" });

            process.DidNotReceive().Start(Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldReturnTrueForNoArguments()
        {
            var result = routine.Run(new string[] { });

            Assert.That(result, Is.True);
        }

        [Test]
        public void Run_ShouldWriteToConsoleForNoArguments()
        {
            routine.Run(new string[] { });

            Translator.Received().Translate("GuiStartMessage");
            Console.Received().WriteLine("GuiStartMessage");
        }

        [Test]
        public void Run_ShouldLaunchWizardForNoArguments()
        {
            routine.Run(new string[] { });

            process.Received().Start(Arg.Is<string>(x => x.Contains("Cloney.Wizard.exe")));
        }
    }
}