using System.IO;
using System.Text;

namespace Cloney.Core.IO
{
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
