using System.Collections.Generic;
using System.IO;
using Cloney.Core.ContextMenu;
using Cloney.Core.SubRoutines;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests.SubRoutines
{
    [TestFixture]
    public class InstallContextMenuRoutineBehavior : SubRoutineTestBase
    {
        private ISubRoutine routine;
        private IContextMenuInstaller installer;

        
        [SetUp]
        public void SetUp()
        {
            var validArgs = new Dictionary<string, string> {{"install", "true"}};
            ArgumentParser.ParseCommandLineArguments(args).Returns(GetArgs(validArgs));

            installer = Substitute.For<IContextMenuInstaller>();
            routine = new InstallContextMenuRoutine(installer, ArgumentParser, Console, Translator);
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
            ArgumentParser.ParseCommandLineArguments(args).Returns(GetArgs(emptyArgs));
            var result = routine.Run(args);

            Assert.That(result, Is.False);
            Console.DidNotReceive().WriteLine(Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldAbortForMoreThanOneArgument()
        {
            var tooManyArgs = new Dictionary<string, string> {{"install", "true"}, {"foo", "bar"}};
            ArgumentParser.ParseCommandLineArguments(args).Returns(GetArgs(tooManyArgs));
            var result = routine.Run(args);

            Assert.That(result, Is.False);
            Console.DidNotReceive().WriteLine(Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldAbortForIrrelevantArgument()
        {
            var irrelevantArgs = new Dictionary<string, string> {{"foo", "bar"}};
            ArgumentParser.ParseCommandLineArguments(args).Returns(GetArgs(irrelevantArgs));
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
        public void Run_ShouldDisplayInstallMessageForRelevantArgument()
        {
            routine.Run(args);

            Translator.Received().Translate("InstallSuccessMessage");
            Console.Received().WriteLine("InstallSuccessMessage");
        }

        [Test]
        public void Run_ShouldRunInstallerForRelevantArgument()
        {
            routine.Run(args);

            installer.Received().RegisterContextMenu(Arg.Is<string>(x => x.Contains(@"Cloney.Wizard.exe")), "ContextMenuText");
        }

        [Test]
        public void Run_ShouldPrintFriendlyFailMessageWhenInstallationFails()
        {
            const string exceptionMessage = "Something exceptional occurred";
            installer.When(x => x.RegisterContextMenu(Arg.Any<string>(), Arg.Any<string>()))
                .Do(x => { throw new FileNotFoundException(exceptionMessage);});

            routine.Run(args);

            Translator.Received().Translate("InstallErrorMessage");
            Console.Received().WriteLine("InstallErrorMessage");
            Console.Received().WriteLine(Arg.Is<string>(x => x.Contains(exceptionMessage)));
        }
    }
}