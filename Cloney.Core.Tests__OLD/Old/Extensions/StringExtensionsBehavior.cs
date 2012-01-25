using Cloney.Core.Old.Extensions;
using NUnit.Framework;

namespace Cloney.Core.Tests.Old.Extensions
{
    [TestFixture]
    public class StringExtensionsBehavior
    {
        [Test]
        public void AdjustPathSlashes_ShouldReplaceForwardSlash()
        {
            Assert.That("foo/bar".AdjustPathSlashes(), Is.EqualTo("foo\\bar"));
        }
    }
}