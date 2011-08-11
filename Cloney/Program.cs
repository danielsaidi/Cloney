using System;
using System.Collections.Specialized;
using Cloney.Core;
using Cloney.Core.Abstractions;
using Cloney.Core.Cloning;
using Cloney.Core.Cloning.Abstractions;
using Cloney.Core.CommandLine;
using Cloney.Core.CommandLine.Abstractions;
using Cloney.Core.Diagnostics;
using Cloney.Core.Diagnostics.Abstractions;
using Cloney.Core.Extensions;

namespace Cloney
{
    class Program
    {
        private static ICanParseArguments argumentParser;
        private static bool cloningInProgress;
        private static IConsole console;
        private static string currentPath;
        private static ICanExtractNamespace folderNamespaceExtractor;
        private static IProcess process;
        private static ISettings coreSettings;
        private static ICanCloneSolution solutionCloner;
        private static ICanExtractNamespace solutionNamespaceExtractor;


        static void Main(string[] args)
        {
            try
            {
                Initialize();
                var arguments = argumentParser.ParseArguments(args);
                HandleWizard(arguments);
                Start(arguments);
            }
            catch (Exception e)
            {
                console.Write(String.Format("Error: {0}", e.Message));
            }
        }


        private static void AssertFolderArgument(StringDictionary arguments, string folderArgument, string folderType)
        {
            if (!arguments.ContainsKey(folderArgument) || arguments[folderArgument].Trim().IsNullOrEmpty())
                throw new ArgumentException(String.Format("Use --{0} <path> to specify a {1} folder", folderArgument, folderType));
        }

        private static void AssertNamespace(string @namespace, string folderType)
        {
            if (@namespace.IsNullOrEmpty())
                throw new ArgumentException(String.Format("The {0} folder does not contain a solution file", folderType));
        }

        private static void HandleCurrentPath()
        {
            if (solutionCloner.CurrentPath == currentPath)
                return;

            currentPath = solutionCloner.CurrentPath;
            console.WriteLine(currentPath);
        }

        private static void HandleWizard(StringDictionary arguments)
        {
            if (arguments.Count > 0)
                return;

            console.WriteLine("Starting the Cloney Wizard...");
            process.Start("Cloney.Wizard.exe");
        }

        private static void Initialize()
        {
            argumentParser = new ArgumentParser();
            console = new ConsoleFacade();
            coreSettings = new SettingsFacade();
            folderNamespaceExtractor = new FolderNamespaceExtractor();
            process = new ProcessFacade();
            solutionCloner = new ThreadedSolutionCloner(new SolutionCloner(coreSettings.ExcludeFolderPatterns.AsEnumerable(), coreSettings.ExcludeFilePatterns.AsEnumerable(), coreSettings.PlainCopyFilePatterns.AsEnumerable()));
            solutionCloner.CloningBegun += solutionCloner_CloningBegun;
            solutionCloner.CloningEnded += solutionCloner_CloningEnded;
            solutionNamespaceExtractor = new SolutionFileNamespaceExtractor();
        }

        private static void Start(StringDictionary arguments)
        {
            if (arguments.Count == 0)
                return;

            AssertFolderArgument(arguments, ArgumentConstants.SourceFolder, "source");
            AssertFolderArgument(arguments, ArgumentConstants.TargetFolder, "target");

            var sourceFolder = arguments[ArgumentConstants.SourceFolder];
            var targetFolder = arguments[ArgumentConstants.TargetFolder];
            var sourceNamespace = solutionNamespaceExtractor.ExtractNamespace(sourceFolder);
            var targetNamespace = folderNamespaceExtractor.ExtractNamespace(targetFolder);

            AssertNamespace(sourceNamespace, "source");
            AssertNamespace(targetNamespace, "target");

            solutionCloner.CloneSolution(sourceFolder, sourceNamespace, targetFolder, targetNamespace);

            cloningInProgress = true;
            while (cloningInProgress)
                HandleCurrentPath();
        }

        static void solutionCloner_CloningBegun(object sender, EventArgs e)
        {
        }

        static void solutionCloner_CloningEnded(object sender, EventArgs e)
        {
            cloningInProgress = false;
        }
    }
}
