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
 

        [SetUp]
        public void SetUp()
        {
            program = new Program();
            program.Console = Substitute.For<IConsole>();
            program.ConsoleApplication = Substitute.For<IProgram>();
            program.GuiApplication = Substitute.For<IProgram>();
            program.Translator = Substitute.For<ITranslator>();

            args = new List<string>{ "foo", "bar" };

            SetUpLanguage();
        }

        private void SetUpLanguage()
        {
            program.Translator.Translate("StartErrorMessage").Returns("StartErrorMessage");
        }


        [Test]
        public void Start_ShouldFailWithPrettyErrorMessage()
        {
            program.ConsoleApplication = null;

            program.Start(args);

            program.Translator.Received().Translate("StartErrorMessage");
            program.Console.Received().WriteLine("StartErrorMessage");
            program.Console.Received().WriteLine("Object reference not set to an instance of an object.");
        }

        [Test]
        public void Start_ShouldStartAllSubPrograms()
        {
            program.Start(args);

            program.ConsoleApplication.Received().Start(args);
            program.GuiApplication.Received().Start(args);
        }
    }
}