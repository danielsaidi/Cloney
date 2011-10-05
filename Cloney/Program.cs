using System;
using System.Collections.Specialized;
using Cloney.Core;
using Cloney.Core.Abstractions;
using Cloney.Core.Cloning;
using Cloney.Core.Cloning.Abstractions;
using Cloney.Core.CommandLine;
using Cloney.Core.CommandLine.Abstractions;
using Cloney.Core.Extensions;
using NExtra;
using NExtra.Abstractions;
using NExtra.Diagnostics;
using NExtra.Diagnostics.Abstractions;

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
                Start(argumentParser.ParseArguments(args));
            }
            catch (Exception e)
            {
                console.Write(String.Format(Resources.MainErrorExpression, e.Message));
            }
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
            if (StartWizard(arguments))
                return;

            ValidateFolderArgument(arguments, Resources.SourceFolderArgumentKey, Resources.Source);
            ValidateFolderArgument(arguments, Resources.TargetFolderArgumentKey, Resources.Target);

            var sourceFolder = arguments[Resources.SourceFolderArgumentKey];
            var targetFolder = arguments[Resources.TargetFolderArgumentKey];
            var sourceNamespace = solutionNamespaceExtractor.ExtractNamespace(sourceFolder);
            var targetNamespace = folderNamespaceExtractor.ExtractNamespace(targetFolder);

            ValidateNamespace(sourceNamespace, Resources.Source);
            ValidateNamespace(targetNamespace, Resources.Target);

            solutionCloner.CloneSolution(sourceFolder, sourceNamespace, targetFolder, targetNamespace);

            cloningInProgress = true;
            while (cloningInProgress)
                UpdateCurrentPath();
        }

        private static bool StartWizard(StringDictionary arguments)
        {
            if (arguments.Count > 0)
                return false;

            console.WriteLine(Resources.StartingWizardMessage);
            process.Start(Resources.WizardExecutable);
            return true;
        }

        private static void UpdateCurrentPath()
        {
            if (solutionCloner.CurrentPath == currentPath)
                return;

            currentPath = solutionCloner.CurrentPath;
            console.WriteLine(currentPath);
        }

        private static void ValidateFolderArgument(StringDictionary arguments, string folderArgument, string folderType)
        {
            if (!arguments.ContainsKey(folderArgument) || arguments[folderArgument].Trim().IsNullOrEmpty())
                throw new ArgumentException(String.Format(Resources.MissingFolderExpression, folderArgument, folderType));
        }

        private static void ValidateNamespace(string @namespace, string folderType)
        {
            if (@namespace.IsNullOrEmpty())
                throw new ArgumentException(String.Format(Resources.InvalidSolutionFolderExpression, folderType));
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
