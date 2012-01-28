using System.Collections.Generic;
using Cloney.Core.SubRoutines;
using NUnit.Framework;

namespace Cloney.Core.Tests.SubRoutines
{
    [TestFixture]
    public class SubRoutineBaseBehavior
    {
        private TestRoutine routine;


        [SetUp]
        public void SetUp()
        {
            routine = new TestRoutine();
        }


        [Test]
        public void Ctor_ShouldInitWithUnfinishedState()
        {
            Assert.That(routine.Finished, Is.False);
        }

        [Test]
        public void Finish_ShouldSetFinishedState()
        {
            routine.Run(null);

            Assert.That(routine.Finished, Is.True);
        }
    }


    internal class TestRoutine : SubRoutineBase, ISubRoutine
    {
        public void Run(IDictionary<string, string> args)
        {
            Finish();
        }
    }
}