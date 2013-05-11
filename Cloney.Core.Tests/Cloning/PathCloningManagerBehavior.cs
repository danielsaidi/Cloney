using Cloney.Core.Cloning;
using Cloney.Core.IO;
using NUnit.Framework;

namespace Cloney.Core.Tests.Cloning
{
    [TestFixture]
    public class PathCloningManagerBehavior
    {
        private IPathCloningManager manager;
        private PathPatternMatcher patternMatcher;


        [SetUp]
        public void Setup()
        {
            patternMatcher = new PathPatternMatcher();
            manager = new PathCloningManager(patternMatcher, new[]{"**obj"}, new[]{"*.txt"}, new[]{"*.exe"});
        }


        [Test]
        public void ShouldExcludeFolderPath_ShouldReturnFalseForNoMatchInExcludeCollection()
        {
            Assert.That(manager.ShouldExcludeFolder("C:\\foo\\bar\\bin"), Is.False);
        }

        [Test]
        public void ShouldExcludeFolderPath_ShouldReturnTrueForMatchInExcludeCollection()
        {
            Assert.That(manager.ShouldExcludeFolder("C:\\foo\\bar\\obj"), Is.True);
        }

        [Test]
        public void ShouldExcludeFilePath_ShouldReturnFalseForNoMatchInExcludeCollection()
        {
            Assert.That(manager.ShouldExcludeFile("C:\\foo\\bar\\foo.rtf"), Is.False);
        }

        [Test]
        public void ShouldExcludeFilePath_ShouldReturnTrueForMatchInExcludeCollection()
        {
            Assert.That(manager.ShouldExcludeFile("C:\\foo\\bar\\foo.txt"), Is.True);
        }

        [Test]
        public void ShouldPlainCopyFilePath_ShouldReturnFalseForNoMatchInPlainCopyCollection()
        {
            Assert.That(manager.ShouldPlainCopyFile("C:\\foo\\bar\\foo.rtf"), Is.False);
        }

        [Test]
        public void ShouldPlainCopyFilePath_ShouldReturnTrueForMatchInPlainCopyCollection()
        {
            Assert.That(manager.ShouldPlainCopyFile("C:\\foo\\bar\\foo.exe"), Is.True);
        }
    }
}
