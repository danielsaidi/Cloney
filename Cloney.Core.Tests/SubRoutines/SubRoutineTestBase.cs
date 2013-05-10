using System.Collections.Generic;
using Cloney.Core.Console;
using Cloney.Core.Localization;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests.SubRoutines
{
    [TestFixture]
    public class SubRoutineTestBase
    {
        protected IEnumerable<string> args;


        [SetUp]
        public void MasterSetUp()
        {
            args = new[] { "foo" };

            ArgumentParser = Substitute.For<ICommandLineArgumentParser>();
            Console = Substitute.For<IConsole>();
            Translator = Substitute.For<ITranslator>();
            Translator.Translate(Arg.Any<string>()).Returns(x => x[0]);
        }


        protected ICommandLineArgumentParser ArgumentParser { get; private set; }

        protected IConsole Console { get; private set; }

        protected ITranslator Translator { get; private set; }


        protected CommandLineArguments GetArgs(IDictionary<string, string> rawArgs)
        {
            return new CommandLineArguments(rawArgs);
        }
    }
}
