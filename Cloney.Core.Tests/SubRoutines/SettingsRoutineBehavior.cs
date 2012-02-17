using System.Collections.Generic;
using Cloney.Core;
using Cloney.Core.SubRoutines;
using NExtra;
using NExtra.Localization;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Tests.SubRoutines
{
    [TestFixture]
    public class SettingsRoutineBehavior
    {
        private ISubRoutine routine;
        private IConsole console;
        private ITranslator translator;


        [SetUp]
        public void SetUp()
        {
            console = Substitute.For<IConsole>();
            translator = Substitute.For<ITranslator>();
            translator.Translate("SettingsMessage").Returns("{0}{1}{2}");

            routine = new SettingsRoutine(console, translator);
        }


        [Test]
        public void Run_ShouldAbortForNoArguments()
        {
            var result = routine.Run(new Dictionary<string, string>());

            Assert.That(result, Is.False);
            console.DidNotReceive().WriteLine(Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldAbortForMoreThanOneArgument()
        {
            var result = routine.Run(new Dictionary<string, string> { { "settings", "true" }, { "foo", "bar" } });

            Assert.That(result, Is.False);
            console.DidNotReceive().WriteLine(Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldAbortForIrrelevantArguments()
        {
            var result = routine.Run(new Dictionary<string, string> { { "foo", "bar" } });

            Assert.That(result, Is.False);
            console.DidNotReceive().WriteLine(Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldProceedForRelevantArgument()
        {
            var result = routine.Run(new Dictionary<string, string> { { "settings", "true" } });

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