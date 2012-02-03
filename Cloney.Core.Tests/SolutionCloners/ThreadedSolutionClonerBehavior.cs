using Cloney.Core.SolutionCloners;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests.SolutionCloners
{
    [TestFixture]
    public class ThreadedSolutionClonerBehavior
    {
        private ISolutionCloner solutionCloner;
        private ISolutionCloner decoratedSolutionCloner;

        [SetUp]
        public void SetUp()
        {
            decoratedSolutionCloner = Substitute.For<ISolutionCloner>();
            decoratedSolutionCloner.CurrentPath.Returns("foo");

            solutionCloner = new ThreadedSolutionCloner(decoratedSolutionCloner);
        }


        [Test]
        public void CurrentPath_ShouldReturnDecoratedClassValue()
        {
            Assert.That(solutionCloner.CurrentPath, Is.EqualTo("foo"));
        }
    }
}