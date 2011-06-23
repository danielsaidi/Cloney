using System;
using Cloney.Domain.Extensions;
using NUnit.Framework;

namespace Cloney.Domain.Tests.Extensions
{
    [TestFixture]
    public class StringExtensionsBehavior
	{
        [Test]
        public void IsNullOrEmpty_ShouldHandleNull()
        {
            String testString = null;

            Assert.That(testString.IsNullOrEmpty(), Is.True);
        }

        [Test]
        public void IsNullOrEmpty_Trim_ShouldReturnTrueForEmptyString()
        {
            Assert.That(" ".IsNullOrEmpty(), Is.True);
        }

        [Test]
        public void IsNullOrEmpty_ShouldReturnTrueForEmptyStringAndNoTrim()
        {
            Assert.That("".IsNullOrEmpty(false), Is.True);
        }

        [Test]
        public void IsNullOrEmpty_Trim_ShouldReturnTrueForNonEmptyString()
        {
            Assert.That(" ".IsNullOrEmpty(), Is.True);
        }

        [Test]
        public void IsNullOrEmpty_ShouldReturnFalseForNonEmptyStringAndNoTrim()
        {
            Assert.That(" ".IsNullOrEmpty(false), Is.False);
        }
    }
}