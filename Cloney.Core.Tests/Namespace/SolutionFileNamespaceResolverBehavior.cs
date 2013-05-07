using System.IO;
using Cloney.Core.IO;
using Cloney.Core.Namespace;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests.Namespace
{
    [TestFixture]
    public class SolutionFileNamespaceResolverBehavior
    {
        private INamespaceResolver resolver;
        private IFile file;


        [SetUp]
        public void SetUp()
        {
            file = Substitute.For<IFile>();
            file.Exists(Arg.Any<string>()).Returns(true);
            resolver = new SolutionFileNamespaceResolver(file);
        }


        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ResolveNamespace_ShouldThrowExceptionForFileNotFound()
        {
            file.Exists(Arg.Any<string>()).Returns(false);

            resolver.ResolveNamespace("foo.bar.sln");
        }

        [Test]
        [TestCase("foobar")]
        [TestCase("foobar.txt")]
        [TestCase("foo.bar.txt")]
        [ExpectedException(typeof(InvalidSolutionFileException))]
        public void ResolveNamespace_ShouldThrowExceptionForInvalidFileType(string fileName)
        {
            resolver.ResolveNamespace(fileName);
        }

        [Test]
        public void ResolveNamespace_ShouldReturnNamespaceForValidFile()
        {
            var result = resolver.ResolveNamespace("foo.bar.sln");

            Assert.That(result, Is.EqualTo("foo.bar"));
        }
    }
}