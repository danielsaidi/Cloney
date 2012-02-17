using System.Collections.Generic;
using Cloney.Core.SubRoutines;
using NExtra;
using NExtra.Diagnostics;
using NExtra.Localization;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Tests.SubRoutines
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
        public void Start_ShouldLaunchExternalProgramForNoArguments()
        {
            var args = new Dictionary<string, string>();

            var result = routine.Run(args);

            Assert.That(result, Is.True);
            process.Received().Start("Cloney.Wizard.exe");
        }

        [Test]
        public void Start_ShouldDisplayLaunchMessageForNoArguments()
        {
            var args = new Dictionary<string, string>();

            var result = routine.Run(args);

            Assert.That(result, Is.True);
            translator.Received().Translate("GuiStartMessage");
            console.Received().WriteLine("message");
        }

        [Test]
        public void Start_ShouldNotLaunchExternalProgramForArguments()
        {
            var args = new Dictionary<string, string> {{"foo", "bar"}};
            
            var result = routine.Run(args);

            Assert.That(result, Is.False);
            process.DidNotReceive().Start(Arg.Any<string>());
        }
    }
}