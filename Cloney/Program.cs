namespace Cloney
{
    /// <summary>
    /// This class will start the Cloney application that
    /// is defined in the Cloney library.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public class Program
    {
        private static void Main(string[] args)
        {
            new Core.Program().Start(args);
        }
    }
}
