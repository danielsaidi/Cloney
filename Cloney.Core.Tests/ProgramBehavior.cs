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
            program.Translator = Substitute.For<ITranslator>();
            program.Wizard = Substitute.For<IWizard>();

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
            

            program.Start(args);

            program.Translator.Received().Translate("StartErrorMessage");
            program.Console.Received().WriteLine("StartErrorMessage");
            program.Console.Received().WriteLine("foo");
        }

        [Test]
        public void Start_ShouldStartWizardWhenNoArgumentsAreProvided()
        {
            program.Start(new List<string>());

            program.Wizard.Received().Start();
        }
    }
}