using System.IO;
using System.Text;
using Mozilla.CharDet;

namespace Cloney.Core.IO
{
    /// <summary>
    /// This class can be used to resolve file encodings,
    /// using the Mozilla-based project that is found at
    /// https://code.google.com/p/chardetsharp/issues/list.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public class UniversalFileEncodingResolver : IFileEncodingResolver
    {
        private readonly UniversalDetector baseEncoder;
        private readonly Encoding defaultEncoding;


        public UniversalFileEncodingResolver(Encoding defaultEncoding)
        {
            baseEncoder = new UniversalDetector();
            this.defaultEncoding = defaultEncoding;
        }


        public Encoding ResolveFileEncoding(string filePath)
        {
            var bytes = File.ReadAllBytes(filePath);
            baseEncoder.HandleData(bytes);
            baseEncoder.DataEnd();
            var charset = baseEncoder.DetectedCharsetName;

            return charset == null ? defaultEncoding : Encoding.GetEncoding(charset);
        }
    }
}