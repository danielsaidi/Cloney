using System;
using System.Reflection;
using Microsoft.Win32;
using NUnit.Framework;
using System.IO;

namespace Cloney.ContextMenu.Tests
{
    [TestFixture]
    public class ContextMenuInstallerTests
    {
        private string binFilePath;
        private ContextMenuInstaller installer;
        private const string RegistryKeyName = "VisualStudio.Launcher.sln";
        private const string MenuText = "Clone solution with Cloney";


        [SetUp]
        public void SetUp()
        {
            binFilePath = GetFilePathToAssemblyBinDirectory();
            installer = new ContextMenuInstaller();
        }

        [Test]
        public void RegisterContextMenu_WithFilePath_ShouldRegisterExtensionWithSpecifiedFilePath()
        {
            var filePathToCloneyWizardExe = string.Format(@"{0}\..\..\..\Cloney\bin\Debug", binFilePath);

            installer.RegisterContextMenu(filePathToCloneyWizardExe, MenuText);

            var key = Registry.ClassesRoot.OpenSubKey(string.Format(@"{0}\Shell\Cloney Context Menu\command",RegistryKeyName) );

            Assert.That(key, Is.Not.Null);
            Assert.That(key.GetValue(""), Is.StringContaining("Cloney.Wizard.exe"));
        }

        [Test]
        public void RegisterContextMenu_WithIncorrectFilePath_ShouldThrowException()
        {
            var incorrectFilePath = string.Format(@"{0}\..", binFilePath);

            Assert.Throws<FileNotFoundException>(() => installer.RegisterContextMenu(incorrectFilePath, MenuText));
        }

        [Test]
        public void RegisterContextMenu_WithNonExistantFilePath_ShouldThrowException()
        {
            var incorrectFilePath = string.Format(@"{0}\idontexist", binFilePath);

            Assert.Throws<FileNotFoundException>(() => installer.RegisterContextMenu(incorrectFilePath, MenuText));
        }

        [Test]
        public void UnregisterContextMenu_NoParameters_ShouldUninstallShellExtension()
        {
            var filePathToCloneyWizardExe = string.Format(@"{0}\..\..\..\Cloney\bin\Debug", binFilePath);
            installer.RegisterContextMenu(filePathToCloneyWizardExe, MenuText);

            installer.UnregisterContextMenu();

            var key = Registry.ClassesRoot.OpenSubKey(string.Format(@"{0}\Shell\Cloney Context Menu", RegistryKeyName));
            Assert.That(key, Is.Null);
        }

        [Test]
        public void UnregisterContextMenu_WithNothingInstalled_ShouldNotThrowAnException()
        {
            installer.UnregisterContextMenu();
        }

        [TearDown]
        protected void TearDown()
        {
           
                installer.UnregisterContextMenu();
        }


        private static string GetFilePathToAssemblyBinDirectory()
        {
            var codeBase = Assembly.GetExecutingAssembly().GetName().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}