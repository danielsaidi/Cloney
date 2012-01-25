using System.Collections.Generic;
using NExtra.Diagnostics;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests
{
    [TestFixture]
    public class GuiApplicationBehavior
    {
        private GuiApplication program;


        [SetUp]
        public void SetUp()
        {
            program = new GuiApplication(Substitute.For<IProcess>());
        }


        [Test]
        public void Start_ShouldAbortForProvidedArguments()
        {
            program.Start(new List<string> { "foo", "bar" });

            program.Process.DidNotReceive().Start(Arg.Any<string>());
        }
        [Test]
        public void Start_ShouldLaunchExternalProgramForNoArguments()
        {
            program.Start(new List<string>());

            program.Process.Received().Start("Cloney.Wizard.exe");
        }
    }
}