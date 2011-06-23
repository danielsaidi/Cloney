using Cloney.Domain.Cloning;
using NUnit.Framework;

namespace Cloney.Domain.Tests.Cloning
{
    [TestFixture]
    public class FolderNamespaceExtractorBehavior
    {
        private FolderNamespaceExtractor extractor;


        [SetUp]
        public void SetUp()
        {
            extractor = new FolderNamespaceExtractor();
        }


        [Test]
        public void ExtractFolderNamespace_ShouldReturnEmptyStringForNullOrEmptyPath()
        {
            string str = null;

            Assert.That(extractor.ExtractFolderNamespace(str), Is.EqualTo(string.Empty));
            Assert.That(extractor.ExtractFolderNamespace(""), Is.EqualTo(string.Empty));
        }

        [Test]
        public void ExtractFolderNamespace_ShouldHandleMissingSlash()
        {
            const string str = "Foo.Bar";

            Assert.That(extractor.ExtractFolderNamespace(str), Is.EqualTo("Foo.Bar"));
        }

        [Test]
        public void ExtractFolderNamespace_ShouldHandleSingleSlash()
        {
            const string str = "c:\\Foo.Bar";

            Assert.That(extractor.ExtractFolderNamespace(str), Is.EqualTo("Foo.Bar"));
        }

        [Test]
        public void ExtractFolderNamespace_ShouldHandleMultipleSlashes()
        {
            const string str = "c:\\Development\\MyProject\\Foo.Bar";

            Assert.That(extractor.ExtractFolderNamespace(str), Is.EqualTo("Foo.Bar"));
        }
    }
}