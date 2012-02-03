using System;
using System.Collections.Generic;

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
    }
}