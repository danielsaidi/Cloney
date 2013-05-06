using System.Linq;
using Cloney.Core.Localization;
using NUnit.Framework;

namespace Cloney.Core.Tests.Localization
{
    [TestFixture]
    public class HierarchicalResourceManagerTranslatorBehavior
    {
        private HierarchicalResourceManagerTranslator obj;


        [SetUp]
        public void SetUp()
        {
            obj = new HierarchicalResourceManagerTranslator(null);
        }


        [Test]
        public void GetKeys_ShouldHandleSingleKey()
        {
            var result = obj.GetKeys("User");

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First(), Is.EqualTo("User"));
        }

        [Test]
        public void GetKeys_ShouldHandleMultipleKeys()
        {
            var result = obj.GetKeys("Domain_User_UserName").ToList();

            Assert.That(result.Count(), Is.EqualTo(3));
            Assert.That(result[0], Is.EqualTo("Domain_User_UserName"));
            Assert.That(result[1], Is.EqualTo("User_UserName"));
            Assert.That(result[2], Is.EqualTo("UserName"));
        }

        [Test]
        public void GetKeys_ShouldIgnoreDifferentSeparator()
        {
            obj = new HierarchicalResourceManagerTranslator(null, "-");

            var result = obj.GetKeys("Domain_User_UserName").ToList();

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result[0], Is.EqualTo("Domain_User_UserName"));
        }
    }
}