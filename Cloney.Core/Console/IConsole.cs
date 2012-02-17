using System;

namespace Cloney.Core.Console
{
    ///<summary>
    /// This interface can be implemented by classes that
    /// can be used to work with the static Console class.
    ///</summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://www.dotnextra.com
    /// </remarks>
    public interface IConsole
    {
        int Read();
        ConsoleKeyInfo ReadKey();
        string ReadLine();
        void Write(string value);
        void WriteLine(string value);
    }
}
