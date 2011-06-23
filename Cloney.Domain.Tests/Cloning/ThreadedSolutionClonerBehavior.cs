using System.Threading;
using Cloney.Domain.Cloning;
using Cloney.Domain.Cloning.Abstractions;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Domain.Tests.Cloning
{
    [TestFixture]
    public class ThreadedSolutionClonerBehavior
    {
        private ICanCloneSolution baseInstance;
        private ICanCloneSolution solutionCloner;

        private int begunCount;
        private int endedCount;


        [SetUp]
        public void SetUp()
        {
            begunCount = 0;
            endedCount = 0;

            baseInstance = Substitute.For<ICanCloneSolution>();

            solutionCloner = new ThreadedSolutionCloner(baseInstance);
            solutionCloner.CloningBegun += solutionCloner_CloningBegun;
            solutionCloner.CloningEnded += solutionCloner_CloningEnded;
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
        public void CurrentPath_ShouldReturnEmptyStringForNoBaseInstanceValue()
        {
            var result = solutionCloner.CurrentPath;

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void CurrentPath_ShouldReturnBaseInstanceValue()
        {
            baseInstance.CurrentPath.Returns("foo");

            var result = solutionCloner.CurrentPath;

            Assert.That(result, Is.EqualTo("foo"));
        }

        [Test]
        public void CloneSolution_ShouldCallBaseInstanceMethod()
        {
            solutionCloner.CloneSolution("foo", "bar", "foobar", "barfoo");
            Thread.Sleep(100);

            baseInstance.Received().CloneSolution("foo", "bar", "foobar", "barfoo");
        }

        [Test]
        public void CloneSolution_ShouldRaiseEvents()
        {
            Assert.That(begunCount, Is.EqualTo(0));
            Assert.That(endedCount, Is.EqualTo(0));

            solutionCloner.CloneSolution("foo", "bar", "foobar", "barfoo");
            Thread.Sleep(100);

            Assert.That(begunCount, Is.EqualTo(1));
            Assert.That(endedCount, Is.EqualTo(1));
        }


    }
}
