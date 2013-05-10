using System.Collections.Generic;
using System.IO;
using Cloney.Core.Console;
using Cloney.Core.ContextMenu;
using Cloney.Core.Localization;
using Cloney.Core.SubRoutines;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests.SubRoutines
{
    [TestFixture]
    public class InstallContextMenuRoutineBehavior
    {
        private ISubRoutine routine;

        private IEnumerable<string> args;
        private IConsole console;
        private ITranslator translator;
        private IContextMenuInstaller installer;
        private ICommandLineArgumentParser commandLineArgumentParser;


        
        [SetUp]
        public void SetUp()
        {
            args = new[] { "foo" };
            console = Substitute.For<IConsole>();
            translator = Substitute.For<ITranslator>();
            installer = Substitute.For<IContextMenuInstaller>();
            translator.Translate(Arg.Any<string>()).Returns(x => x[0]);
            commandLineArgumentParser = Substitute.For<ICommandLineArgumentParser>();
            commandLineArgumentParser.ParseCommandLineArguments(args).Returns(new Dictionary<string, string> { { "install", "true" } });

            routine = new InstallContextMenuRoutine(console, translator, installer, commandLineArgumentParser);
        }


        [Test]
        public void Run_ShouldParseIEnumerableToDictionary()
        {
            routine.Run(args);

            commandLineArgumentParser.Received().ParseCommandLineArguments(args);
        }

        [Test]
        public void Run_ShouldAbortForNoArguments()
        {
            commandLineArgumentParser.ParseCommandLineArguments(args).Returns(new Dictionary<string, string>());
            var result = routine.Run(args);

            Assert.That(result, Is.False);
            console.DidNotReceive().WriteLine(Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldAbortForMoreThanOneArgument()
        {
            commandLineArgumentParser.ParseCommandLineArguments(args).Returns(new Dictionary<string, string> { { "install", "true" }, { "foo", "bar" } });
            var result = routine.Run(args);

            Assert.That(result, Is.False);
            console.DidNotReceive().WriteLine(Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldAbortForIrrelevantArgument()
        {
            commandLineArgumentParser.ParseCommandLineArguments(args).Returns(new Dictionary<string, string> { { "foo", "bar" } });
            var result = routine.Run(args);

            Assert.That(result, Is.False);
            console.DidNotReceive().WriteLine(Arg.Any<string>());
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

            translator.Received().Translate("InstallSuccessMessage");
            console.Received().WriteLine("InstallSuccessMessage");
        }

        [Test]
        public void Run_ShouldRunInstallerForRelevantArgument()
        {
            routine.Run(args);

            installer.Received().RegisterContextMenu(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldRunInstallerWithWizardArgumentForRelevantArgument()
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

            translator.Received().Translate("InstallErrorMessage");
            console.Received().WriteLine("InstallErrorMessage");
            console.Received().WriteLine(Arg.Is<string>(x => x.Contains(exceptionMessage)));
        }
    }
}