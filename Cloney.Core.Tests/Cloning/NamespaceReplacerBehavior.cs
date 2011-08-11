using Cloney.Core.Cloning;
using Cloney.Core.Cloning.Abstractions;
using NUnit.Framework;

namespace Cloney.Core.Tests.Cloning
{
    [TestFixture]
    public class NamespaceReplacerBehavior
    {
        private ICanReplaceNamespace namespaceReplacer;


        [SetUp]
        public void SetUp()
        {
            namespaceReplacer = new NamespaceReplacer();
        }

        
        [Test]
        public void ReplaceNamespace_ShouldNotAffectNonMatchString()
        {
            Assert.That(namespaceReplacer.ReplaceNamespace("Foo", "Bar", "Foo.Bar"), Is.EqualTo("Foo"));
        }

        [Test]
        public void ReplaceNamespace_ShouldReplaceFullNamespace()
        {
            Assert.That(namespaceReplacer.ReplaceNamespace("Foo", "Foo", "Foo.Bar"), Is.EqualTo("Foo.Bar"));
        }

        [Test]
        public void ReplaceNamespace_ShouldReplacePartialNamespace()
        {
            Assert.That(namespaceReplacer.ReplaceNamespace("Foo.Bar", "Foo", "Foo.Bar"), Is.EqualTo("Foo.Bar.Bar"));
        }

        [Test]
        public void ReplaceNamespace_ShouldReplaceFullNamespaceForLowerCaseMatch()
        {
            Assert.That(namespaceReplacer.ReplaceNamespace("foo", "Foo", "Foo.Bar"), Is.EqualTo("foo.bar"));
        }

        [Test]
        public void ReplaceNamespace_ShouldReplacePartialNamespaceForLowerCaseMatch()
        {
            Assert.That(namespaceReplacer.ReplaceNamespace("foo.bar", "Foo", "Foo.Bar"), Is.EqualTo("foo.bar.bar"));
        }
    }
}
