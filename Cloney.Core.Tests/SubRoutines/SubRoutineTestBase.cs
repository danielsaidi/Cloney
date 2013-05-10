using System.Collections.Generic;
using Cloney.Core.Console;
using Cloney.Core.Localization;
using NSubstitute;

namespace Cloney.Core.Tests.SubRoutines
{
    public class SubRoutineTestBase
    {
        protected readonly IEnumerable<string> args;


        public SubRoutineTestBase()
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
    }
}
