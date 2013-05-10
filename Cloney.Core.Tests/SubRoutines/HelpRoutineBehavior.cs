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
            var emptyArgs = new Dictionary<string, string>();
            ArgumentParser.ParseCommandLineArguments(args).Returns(emptyArgs);
            var result = routine.Run(args);

            Assert.That(result, Is.False);
            Console.DidNotReceive().WriteLine(Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldAbortForMoreThanOneArgument()
        {
            var tooManyArgs = new Dictionary<string, string> {{"help", "true"}, {"foo", "bar"}};
            ArgumentParser.ParseCommandLineArguments(args).Returns(tooManyArgs);
            var result = routine.Run(args);

            Assert.That(result, Is.False);
            Console.DidNotReceive().WriteLine(Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldAbortForIrrelevantArgument()
        {
            var irrelevantArgs = new Dictionary<string, string> {{"foo", "bar"}};
            ArgumentParser.ParseCommandLineArguments(args).Returns(irrelevantArgs);
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
        public void Run_ShouldWriteToConsoleRelevantArgument()
        {
            routine.Run(args);

            Translator.Received().Translate("GeneralHelpMessage");
            Console.Received().WriteLine("GeneralHelpMessage");
        }
    }
}