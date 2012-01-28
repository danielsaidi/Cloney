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

        [Test]
        public void ArgHasKeyWithValue_ShouldReturnFalseForNoArguments()
        {
            var result = routine.ArgHasKeyWithValue(new Dictionary<string, string>(), "foo", "bar");

            Assert.That(result, Is.False);
        }

        [Test]
        public void ArgHasKeyWithValue_ShouldAbortForInvalidArguments()
        {
            var result = routine.ArgHasKeyWithValue(new Dictionary<string, string> { { "foo", "bar" } }, "bar", "foo");

            Assert.That(result, Is.False);
        }

        [Test]
        public void ArgHasKeyWithValue_ShouldProceedForRelevantArgument()
        {
            var result = routine.ArgHasKeyWithValue(new Dictionary<string, string> { { "foo", "bar" } }, "foo", "bar");

            Assert.That(result, Is.True);
        }
    }


    internal class TestRoutine : SubRoutineBase, ISubRoutine
    {
        new public bool ArgHasKeyWithValue(Dictionary<string, string> args, string key, string value)
        {
            return base.ArgHasKeyWithValue(args, key, value);
        }

        public void Run(IDictionary<string, string> args)
        {
            Finish();
        }
    }
}