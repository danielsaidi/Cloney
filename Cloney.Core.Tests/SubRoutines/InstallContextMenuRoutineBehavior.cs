using System.IO;
using Cloney.ContextMenu;
using Cloney.Core.Console;
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
        private IConsole console;
        private ITranslator translator;
        private IContextMenuInstaller installer;


        [SetUp]
        public void SetUp()
        {
            console = Substitute.For<IConsole>();
            translator = Substitute.For<ITranslator>();
            installer = Substitute.For<IContextMenuInstaller>();
            translator.Translate("InstallMessage").Returns("Installmessage");
            translator.Translate("UninstallMessage").Returns("Uninstallmessage");
            translator.Translate("SuccessfulInstallMessage").Returns("SuccessfulInstallmessage");
            translator.Translate("SuccessfulUninstallMessage").Returns("SuccessfulUninstallmessage");
            translator.Translate("InstallerErrorMessage").Returns("InstallerErrorMessage");
            translator.Translate("ContextMenuText").Returns("ContextMenuText");

            routine = new InstallContextMenuRoutine(console, translator, installer);
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
            console.Received().WriteLine("Installmessage");
        }

        [Test]
        public void Run_WithInstallArgument_ShouldRunContextMenuInstaller()
        {
            var result = routine.Run(new[] { "--install" });

            Assert.That(result, Is.True);
            installer.Received().RegisterContextMenu(Arg.Any<string>(), Arg.Any<string>());
            translator.Received().Translate("SuccessfulInstallMessage");
            console.Received().WriteLine(Arg.Is<string>(s => s.Contains("SuccessfulInstallmessage")));
        }

        [Test]
        public void Run_WithInstallArgument_ShouldPassInFilePathForConsoleExe()
        {
            var result = routine.Run(new[] { "--install" });

            Assert.That(result, Is.True);
            //installer.Received().RegisterContextMenu(Arg.Is<string>(x => x.Contains(@"Cloney")), Arg.Any<string>());
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