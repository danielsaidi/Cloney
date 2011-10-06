using System;
using Cloney.Core.CommandLine;
using Cloney.Core.CommandLine.Abstractions;
using NUnit.Framework;

namespace Cloney.Core.Tests.CommandLine
{
    [TestFixture]
    public class RequiredNamespaceArgumentValidatorBehavior
    {
        private INamespaceArgumentValidator obj;


        [SetUp]
        public void SetUp()
        {
            obj = new RequiredNamespaceArgumentValidator();
        }


        [Test, ExpectedException(typeof(NullReferenceException))]
        public void Validate_ShouldFailForNullValue()
        {
            obj.Validate(null, "bar");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void Validate_ShouldFailForEmptyValue()
        {
            obj.Validate("   ", "bar");
        }

        [Test]
        public void Validate_ShouldSucceedForNonEmptyValue()
        {
            obj.Validate("foo", "bar");
        }
    }
}