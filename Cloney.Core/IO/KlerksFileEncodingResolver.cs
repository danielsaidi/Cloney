using System.Text;

namespace Cloney.Core.IO
{
    /// <summary>
    /// From here http://www.architectshack.com/TextFileEncodingDetector.ashx
    /// Also used by http://findandreplace.codeplex.com which fulfills quite similar problem as cloney.
    /// </summary>
    public class KlerksFileEncodingResolver : IFileEncodingResolver
    {
        private readonly Encoding defaultEncoding;


        public KlerksFileEncodingResolver(Encoding defaultEncoding)
        {
            this.defaultEncoding = defaultEncoding;
        }


        public Encoding ResolveFileEncoding(string filePath)
        {
            return KlerksSoftFileEncodingDetector.DetectTextFileEncoding(filePath, defaultEncoding);
        }
    }
}