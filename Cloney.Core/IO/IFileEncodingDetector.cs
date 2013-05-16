using System.Text;

namespace Cloney.Core.IO
{
    /// <summary>
    /// This interface can be implemented by classes that
    /// can be used to resolve the text encoding of files.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public interface IFileEncodingDetector
    {
        Encoding ResolveFileEncoding(string filePath);
    }
}
