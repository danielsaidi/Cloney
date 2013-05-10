using System.Collections.Generic;
using Cloney.Core.SubRoutines;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests.SubRoutines
{
    [TestFixture]
    public class SettingsRoutineBehavior : SubRoutineTestBase
    {
        private ISubRoutine routine;


        [SetUp]
        public void SetUp()
        {
            var validArgs = new Dictionary<string, string> {{"settings", "true"}};
            ArgumentParser.ParseCommandLineArguments(args).Returns(validArgs);

            Translator.Translate("SettingsMessage").Returns("{0}{1}{2}");

            routine = new SettingsRoutine(ArgumentParser, Console, Translator);
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

            AssertSubRoutineStopped(result);
        }

        [Test]
        public void Run_ShouldAbortForMoreThanOneArgument()
        {
            var tooManyArgs = new Dictionary<string, string> {{"settings", "true"}, {"foo", "bar"}};
            ArgumentParser.ParseCommandLineArguments(args).Returns(tooManyArgs);
            var result = routine.Run(args);

            AssertSubRoutineStopped(result);
        }

        [Test]
        public void Run_ShouldAbortForIrrelevantArguments()
        {
            var irrelevantArgs = new Dictionary<string, string> {{"settings", "true"}, {"foo", "bar"}};
            ArgumentParser.ParseCommandLineArguments(args).Returns(irrelevantArgs);
            var result = routine.Run(args);

            AssertSubRoutineStopped(result);
        }

        [Test]
        public void Run_ShouldProceedForRelevantArgument()
        {
            var result = routine.Run(args);

            Assert.That(result, Is.True);
        }

        [Test]
        public void Run_ShouldWriteToConsoleForRelevantArgument()
        {
            routine.Run(args);

            var settingsMessage = Translator.Translate("SettingsMessage");
            var excludeFolderMessage = string.Join(", ", Default.ExcludeFolderPatterns);
            var excludeFileMessage = string.Join(", ", Default.ExcludeFilePatterns);
            var plainCopyFileMessage = string.Join(", ", Default.PlainCopyFilePatterns);
            var message = string.Format(settingsMessage, excludeFolderMessage, excludeFileMessage, plainCopyFileMessage);

            Translator.Received().Translate("SettingsMessage");
            Console.Received().WriteLine(message);
        }


        private void AssertSubRoutineStopped(bool result)
        {
            Assert.That(result, Is.False);
            Translator.DidNotReceive().Translate(Arg.Any<string>());
            Console.DidNotReceive().WriteLine(Arg.Any<string>());
        }
    }
}