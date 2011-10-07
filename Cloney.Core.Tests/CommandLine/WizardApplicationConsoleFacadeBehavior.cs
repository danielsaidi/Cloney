using System.Collections.Generic;
using Cloney.Core.CommandLine;
using NExtra.Abstractions;
using NExtra.Diagnostics.Abstractions;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests.CommandLine
{
    [TestFixture]
    public class WizardApplicationConsoleFacadeBehavior
    {
        private WizardApplicationFacade obj;
        private IConsole console;
        private IProcess process;


        [SetUp]
        public void SetUp()
        {
            console = Substitute.For<IConsole>();
            process = Substitute.For<IProcess>();

            obj = new WizardApplicationFacade(console, process, "exe" ,new Dictionary<string, string> { { "foo", "bar" } }, "foo");
        }

        private void SetUpFacadeWithoutArguments()
        {
            obj = new WizardApplicationFacade(console, process, "exe", new Dictionary<string, string>(), "foo");
        }


        [Test]
        public void ShouldStart_ShouldReturnFalseForApplicationWithArguments()
        {
            Assert.That(obj.ShouldStart, Is.False);
        }

        [Test]
        public void ShouldStart_ShouldReturnTrueForApplicationWithoutArguments()
        {
            SetUpFacadeWithoutArguments();

            Assert.That(obj.ShouldStart, Is.True);
        }


        [Test]
        public void Start_ShouldAbortForApplicationWithArguments()
        {
            var result = obj.Start();

            Assert.That(result, Is.False);
            console.DidNotReceive().WriteLine(Arg.Any<string>());
            process.DidNotReceive().Start(Arg.Any<string>());
        }

        [Test]
        public void Start_ShouldProceedForApplicationWithoutArguments()
        {
            SetUpFacadeWithoutArguments();

            var result = obj.Start();

            Assert.That(result, Is.True);
            console.Received().WriteLine("foo");
            process.Received().Start("exe");
        }
    }
}