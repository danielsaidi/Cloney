using System.Collections.Specialized;
using System.Linq;
using Cloney.Domain.Extensions;
using NUnit.Framework;

namespace Cloney.Domain.Tests.Extensions
{
    [TestFixture]
    public class StringCollectionExtensionsBehavior
    {
        [Test]
        public void AsEnumerable_ShouldHandleNullCollection()
        {
            StringCollection testCollection = null;
            Assert.That(testCollection.AsEnumerable(), Is.Null);
        }

        [Test]
        public void AsEnumerable_EmptyCollectionShouldReturnEmptyResult()
        {
            var collection = new StringCollection();
            Assert.That(collection.AsEnumerable().Count(), Is.EqualTo(0));
        }

        [Test]
        public void AsEnumerable_NonEmptyCollectionShouldReturnNonEmptyResult()
        {
            var collection = new StringCollection { "foo", "bar" };
            Assert.That(collection.AsEnumerable().Count(), Is.EqualTo(2));
            Assert.That(collection.AsEnumerable().First(), Is.EqualTo("foo"));
            Assert.That(collection.AsEnumerable().Last(), Is.EqualTo("bar"));
        }

        [Test]
        public void IsNullOrEmpty_ShouldHandleNull()
        {
            StringCollection testCollection = null;
            Assert.That(testCollection.IsNullOrEmpty(), Is.True);
        }

        [Test]
        public void IsNullOrEmpty_ShouldReturnTrueForEmptyCollection()
        {
            Assert.That(new StringCollection().IsNullOrEmpty(), Is.True);
        }

        [Test]
        public void IsNullOrEmpty_ShouldReturnFalseForNonEmptyCollection()
        {
            var collection = new StringCollection { "foo" };
            Assert.That(collection.IsNullOrEmpty(), Is.False);
        }
    }
}