﻿using System;
using System.Collections.Generic;
using Cloney.Core.SolutionCloners;

namespace Cloney.Core.Tests.SolutionCloners
{
    internal class FakeSolutionCloner : SolutionClonerBase, ISolutionCloner
    {
        public void CloneSolution(string sourcePath, string targetPath)
        {
            OnCloningBegun(new EventArgs());
            CurrentPath = "foo/bar";
            OnCurrentPathChanged(new EventArgs());
            OnCloningEnded(new EventArgs());
        }

        public void CloneSolution(string sourcePath, string targetPath, IEnumerable<string> excludeFolderPatterns, IEnumerable<string> excludeFilePatterns, IEnumerable<string> plainCopyFilePatterns)
        {
            throw new NotImplementedException();
        }
    }
}