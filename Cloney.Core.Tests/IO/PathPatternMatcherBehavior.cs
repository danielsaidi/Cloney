using System.Collections.Generic;
using System.Linq;
using Cloney.Core.IO;
using NUnit.Framework;

namespace Cloney.Core.Tests.IO
{
    public class PathPatternMatcherBehavior
    {
        private PathPatternMatcher pathPatternMatcher;
        private List<string> exactCollection;
        private List<string> wildcardCollection;


        [SetUp]
        public void SetUp()
        {
            pathPatternMatcher = new PathPatternMatcher();

            exactCollection = new List<string> {"foo", "bar"};
            wildcardCollection = new List<string> {"*foo", "bar*"};
        }


        [Test]
        public void IsMatch_ShouldReturnFalseForExactMismatch()
        {
            Assert.That(pathPatternMatcher.IsMatch("foot", exactCollection.First()), Is.False);
        }

        [Test]
        public void IsMatch_ShouldReturnFalseForWildcardMismatches()
        {
            Assert.That(pathPatternMatcher.IsMatch("foot", wildcardCollection.First()), Is.False);
        }

        [Test]
        public void IsMatch_ShouldReturnTrueForExactMatch()
        {
            Assert.That(pathPatternMatcher.IsMatch("foo", exactCollection.First()), Is.True);
        }

        [Test]
        public void IsMatch_ShouldReturnTrueForWildcardMatches()
        {
            Assert.That(pathPatternMatcher.IsMatch("afoo", wildcardCollection.First()), Is.True);
        }


        [Test]
        public void IsAnyMatch_ShouldReturnFalseForNoSpecificedPaths()
        {
            Assert.That(pathPatternMatcher.IsAnyMatch("foo", new List<string>()), Is.False);
        }

        [Test]
        public void IsAnyMatch_ShouldReturnFalseForExactMismatch()
        {
            Assert.That(pathPatternMatcher.IsAnyMatch("foot", exactCollection), Is.False);
            Assert.That(pathPatternMatcher.IsAnyMatch("bart", exactCollection), Is.False);
        }

        [Test]
        public void IsAnyMatch_ShouldReturnFalseForWildcardMismatches()
        {
            Assert.That(pathPatternMatcher.IsAnyMatch("foot", wildcardCollection), Is.False);
            Assert.That(pathPatternMatcher.IsAnyMatch("obar", wildcardCollection), Is.False);
        }

        [Test]
        public void IsAnyMatch_ShouldReturnTrueForExactMatch()
        {
            Assert.That(pathPatternMatcher.IsAnyMatch("foo", exactCollection), Is.True);
            Assert.That(pathPatternMatcher.IsAnyMatch("bar", exactCollection), Is.True);
        }

        [Test]
        public void IsAnyMatch_ShouldReturnTrueForWildcardMatches()
        {
            Assert.That(pathPatternMatcher.IsAnyMatch("afoo", wildcardCollection), Is.True);
            Assert.That(pathPatternMatcher.IsAnyMatch("bara", wildcardCollection), Is.True);
        }
    }
}
