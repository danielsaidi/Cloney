using System.Collections.Generic;
using System.Linq;
using Cloney.Core.Extensions;
using NUnit.Framework;

namespace Cloney.Core.Tests.Extensions
{
    [TestFixture]
    public class IEnumerableExtensionsBehavior
    {
        private List<KeyValuePair<string, int>> collection;
        private List<KeyValuePair<string, int>> magazineSales;


        [SetUp]
        public void SetUp()
        {
            collection = new List<KeyValuePair<string, int>> {
				new KeyValuePair<string, int>("a", 99),
				new KeyValuePair<string, int>("b", 98),
				new KeyValuePair<string, int>("c", 97),
				new KeyValuePair<string, int>("d", 96),
				new KeyValuePair<string, int>("e", 95)
			};

            magazineSales = new List<KeyValuePair<string, int>> {
                new KeyValuePair<string, int>("DN", 5),
                new KeyValuePair<string, int>("SvD", 5),
                new KeyValuePair<string, int>("DN", 1),
                new KeyValuePair<string, int>("SvD", 1)
            };
        }


        [Test]
        public void Contains_ShouldReturnFalseForNullCollection()
        {
            collection = null;

            Assert.That(collection.Contains(magazineSales.First(), true), Is.False);
        }

        [Test]
        public void Contains_ShouldReturnFalseForEmptyCollection()
        {
            collection = new List<KeyValuePair<string, int>>();

            Assert.That(collection.Contains(magazineSales.First(), true), Is.False);
        }

        [Test]
        public void Contains_ShouldReturnFalseForNonExistingValue()
        {
            Assert.That(collection.Contains(magazineSales.First(), true), Is.False);
        }

        [Test]
        public void Contains_ShouldReturnTrueForExistingValue()
        {
            Assert.That(collection.Contains(collection.First(), true), Is.True);
        }
    }
}
