using Cloney.Core.Namespace;
using NUnit.Framework;

namespace Cloney.Core.Tests.Namespace
{
    [TestFixture]
    public class FolderNamespaceResolverBehavior
    {
        private INamespaceResolver resolver;


        [SetUp]
        public void SetUp()
        {
            resolver = new FolderNamespaceResolver();
        }


        [Test]
        public void ResolveNamespace_ShouldReturnEmptyStringForMissingFolderName()
        {
            var result = resolver.ResolveNamespace("");

            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void ResolveNamespace_ShouldHandleForwardSlashes()
        {
            var result = resolver.ResolveNamespace("foo/bar/solution");

            Assert.That(result, Is.EqualTo("solution"));
        }

        [Test]
        public void ResolveNamespace_ShouldHandleBackwardSlashes()
        {
            var result = resolver.ResolveNamespace("foo\\bar\\solution");

            Assert.That(result, Is.EqualTo("solution"));
        }

        [Test]
        public void ResolveNamespace_ShouldHandleForwardEndingSlash()
        {
            var result = resolver.ResolveNamespace("foo/bar/solution/");

            Assert.That(result, Is.EqualTo("solution"));
        }

        [Test]
        public void ResolveNamespace_ShouldHandleBackwardEndingSlash()
        {
            var result = resolver.ResolveNamespace("foo\\bar\\solution\\");

            Assert.That(result, Is.EqualTo("solution"));
        }

        [Test]
        public void ResolveNamespace_ShouldHandleNoSlash()
        {
            var result = resolver.ResolveNamespace("solution");

            Assert.That(result, Is.EqualTo("solution"));
        }
    }
}