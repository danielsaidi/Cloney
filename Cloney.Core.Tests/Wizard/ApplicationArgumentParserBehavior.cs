using Cloney.Core.Console;
using Cloney.Core.Wizard;
using NUnit.Framework;

namespace Cloney.Core.Tests.Wizard
{
    [TestFixture]
    public class ApplicationArgumentParserBehavior
    {/*
        private ICommandLineArgumentParser<ApplicationArguments> argumentParser;


        [SetUp]
        public void SetUp()
        {
            argumentParser = new ApplicationCommandLineArgumentParser();
        }


        [Test]
        public void Constructor_ShouldNotParseIrrelevantArgs()
        {
            var args = argumentParser.ParseCommandLineArguments(new[] { "--foo", "--bar" });

            Assert.That(args.ModalMode, Is.False);
            Assert.That(args.SourcePath, Is.Null);
        }

        [Test]
        public void Constructor_ShouldParseModalMode()
        {
            var args = argumentParser.ParseCommandLineArguments(new[] { "--modal", "--bar" });

            Assert.That(args.ModalMode, Is.True);
            Assert.That(args.SourcePath, Is.Null);
        }

        [Test]
        public void Constructor_ShouldParseSourcePath()
        {
            var args = argumentParser.ParseCommandLineArguments(new[] { "--source=foo", "--bar" });

            Assert.That(args.ModalMode, Is.False);
            Assert.That(args.SourcePath, Is.EqualTo("foo"));
        }*/
    }
}