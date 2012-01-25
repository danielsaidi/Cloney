using System.Collections.Generic;
using System.Threading;
using Cloney.Core.Old;
using Cloney.Core.Old.Abstractions;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests.Old
{
    [TestFixture]
    public class SolutionClonerBehavior
    {
        private SolutionCloner solutionCloner;
        private ICanMatchPathPattern pathPatternMatcher;
        private ICanReplaceNamespace namespaceReplacer;

        private readonly List<string> excludeFolderPatterns = new List<string> { "foo" };
        private readonly List<string> excludeFilePatterns = new List<string> { "bar" };
        private readonly List<string> plainCopyFilePatterns = new List<string> { "foobar" };

        private int begunCount, endedCount;
 

        [SetUp]
        public void SetUp()
        {
            pathPatternMatcher = Substitute.For<ICanMatchPathPattern>();
            namespaceReplacer = Substitute.For<ICanReplaceNamespace>();

            solutionCloner = new SolutionCloner(excludeFolderPatterns, excludeFilePatterns, plainCopyFilePatterns, pathPatternMatcher, namespaceReplacer);
            solutionCloner.CloningBegun += solutionCloner_CloningBegun;
            solutionCloner.CloningEnded += solutionCloner_CloningEnded;

            begunCount = 0;
            endedCount = 0;
        }


        void solutionCloner_CloningBegun(object sender, System.EventArgs e)
        {
            begunCount++;
        }

        void solutionCloner_CloningEnded(object sender, System.EventArgs e)
        {
            endedCount++;
        }


        [Test]
        public void Constructor_ShouldCreateDefaultInstance()
        {
            solutionCloner = new SolutionCloner(excludeFolderPatterns, excludeFilePatterns, plainCopyFilePatterns);

            Assert.That(solutionCloner.CurrentPath, Is.Null);
            Assert.That(solutionCloner.ExcludeFolderPatterns, Is.EqualTo(excludeFolderPatterns));
            Assert.That(solutionCloner.ExcludeFilePatterns, Is.EqualTo(excludeFilePatterns));
            Assert.That(solutionCloner.PlainCopyFilePatterns, Is.EqualTo(plainCopyFilePatterns));
            Assert.That(solutionCloner.PathPatternMatcher, Is.Not.Null);
            Assert.That(solutionCloner.NamespaceReplacer, Is.Not.Null);
        }

        [Test]
        public void Constructor_ShouldCreateCustomInstance()
        {
            Assert.That(solutionCloner.CurrentPath, Is.Null);
            Assert.That(solutionCloner.ExcludeFolderPatterns, Is.EqualTo(excludeFolderPatterns));
            Assert.That(solutionCloner.ExcludeFilePatterns, Is.EqualTo(excludeFilePatterns));
            Assert.That(solutionCloner.PlainCopyFilePatterns, Is.EqualTo(plainCopyFilePatterns));
            Assert.That(solutionCloner.PathPatternMatcher, Is.EqualTo(pathPatternMatcher));
            Assert.That(solutionCloner.NamespaceReplacer, Is.EqualTo(namespaceReplacer));
        }


        [Test]
        public void AdjustPath_ShouldReplaceSourceFolderPathWithEmptyStringAndCallNamespaceReplacer()
        {
            solutionCloner.AdjustPath("sourceFolderPath\\foo", "sourceFolderPath\\", "foo", "bar");

            namespaceReplacer.Received().ReplaceNamespace("foo", "foo", "bar");
        }

        [Test]
        public void AdjustPath_ShouldReturnNamespaceReplacerResult()
        {
            namespaceReplacer.ReplaceNamespace(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).Returns("result");

            var result = solutionCloner.AdjustPath("sourceFolderPath\\foo", "sourceFolderPath\\", "foo", "bar");

            Assert.That(result, Is.EqualTo("result"));
        }

        [Test]
        public void IsExcludedFolder_ShouldCallRegexExtender()
        {
            solutionCloner.IsExcludedFolder("foo");

            pathPatternMatcher.Received().IsAnyMatch("foo", excludeFolderPatterns);
        }

        [Test]
        public void IsExcludedFile_ShouldCallRegexExtender()
        {
            solutionCloner.IsExcludedFile("foo");

            pathPatternMatcher.Received().IsAnyMatch("foo", excludeFilePatterns);
        }

        [Test]
        public void IsPlainCopyFile_ShouldCallRegexExtender()
        {
            solutionCloner.IsPlainCopyFile("foo");

            pathPatternMatcher.Received().IsAnyMatch("foo", plainCopyFilePatterns);
        }

        [Test]
        public void CloneSolution_ShouldTriggerEvents()
        {
            Assert.That(begunCount, Is.EqualTo(0));
            Assert.That(endedCount, Is.EqualTo(0));

            solutionCloner.CloneSolution(null, null, null, null);
            Thread.Sleep(100);

            Assert.That(begunCount, Is.EqualTo(1));
            Assert.That(endedCount, Is.EqualTo(1));
        }
    }
}
