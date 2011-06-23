using System;

namespace Cloney.Domain.Cloning.Abstractions
{
    public interface ICanCloneSolution
    {
        event EventHandler CloningBegun;
        event EventHandler CloningEnded;

        String CurrentPath { get; }

        void CloneSolution(string sourcePath, string sourceNamespace, string targetPath, string targetNamespace);
    }
}