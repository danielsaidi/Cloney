using System;
using System.Collections.Generic;
using Cloney.Core;
using Cloney.Core.Abstractions;
using Cloney.Core.CommandLine;
using Cloney.Core.CommandLine.Abstractions;
using NExtra;
using NExtra.Abstractions;
using NExtra.Extensions;
using NExtra.Diagnostics;
using NExtra.Diagnostics.Abstractions;

namespace Cloney
{
    class Program
    {
        private static ICommandLineArgumentParser argumentParser;
        private static IDictionary<string,string> arguments;
        private static bool cloningInProgress;
        private static IConsole console;
        private static string currentPath;
        private static IFolderArgumentRetriever folderArgumentRetriever;
        private static ICanExtractNamespace folderNamespaceExtractor;
        private static IFolderNamespaceRetriever folderNamespaceRetriever;
        private static IProcess process;
        private static ICanCloneSolution solutionCloner;
        private static ICanExtractNamespace solutionNamespaceExtractor;


        static void Main(string[] args)
        {
            try
            {
                Initialize(args);
                Start();
            }
            catch (Exception e)
            {
                console.Write(String.Format(Language.MainErrorExpression, e.Message));
            }
        }



        private static void Initialize(IEnumerable<string> args)
        {
            argumentParser = new CommandLineArgumentParser();
            arguments = argumentParser.ParseCommandLineArguments(args);
            console = new ConsoleFacade();
            folderArgumentRetriever = new FolderArgumentRetriever();
            folderNamespaceExtractor = new FolderNamespaceExtractor();
            folderNamespaceRetriever = new FolderNamespaceRetriever();
            process = new ProcessFacade();
            solutionCloner = new ThreadedSolutionCloner(new SolutionCloner(CoreSettings.ExcludeFolderPatterns.AsEnumerable(), CoreSettings.ExcludeFilePatterns.AsEnumerable(), CoreSettings.PlainCopyFilePatterns.AsEnumerable()));
            solutionCloner.CloningEnded += solutionCloner_CloningEnded;
            solutionNamespaceExtractor = new SolutionFileNamespaceExtractor();
        }

        private static void Start()
        {
            if (StartWizard())
                return;

            var sourceFolder = folderArgumentRetriever.GetFolderArgumentValue(arguments, Language.SourceFolderArgumentName, Language.SourceFolderDisplayName, Language.FolderArgumentErrorMessage);
            var targetFolder = folderArgumentRetriever.GetFolderArgumentValue(arguments, Language.TargetFolderArgumentName, Language.TargetFolderDisplayName, Language.FolderArgumentErrorMessage);
            var sourceNamespace = folderNamespaceRetriever.GetFolderNamespace(solutionNamespaceExtractor, sourceFolder, Language.InvalidSolutionFolderExpression);
            var targetNamespace = folderNamespaceRetriever.GetFolderNamespace(folderNamespaceExtractor, targetFolder, Language.InvalidTargetFolderExpression);

            solutionCloner.CloneSolution(sourceFolder, sourceNamespace, targetFolder, targetNamespace);

            cloningInProgress = true;
            while (cloningInProgress)
                UpdateCurrentPath();
        }

        private static bool StartWizard()
        {
            var wizard = new WizardApplicationFacade(console, process, Language.WizardExecutable, arguments, Language.StartingWizardMessage);
            return wizard.Start();
        }

        private static void UpdateCurrentPath()
        {
            if (solutionCloner.CurrentPath == currentPath)
                return;

            currentPath = solutionCloner.CurrentPath;
            console.WriteLine(currentPath);
        }


        static void solutionCloner_CloningEnded(object sender, EventArgs e)
        {
            cloningInProgress = false;
        }
    }
}
