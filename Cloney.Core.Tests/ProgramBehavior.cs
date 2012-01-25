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
        private IProgram consoleApplication;
        private IProgram guiApplication;
        private ITranslator translator;
 

        [SetUp]
        public void SetUp()
        {
            console = Substitute.For<IConsole>();
            consoleApplication = Substitute.For<IProgram>();
            guiApplication = Substitute.For<IProgram>();
            translator = Substitute.For<ITranslator>();

            program = new Program(console, consoleApplication, guiApplication, translator);
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
            program = new Program(console, null, guiApplication, translator);

            program.Start(args);

            translator.Received().Translate("StartErrorMessage");
            console.Received().WriteLine("StartErrorMessage");
            console.Received().WriteLine("Object reference not set to an instance of an object.");
        }

        [Test]
        public void Start_ShouldNotStartGuiApplicationIfConsoleApplicationStarts()
        {
            consoleApplication.Start(args).Returns(true);

            var result = program.Start(args);

            consoleApplication.Received().Start(args);
            guiApplication.DidNotReceive().Start(args);
            Assert.That(result, Is.True);
        }

        [Test]
        public void Start_ShouldStartGuiApplicationIfConsoleApplicationDoesNotStart()
        {
            consoleApplication.Start(args).Returns(false);
            guiApplication.Start(args).Returns(true);

            var result = program.Start(args);

            consoleApplication.Received().Start(args);
            guiApplication.Received().Start(args);
            Assert.That(result, Is.True);
        }

        [Test]
        public void Start_ShouldReturnFalseIfNoApplicationStarted()
        {
            var result = program.Start(args);

            consoleApplication.Received().Start(args);
            guiApplication.Received().Start(args);
            Assert.That(result, Is.False);
        }
    }
}