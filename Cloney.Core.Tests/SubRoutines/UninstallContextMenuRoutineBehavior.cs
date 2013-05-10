using System.Collections.Generic;
using System.IO;
using Cloney.Core.ContextMenu;
using Cloney.Core.SubRoutines;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests.SubRoutines
{
    [TestFixture]
    public class UninstallContextMenuRoutineBehavior : SubRoutineTestBase
    {
        private ISubRoutine routine;
        private IContextMenuInstaller uninstaller;


        [SetUp]
        public void SetUp()
        {
            var validArgs = new Dictionary<string, string> {{"uninstall", "true"}};
            ArgumentParser.ParseCommandLineArguments(args).Returns(validArgs);
            
            uninstaller = Substitute.For<IContextMenuInstaller>();
            routine = new UninstallContextMenuRoutine(uninstaller, ArgumentParser, Console, Translator);
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
            var tooManyArgs = new Dictionary<string, string> {{"uninstall", "true"}, {"foo", "bar"}};
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
        public void Run_ShouldDisplayUninstallMessageForRelevantArgument()
        {
            routine.Run(args);

            Translator.Received().Translate("UninstallSuccessMessage");
            Console.Received().WriteLine("UninstallSuccessMessage");
        }

        [Test]
        public void Run_ShouldRunUninstallerForRelevantArgument()
        {
            routine.Run(args);

            uninstaller.Received().UnregisterContextMenu();
        }

        [Test]
        public void Run_ShouldPrintFriendlyFailMessageWhenUninstallationFails()
        {
            const string exceptionMessage = "Something exceptional occurred";
            uninstaller.When(x => x.UnregisterContextMenu())
                .Do(x => { throw new FileNotFoundException(exceptionMessage); });

            routine.Run(args);

            Translator.Received().Translate("UninstallErrorMessage");
            Console.Received().WriteLine("UninstallErrorMessage");
            Console.Received().WriteLine(Arg.Is<string>(x => x.Contains(exceptionMessage)));
        }
    }
}