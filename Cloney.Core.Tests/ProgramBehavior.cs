using System.Collections.Generic;
using NExtra;
using NExtra.Localization;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests
{
    [TestFixture]
    public class ProgramBehavior
    {
        private Program program;
        private List<string> args;

        private IConsole console;
        private IProgram consoleProgram;
        private IProgram guiProgram;
        private ITranslator translator;
 

        [SetUp]
        public void SetUp()
        {
            console = Substitute.For<IConsole>();
            consoleProgram = Substitute.For<IProgram>();
            guiProgram = Substitute.For<IProgram>();
            translator = Substitute.For<ITranslator>();

            program = new Program(console, consoleProgram, guiProgram, translator);
            args = new List<string>{ "foo", "bar" };

            SetUpLanguage();
        }

        private void SetUpLanguage()
        {
            translator.Translate("StartErrorMessage").Returns("StartErrorMessage");
        }


        [Test]
        public void Start_ShouldFailWithPrettyErrorMessage()
        {
            program = new Program(console, null, guiProgram, translator);

            program.Start(args);

            translator.Received().Translate("StartErrorMessage");
            console.Received().WriteLine("StartErrorMessage");
            console.Received().WriteLine("Object reference not set to an instance of an object.");
        }

        [Test]
        public void Start_ShouldNotStartGuiApplicationIfConsoleApplicationStarts()
        {
            consoleProgram.Start(args).Returns(true);

            var result = program.Start(args);

            consoleProgram.Received().Start(args);
            guiProgram.DidNotReceive().Start(args);
            Assert.That(result, Is.True);
        }

        [Test]
        public void Start_ShouldStartGuiApplicationIfConsoleApplicationDoesNotStart()
        {
            consoleProgram.Start(args).Returns(false);
            guiProgram.Start(args).Returns(true);

            var result = program.Start(args);

            consoleProgram.Received().Start(args);
            guiProgram.Received().Start(args);
            Assert.That(result, Is.True);
        }

        [Test]
        public void Start_ShouldReturnFalseIfNoApplicationStarted()
        {
            var result = program.Start(args);

            consoleProgram.Received().Start(args);
            guiProgram.Received().Start(args);
            Assert.That(result, Is.False);
        }
    }
}