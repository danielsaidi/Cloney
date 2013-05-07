using System.IO;
using Cloney.Core.IO;
using Cloney.Core.Namespace;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests.Namespace
{
    [TestFixture]
    public class DirectoryNamespaceResolverBehavior
    {
        private INamespaceResolver resolver;
        private IDirectory directory;


        [SetUp]
        public void SetUp()
        {
            directory = Substitute.For<IDirectory>();
            directory.Exists(Arg.Any<string>()).Returns(true);
            resolver = new DirectoryNamespaceResolver(directory);
        }


        [Test]
        [ExpectedException(typeof(DirectoryNotFoundException))]
        public void ResolveNamespace_ShouldThrowExceptionForNonExistingFolder()
        {
            directory.Exists(Arg.Any<string>()).Returns(false);
            
            resolver.ResolveNamespace("foobar");
        }

        [Test]
        [ExpectedException(typeof(InvalidFolderException))]
        public void ResolveNamespace_ShouldThrowExceptionForEmptyFolderName()
        {
            resolver.ResolveNamespace("");
        }

        [Test]
        [TestCase("foo/bar/solution")]
        [TestCase("c:/foo/bar/solution")]
        public void ResolveNamespace_ShouldHandleForwardSlashes(string path)
        {
            var result = resolver.ResolveNamespace(path);
            
            Assert.That(result, Is.EqualTo("solution"));
        }

        [Test]
        [TestCase("foo\\bar\\solution")]
        [TestCase("c:foo\\bar\\solution")]
        public void ResolveNamespace_ShouldHandleBackwardSlashes(string path)
        {
            var result = resolver.ResolveNamespace(path);

            Assert.That(result, Is.EqualTo("solution"));
        }

        [Test]
        [TestCase("foo/bar/solution/")]
        [TestCase("c:/foo/bar/solution/")]
        [TestCase("foo\\bar\\solution\\")]
        [TestCase("c:\\foo\\bar\\solution\\")]
        public void ResolveNamespace_ShouldHandleEndingSlash(string path)
        {
            var result = resolver.ResolveNamespace(path);

            Assert.That(result, Is.EqualTo("solution"));
        }

        [Test]
        [TestCase("solution")]
        [TestCase("foo.bar")]
        public void ResolveNamespace_ShouldHandleNoSlash(string path)
        {
            var result = resolver.ResolveNamespace(path);

            Assert.That(result, Is.EqualTo(path));
        }
    }
}