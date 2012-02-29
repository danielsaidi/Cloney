using System.Collections.Generic;
using Cloney.Core.Console;
using Cloney.Core.Diagnostics;
using Cloney.Core.Localization;
using Cloney.Core.SubRoutines;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests.SubRoutines
{
    [TestFixture]
    public class GuiRoutineBehavior
    {
        private ISubRoutine routine;
        private IConsole console;
        private ITranslator translator;
        private IProcess process;


        [SetUp]
        public void SetUp()
        {
            console = Substitute.For<IConsole>();
            translator = Substitute.For<ITranslator>();
            translator.Translate("GuiStartMessage").Returns("message");
            process = Substitute.For<IProcess>();

            routine = new GuiRoutine(console, translator, process);
        }


        [Test]
        public void Run_ShouldAbortForArguments()
        {
            var args = new List<string>{"foo"};
            
            var result = routine.Run(args);

            Assert.That(result, Is.False);
            process.DidNotReceive().Start(Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldReturnTrueForNoArguments()
        {
            var result = routine.Run(new string[] { });

            Assert.That(result, Is.True);
        }

        [Test]
        public void Run_ShouldLaunchExternalProgramForNoArguments()
        {
            routine.Run(new string[] { });

            process.Received().Start("Cloney.Wizard.exe");
        }

        [Test]
        public void Run_ShouldDisplayTranslatedLaunchMessageForNoArguments()
        {
            routine.Run(new string[] { });

            translator.Received().Translate("GuiStartMessage");
            console.Received().WriteLine("message");
        }
    }
}