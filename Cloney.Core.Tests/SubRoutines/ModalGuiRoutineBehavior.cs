using Cloney.Core.Console;
using Cloney.Core.Diagnostics;
using Cloney.Core.Localization;
using Cloney.Core.SubRoutines;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests.SubRoutines
{
    [TestFixture]
    public class ModalGuiRoutineBehavior
    {
        private ISubRoutine routine;

        private IProcess process;
        private ICommandLineArgumentParser argumentParser;
        private IConsole console;
        private ITranslator translator;


        [SetUp]
        public void SetUp()
        {
            process = Substitute.For<IProcess>();
            argumentParser = Substitute.For<ICommandLineArgumentParser>();
            console = Substitute.For<IConsole>();
            console.ReadLine().Returns("val");
            translator = Substitute.For<ITranslator>();
            translator.Translate("EnterFolderPath").Returns("foo");

            routine = new ModalGuiRoutine(process, argumentParser, console, translator);
        }


        [Test]
        public void Start_ShouldNotTriggerForNoArguments()
        {
            var result = routine.Run(new string[]{});

            Assert.That(result, Is.False);
            process.DidNotReceive().Start(Arg.Any<string>(), Arg.Any<string>());
        }
        [Test]
        public void Start_ShouldNotTriggerForSeveralArguments()
        {
            var result = routine.Run(new[] { "--modal", "--foo" });

            Assert.That(result, Is.False);
            process.DidNotReceive().Start(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void Start_ShouldLaunchExternalProgramForSingleRelevantArgument()
        {
            var result = routine.Run(new[] { "--modal" });

            Assert.That(result, Is.True);
            process.Received().Start("Cloney.Wizard.exe", "--modal");
        }
    }
}