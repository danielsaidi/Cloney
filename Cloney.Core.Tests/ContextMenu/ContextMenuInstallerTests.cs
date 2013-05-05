using System.IO;
using Cloney.Core.ContextMenu;
using Cloney.Core.IO;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests.ContextMenu
{
    [TestFixture]
    public class ContextMenuInstallerTests
    {
        private ContextMenuInstaller installer;
        private IFile file;
        private IContextMenuRegistryWriter registryWriter;


        [SetUp]
        public void Setup()
        {
            file = Substitute.For<IFile>();
            file.Exists(Arg.Any<string>()).Returns(true);
            registryWriter = Substitute.For<IContextMenuRegistryWriter>();
            installer = new ContextMenuInstaller(file, registryWriter);
        }


        [Test]
        public void RegistryPath_ShouldReturnValidPath()
        {
            var path = installer.RegistryPath;
            const string expected = @"VisualStudio.Launcher.sln\shell\Cloney Context Menu";

            Assert.That(path, Is.EqualTo(expected));
        }


        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void RegisterContextMenu_ShouldThrowExceptionForInvalidApplicationPath()
        {
            file.Exists(Arg.Any<string>()).Returns(false);

            installer.RegisterContextMenu("foo", "bar");
        }

        [Test]
        public void RegisterContextMenu_ShouldRegisterShellExtensionForValidApplicationPath()
        {
            var expectedCommand = string.Format("\"foobar\" --source=\"%1\" --modal");

            installer.RegisterContextMenu("foobar", "Foo Bar");

            registryWriter.Received().RegisterShellExtension(installer.RegistryPath, "Foo Bar", expectedCommand);
        }


        [Test]
        public void UnregisterContextMenu_ShouldUnregisterShellExtension()
        {
            installer.UnregisterContextMenu();

            registryWriter.Received().UnregisterShellExtension(installer.RegistryPath);
        }
    }
}
