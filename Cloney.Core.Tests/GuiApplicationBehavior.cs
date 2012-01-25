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

        private IProcess process;


        [SetUp]
        public void SetUp()
        {
            process = Substitute.For<IProcess>();

            program = new GuiApplication(process);
        }


        [Test]
        public void Start_ShouldLaunchExternalProgramForNoArguments()
        {
            program.Start(new List<string>());

            process.Received().Start("Cloney.Wizard.exe");
        }

        [Test]
        public void Start_ShouldLaunchExternalProgramForArguments()
        {
            program.Start(new List<string> { "foo", "bar" });

            process.Received().Start("Cloney.Wizard.exe");
        }
    }
}