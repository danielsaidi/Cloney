using Cloney.Core.Console;
using Cloney.Core.Localization;
using Cloney.Core.SubRoutines;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests.SubRoutines
{
    [TestFixture]
    public class SettingsRoutineBehavior
    {
        private ISubRoutine routine;

        private ICommandLineArgumentParser argumentParser;
        private IConsole console;
        private ITranslator translator;


        [SetUp]
        public void SetUp()
        {
            console = Substitute.For<IConsole>();
            argumentParser = Substitute.For<ICommandLineArgumentParser>();
            translator = Substitute.For<ITranslator>();
            translator.Translate("SettingsMessage").Returns("{0}{1}{2}");

            routine = new SettingsRoutine(argumentParser, console, translator);
        }


        [Test]
        public void Run_ShouldAbortForNoArguments()
        {
            var result = routine.Run(new string[]{});

            Assert.That(result, Is.False);
            console.DidNotReceive().WriteLine(Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldAbortForMoreThanOneArgument()
        {
            var result = routine.Run(new[] { "--settings", "-foo=bar"});

            Assert.That(result, Is.False);
            console.DidNotReceive().WriteLine(Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldAbortForIrrelevantArguments()
        {
            var result = routine.Run(new[] { "-foo=bar" });

            Assert.That(result, Is.False);
            console.DidNotReceive().WriteLine(Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldProceedForRelevantArgument()
        {
            var result = routine.Run(new[] { "--settings" });

            var settingsMessage = translator.Translate("SettingsMessage");
            var excludeFolderMessage = string.Join(", ", Default.ExcludeFolderPatterns);
            var excludeFileMessage = string.Join(", ", Default.ExcludeFilePatterns);
            var plainCopyFileMessage = string.Join(", ", Default.PlainCopyFilePatterns);
            var message = string.Format(settingsMessage, excludeFolderMessage, excludeFileMessage, plainCopyFileMessage);
            
            Assert.That(result, Is.True);
            translator.Received().Translate("SettingsMessage");
            console.Received().WriteLine(message);
        }
    }
}