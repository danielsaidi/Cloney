using System.Collections.Generic;
using Cloney.Core.Console;
using NUnit.Framework;

namespace Cloney.Core.Tests.Console
{
    [TestFixture]
    public class CommandLineArgumentsBehavior
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void RawCollection_ShouldReturnCtorArgument()
        {
            var dict = new Dictionary<string, string>();
            var args = new CommandLineArguments(dict);

            Assert.That(args.Raw, Is.EqualTo(dict));
        }

        [Test]
        public void HasArgument_ShouldReturnTrueIfArgumentIsPresentInCtorArgument()
        {
            var dict = new Dictionary<string, string> { { "foo", "bar" } };
            var args = new CommandLineArguments(dict);

            Assert.That(args.HasArgument("foo"), Is.True);
        }

        [Test]
        public void HasArgument_ShouldReturnFalseIfArgumentIsNotPresentInCtorArgument()
        {
            var dict = new Dictionary<string, string> { { "foo", "bar" } };
            var args = new CommandLineArguments(dict);

            Assert.That(args.HasArgument("bar"), Is.False);
        }

        [Test]
        public void HasArgument_ShouldReturnTrueIfArgumentWithValueIsPresentInCtorArgument()
        {
            var dict = new Dictionary<string, string> { { "foo", "bar" } };
            var args = new CommandLineArguments(dict);

            Assert.That(args.HasArgument("foo", "bar"), Is.True);
        }

        [Test]
        public void HasArgument_ShouldReturnFalseIfArgumentWithMismatchValueIsPresentInCtorArgument()
        {
            var dict = new Dictionary<string, string> { { "foo", "bar" } };
            var args = new CommandLineArguments(dict);

            Assert.That(args.HasArgument("foo", "foo"), Is.False);
        }

        [Test]
        public void HasSingleArgument_ShouldReturnTrueIfArgumentIsSingleInCtorArgument()
        {
            var dict = new Dictionary<string, string> { { "foo", "bar" } };
            var args = new CommandLineArguments(dict);

            Assert.That(args.HasSingleArgument("foo"), Is.True);
        }

        [Test]
        public void HasSingleArgument_ShouldReturnFalseIfArgumentIsNotPresentInCtorArgument()
        {
            var dict = new Dictionary<string, string> { { "foo", "bar" } };
            var args = new CommandLineArguments(dict);

            Assert.That(args.HasSingleArgument("bar"), Is.False);
        }

        [Test]
        public void HasSingleArgument_ShouldReturnFalseIfArgumentIsNotSingleInCtorArgument()
        {
            var dict = new Dictionary<string, string> {{"foo", "bar"}, {"foo2", "bar"}};
            var args = new CommandLineArguments(dict);

            Assert.That(args.HasSingleArgument("foo"), Is.False);
        }

        [Test]
        public void HasSingleArgument_ShouldReturnTrueIfArgumentWithValueIsSingleInCtorArgument()
        {
            var dict = new Dictionary<string, string> {{"foo", "bar"}};
            var args = new CommandLineArguments(dict);

            Assert.That(args.HasSingleArgument("foo", "bar"), Is.True);
        }

        [Test]
        public void HasSingleArgument_ShouldReturnFalseIfArgumentWithMismatchValueIsSingleInCtorArgument()
        {
            var dict = new Dictionary<string, string> {{"foo", "bar"}};
            var args = new CommandLineArguments(dict);

            Assert.That(args.HasSingleArgument("foo", "foo"), Is.False);
        }

        [Test]
        public void HasSingleArgument_ShouldReturnTrueIfArgumentWithValueIsNotSingleInCtorArgument()
        {
            var dict = new Dictionary<string, string> {{"foo", "bar"}, {"foo2", "bar"}};
            var args = new CommandLineArguments(dict);

            Assert.That(args.HasSingleArgument("foo", "bar"), Is.False);
        }
    }
}
