using System;
using Cloney.Core.SolutionCloners;
using NUnit.Framework;

namespace Cloney.Core.Tests.SolutionCloners
{
    [TestFixture]
    public class SolutionClonerBaseBehavior
    {
        private TestCloner cloner;
        private int begunCount;
        private int endedCount;


        [SetUp]
        public void SetUp()
        {
            cloner = new TestCloner();
        }


        [Test]
        public void OnCloningBegun_ShouldNotTriggerEventIfNoSubscribersExist()
        {
            cloner.OnCloningBegun(new EventArgs());

            Assert.That(begunCount, Is.EqualTo(0));
        }

        [Test]
        public void OnCloningBegun_ShouldTriggerEventIfSubscribersExist()
        {
            cloner.CloningBegun += cloner_CloningBegun;

            cloner.OnCloningBegun(new EventArgs());

            Assert.That(begunCount, Is.EqualTo(1));
        }

        [Test]
        public void OnCloningEnded_ShouldNotTriggerEventIfNoSubscribersExist()
        {
            cloner.OnCloningEnded(new EventArgs());

            Assert.That(endedCount, Is.EqualTo(0));
        }

        [Test]
        public void OnCloningEnded_ShouldTriggerEventIfSubscribersExist()
        {
            cloner.CloningEnded += cloner_CloningEnded;

            cloner.OnCloningEnded(new EventArgs());

            Assert.That(endedCount, Is.EqualTo(1));
        }


        void cloner_CloningBegun(object sender, EventArgs e)
        {
            begunCount++;
        }

        void cloner_CloningEnded(object sender, EventArgs e)
        {
            endedCount++;
        }
    }


    internal class TestCloner : SolutionClonerBase { }
}