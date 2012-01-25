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

            args = new List<string>{ "foo", "bar" };
        }


        [Test]
        public void Start_ShouldFailWithPrettyErrorMessage()
        {
            program.Translator.Translate("StartErrorMessage").Returns("Foo bar");

            program.Start(args);

            program.Translator.Received().Translate("StartErrorMessage");
            program.Console.Received().WriteLine("Foo bar");
            program.Console.Received().WriteLine("foo");
        }
    }
}