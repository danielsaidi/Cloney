using System.Collections.Generic;
using Cloney.Core.SubRoutines;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests.SubRoutines
{
    [TestFixture]
    public class HelpRoutineBehavior : SubRoutineTestBase
    {
        private ISubRoutine routine;


        [SetUp]
        public void SetUp()
        {
            var validArgs = new Dictionary<string, string> {{"help", "true"}};
            ArgumentParser.ParseCommandLineArguments(args).Returns(validArgs);

            routine = new HelpRoutine(ArgumentParser, Console, Translator);
        }


        [Test]
        public void Run_ShouldParseIEnumerableToDictionary()
        {
            routine.Run(args);

            ArgumentParser.Received().ParseCommandLineArguments(args);
        }

        [Test]
        public void Run_ShouldAbortForNoArguments()
        {
            ArgumentParser.ParseCommandLineArguments(args).Returns(new Dictionary<string, string>());
            var result = routine.Run(args);

            Assert.That(result, Is.False);
            Console.DidNotReceive().WriteLine(Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldAbortForMoreThanOneArgument()
        {
            ArgumentParser.ParseCommandLineArguments(args).Returns(new Dictionary<string, string> { { "help", "true" }, { "foo", "bar" } });
            var result = routine.Run(args);

            Assert.That(result, Is.False);
            Console.DidNotReceive().WriteLine(Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldAbortForIrrelevantArgument()
        {
            ArgumentParser.ParseCommandLineArguments(args).Returns(new Dictionary<string, string> { { "foo", "bar" } });
            var result = routine.Run(args);

            Assert.That(result, Is.False);
            Console.DidNotReceive().WriteLine(Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldProceedForRelevantArgument()
        {
            var result = routine.Run(args);

            Assert.That(result, Is.True);
        }

        [Test]
        public void Run_ShouldDisplayTranslatedErrorMessageForRelevantArgument()
        {
            routine.Run(args);

            Translator.Received().Translate("GeneralHelpMessage");
            Console.Received().WriteLine("GeneralHelpMessage");
        }
    }
}