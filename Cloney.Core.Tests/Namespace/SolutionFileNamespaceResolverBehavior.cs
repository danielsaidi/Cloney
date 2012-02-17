using System.Collections.Generic;
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
        private IDirectory directory;


        [SetUp]
        public void SetUp()
        {
            directory = Substitute.For<IDirectory>();
            resolver = new SolutionFileNamespaceResolver(directory);
        }


        [Test]
        public void ResolveNamespace_ShouldReturnEmptyStringForNoFiles()
        {
            directory.GetFiles("foo").Returns(new List<string>());

            var result = resolver.ResolveNamespace("foo");

            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void ResolveNamespace_ShouldReturnSolutionFileName()
        {
            directory.GetFiles("foo").Returns(new List<string> { "class.cs", "interface.cs", "solution.sln", "class.cs" });

            var result = resolver.ResolveNamespace("foo");

            Assert.That(result, Is.EqualTo("solution"));
        }

        [Test]
        public void ResolveNamespace_ShouldReturnFirstOfSeveralSolutionFileNames()
        {
            directory.GetFiles("foo").Returns(new List<string> { "class.cs", "interface.cs", "solution1.sln", "solution2.sln" });

            var result = resolver.ResolveNamespace("foo");

            Assert.That(result, Is.EqualTo("solution1"));
        }
    }
}