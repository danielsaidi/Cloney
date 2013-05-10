using System.IO;
using System.Text;
using Cloney.Core.IO;
using Cloney.Core.Reflection;
using NUnit.Framework;

namespace Cloney.Core.Tests.IO
{
    [TestFixture]
    public class StreamReaderFileEncodingResolverBehavior
    {
        private IFileEncodingResolver resolver;


        [SetUp]
        public void Setup()
        {
            resolver = new StreamReaderFileEncodingResolver();
        }


        [Test]
        public void ResolveFileEncoding_ShouldFailAndReturnDefaultEncodingEvenForResolvableFile()
        {
            var folderPath = Assembly_FileExtensions.GetFolderPathOfExecutingAssembly();
            var filePath = Path.Combine(folderPath, "ISO_8859_1.txt");
            var encoding = resolver.ResolveFileEncoding(filePath);

            Assert.That(encoding, Is.EqualTo(Encoding.UTF8));
        }

        [Test]
        public void ResolveFileEncoding_ShouldReturnDefaultEncodingForNonResolvableFile()
        {
            var folderPath = Assembly_FileExtensions.GetFolderPathOfExecutingAssembly();
            var filePath = Path.Combine(folderPath, "NSubstitute.xml");
            var encoding = resolver.ResolveFileEncoding(filePath);

            Assert.That(encoding, Is.EqualTo(Encoding.UTF8));
        }
    }
}
