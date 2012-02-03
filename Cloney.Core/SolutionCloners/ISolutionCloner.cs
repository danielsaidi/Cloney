using System;

namespace Cloney.Core.SolutionCloners
{
    /// <summary>
    /// This interface can be implemented by classes that
    /// can be used to clone Visual Studio solutions from
    /// a source to a target folder.
    /// </summary>
    public interface ISolutionCloner
    {
        event EventHandler CloningBegun;
        event EventHandler CloningEnded;
        event EventHandler CurrentPathChanged;

        String CurrentPath { get; }

        void CloneSolution(string sourcePath, string targetPath);
        void OnCloningBegun(EventArgs e);
        void OnCloningEnded(EventArgs e);
        void OnCurrentPathChanged(EventArgs e);
    }
}