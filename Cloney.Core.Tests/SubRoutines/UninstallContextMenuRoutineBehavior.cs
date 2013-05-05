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
    public class UninstallContextMenuRoutineBehavior
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
            translator.Translate("UninstallMessage").Returns("Uninstallmessage");
            translator.Translate("SuccessfulUninstallMessage").Returns("SuccessfulUninstallmessage");
            translator.Translate("InstallerErrorMessage").Returns("InstallerErrorMessage");
            translator.Translate("ContextMenuText").Returns("ContextMenuText");

            routine = new UninstallContextMenuRoutine(console, translator, installer);
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
            var result = routine.Run(new[] { "--uninstall", "--foo=bar", });

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
        public void Run_ShouldProceedForUnInstallArgument()
        {
            var result = routine.Run(new[] { "--uninstall" });

            Assert.That(result, Is.True);
            translator.Received().Translate("UninstallMessage");
            console.Received().WriteLine("Uninstallmessage");
            translator.Received().Translate("SuccessfulUninstallMessage");
            console.Received().WriteLine(Arg.Is<string>(s => s.Contains("SuccessfulUninstallmessage")));
        }

        [Test]
        public void Run_WithUninstallArgument_ShouldRunContextMenuInstaller()
        {
            var result = routine.Run(new[] { "--uninstall" });

            Assert.That(result, Is.True);
            installer.Received().UnregisterContextMenu();
        }

        [Test]
        public void Run_WhenInstallationFails_ShouldPrintFriendlyFailMessage()
        {
            const string exceptionMessage = "Something exceptional occurred";
            installer.When(x => x.UnregisterContextMenu())
                .Do(x => { throw new FileNotFoundException(exceptionMessage);});

            routine.Run(new[] { "--uninstall" });

            translator.Received().Translate("InstallerErrorMessage");
            console.Received().WriteLine(Arg.Is<string>(x => x.Contains(exceptionMessage)));
        }
    }
}