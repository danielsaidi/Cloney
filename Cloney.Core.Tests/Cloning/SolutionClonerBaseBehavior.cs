using System;
using Cloney.Core.Cloning;
using NUnit.Framework;

namespace Cloney.Core.Tests.Cloning
{
    [TestFixture]
    public class SolutionClonerBaseBehavior
    {
        private TestCloner cloner;
        private int eventCount;


        [SetUp]
        public void SetUp()
        {
            cloner = new TestCloner();
            eventCount = 0;
        }


        [Test]
        public void CurrentPath_ShouldNotTriggerEventIfNoSubscribersExist()
        {
            cloner.OnCurrentPathChanged(new EventArgs());

            cloner.CurrentPath = "foo";

            Assert.That(eventCount, Is.EqualTo(0));
        }

        [Test]
        public void CurrentPath_ShouldTriggerEventIfSubscribersExist()
        {
            cloner.CurrentPathChanged += cloner_CurrentPathChanged;

            cloner.CurrentPath = "foo";

            Assert.That(eventCount, Is.EqualTo(1));
        }


        [Test]
        public void OnCloningBegun_ShouldNotTriggerEventIfNoSubscribersExist()
        {
            cloner.OnCloningBegun(new EventArgs());

            Assert.That(eventCount, Is.EqualTo(0));
        }

        [Test]
        public void OnCloningBegun_ShouldTriggerEventIfSubscribersExist()
        {
            cloner.CloningBegun += cloner_CloningBegun;

            cloner.OnCloningBegun(new EventArgs());

            Assert.That(eventCount, Is.EqualTo(1));
        }

        [Test]
        public void OnCloningEnded_ShouldNotTriggerEventIfNoSubscribersExist()
        {
            cloner.OnCloningEnded(new EventArgs());

            Assert.That(eventCount, Is.EqualTo(0));
        }

        [Test]
        public void OnCloningEnded_ShouldTriggerEventIfSubscribersExist()
        {
            cloner.CloningEnded += cloner_CloningEnded;

            cloner.OnCloningEnded(new EventArgs());

            Assert.That(eventCount, Is.EqualTo(1));
        }

        [Test]
        public void OnCurrentPathChanged_ShouldNotTriggerEventIfNoSubscribersExist()
        {
            cloner.OnCurrentPathChanged(new EventArgs());

            Assert.That(eventCount, Is.EqualTo(0));
        }

        [Test]
        public void OnCurrentPathChanged_ShouldTriggerEventIfSubscribersExist()
        {
            cloner.CurrentPathChanged += cloner_CurrentPathChanged;

            cloner.OnCurrentPathChanged(new EventArgs());

            Assert.That(eventCount, Is.EqualTo(1));
        }

        
        void cloner_CloningBegun(object sender, EventArgs e)
        {
            eventCount++;
        }

        void cloner_CloningEnded(object sender, EventArgs e)
        {
            eventCount++;
        }

        void cloner_CurrentPathChanged(object sender, EventArgs e)
        {
            eventCount++;
        }
    }


    internal class TestCloner : SolutionClonerBase { }
}