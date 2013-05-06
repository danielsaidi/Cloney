using System;

namespace Cloney.Core.Console
{
    ///<summary>
    /// This interface can be implemented by classes that
    /// can be used to work with the static Console class.
    /// It only contains some of the methods. If more are
    /// needed, simply add them to the interface.
    ///</summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/nextra
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
