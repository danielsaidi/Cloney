using System.IO;
using System.Text;

namespace Cloney.Core.IO
{
    /// <summary>
    /// This class can be used to resolve file encodings,
    /// using a StreamReader instance. It is very faulty,
    /// though, since it cna only detect a narrow set of
    /// text encoding formats.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public class StreamReaderFileEncodingResolver : IFileEncodingResolver
    {
        public Encoding ResolveFileEncoding(string filePath)
        {
            Encoding result;
            using (var reader = new StreamReader(filePath, true))
            {
                reader.Peek();
                result = reader.CurrentEncoding;
                reader.Close();
            }
            return result;
        }
    }
}
