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
        private IEnumerable<string> args;
        private ISubRoutine routine;
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

            routine = new InstallContextMenuRoutine(console, translator, installer, commandLineArgumentParser);
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
            var result = routine.Run(new[]{ "--install", "--foo=bar" });

            Assert.That(result, Is.False);
            console.DidNotReceive().WriteLine(Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldAbortForIrrelevantArguments()
        {
            var result = routine.Run(new[] { "--foo=bar" });

            Assert.That(result, Is.False);
            console.DidNotReceive().WriteLine(Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldProceedForInstallArgument()
        {
            var result = routine.Run(new[] { "--install" });

            Assert.That(result, Is.True);
            translator.Received().Translate("InstallMessage");
            console.Received().WriteLine("InstallMessage");
        }

        [Test]
        public void Run_WithInstallArgument_ShouldRunContextMenuInstaller()
        {
            var result = routine.Run(new[] { "--install" });

            Assert.That(result, Is.True);
            installer.Received().RegisterContextMenu(Arg.Any<string>(), Arg.Any<string>());
            translator.Received().Translate("SuccessfulInstallMessage");
            console.Received().WriteLine(Arg.Is<string>(s => s.Contains("SuccessfulInstallMessage")));
        }

        [Test]
        public void Run_WithInstallArgument_ShouldPassInFilePathForConsoleExe()
        {
            var result = routine.Run(new[] { "--install" });

            Assert.That(result, Is.True);
            installer.Received().RegisterContextMenu(Arg.Is<string>(x => x.Contains(@"Cloney.Core.Tests\bin")), Arg.Any<string>());
        }

        [Test]
        public void Run_WithInstallArgument_ShouldPassInTranslationForMenuText()
        {
            var result = routine.Run(new[] { "--install" });

            Assert.That(result, Is.True);
            installer.Received().RegisterContextMenu(Arg.Any<string>(), Arg.Is<string>(x => x.Contains("ContextMenuText")));
            translator.Received().Translate("ContextMenuText");
        }

        [Test]
        public void Run_WhenInstallationFails_ShouldPrintFriendlyFailMessage()
        {
            const string exceptionMessage = "Something exceptional occurred";
            installer.When(x => x.RegisterContextMenu(Arg.Any<string>(), Arg.Any<string>()))
                .Do(x => { throw new FileNotFoundException(exceptionMessage);});

            routine.Run(new[] { "--install" });

            translator.Received().Translate("InstallerErrorMessage");
            console.Received().WriteLine(Arg.Is<string>(x => x.Contains(exceptionMessage)));
        }
    }
}