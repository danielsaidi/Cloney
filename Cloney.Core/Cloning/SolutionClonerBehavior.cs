using System.Collections.Generic;
using Cloney.Core.IO;

namespace Cloney.Core.Cloning
{
    /// <summary>
    /// This class can be used to determine how a certain
    /// path is to be cloned, as well as other behavioral
    /// stuff that has nothing to do with the actual file
    /// and folder shuffling of the cloner.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public class SolutionClonerBehavior : ISolutionClonerBehavior
    {
        private readonly IPathPatternMatcher pathPatternMatcher;
        private readonly IEnumerable<string> excludeFolderPatterns;
        private readonly IEnumerable<string> excludeFilePatterns;
        private readonly IEnumerable<string> plainCopyFilePatterns;


        public SolutionClonerBehavior(IPathPatternMatcher pathPatternMatcher, IEnumerable<string> excludeFolderPatterns, IEnumerable<string> excludeFilePatterns, IEnumerable<string> plainCopyFilePatterns)
        {
            this.pathPatternMatcher = pathPatternMatcher;
            this.excludeFolderPatterns = excludeFolderPatterns;
            this.excludeFilePatterns = excludeFilePatterns;
            this.plainCopyFilePatterns = plainCopyFilePatterns;
        }


        public bool ShouldExcludeFolder(string folderPath)
        {
            return pathPatternMatcher.IsAnyMatch(folderPath, excludeFolderPatterns);
        }

        public bool ShouldExcludeFile(string filePath)
        {
            return pathPatternMatcher.IsAnyMatch(filePath, excludeFilePatterns);
        }

        public bool ShouldPlainCopyFile(string filePath)
        {
            return pathPatternMatcher.IsAnyMatch(filePath, plainCopyFilePatterns);
        }
    }
}