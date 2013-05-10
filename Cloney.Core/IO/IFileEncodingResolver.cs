using System.Text;

namespace Cloney.Core.IO
{
    public interface IFileEncodingResolver
    {
        Encoding ResolveFileEncoding(string filePath);
    }
}
