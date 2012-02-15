using System;

namespace Cloney.Cloners
{
    /// <summary>
    /// This interface can be implemented by classes that
    /// can be used to clone Visual Studio solutions from
    /// a source to a target folder.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public interface ISolutionCloner
    {
        event EventHandler CloningBegun;
        event EventHandler CloningEnded;
        event EventHandler CurrentPathChanged;

        String CurrentPath { get; }

        void CloneSolution(string sourcePath, string targetPath);
    }
}