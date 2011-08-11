using Cloney.Core.Facades;
using NUnit.Framework;

namespace Cloney.Core.Tests.Facades
{
    [TestFixture]
    public class FacadeBaseBehavior
    {
        private FacadeTestClass facade;
        private string baseInstance;


        [SetUp]
        public void SetUp()
        {
            baseInstance = "foo";
            facade = new FacadeTestClass(baseInstance);
        }


        [Test]
        public void BaseInstance_ShouldBeSetByConstructor()
        {
            Assert.That(facade.BaseInstance, Is.EqualTo(baseInstance));
        }
    }


    public class FacadeTestClass : FacadeBase<string>
    {
        public FacadeTestClass(string baseInstance) : base(baseInstance) { }
    }
}