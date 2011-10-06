using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Cloney.Core;
using Cloney.Core.Cloning;
using Cloney.Core.Cloning.Abstractions;
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
        private static ICanParseArguments argumentParser;
        private static IArgumentValidator argumentValidator;
        private static StringDictionary arguments;
        private static bool cloningInProgress;
        private static IConsole console;
        private static string currentPath;
        private static ICanExtractNamespace folderNamespaceExtractor;
        private static INamespaceArgumentValidator namespaceArgumentValidator;
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
                console.Write(String.Format(Resources.MainErrorExpression, e.Message));
            }
        }


        private static void Initialize(IEnumerable<string> args)
        {
            argumentParser = new ArgumentParser();
            argumentValidator = new RequiredArgumentValidator();
            arguments = argumentParser.ParseArguments(args);
            console = new ConsoleFacade();
            folderNamespaceExtractor = new FolderNamespaceExtractor();
            process = new ProcessFacade();
            solutionCloner = new ThreadedSolutionCloner(new SolutionCloner(CoreSettings.ExcludeFolderPatterns.AsEnumerable(), CoreSettings.ExcludeFilePatterns.AsEnumerable(), CoreSettings.PlainCopyFilePatterns.AsEnumerable()));
            solutionCloner.CloningBegun += solutionCloner_CloningBegun;
            solutionCloner.CloningEnded += solutionCloner_CloningEnded;
            solutionNamespaceExtractor = new SolutionFileNamespaceExtractor();
        }

        private static void Start()
        {
            if (StartWizard())
                return;

            argumentValidator.Validate(arguments, Resources.SourceFolderArgumentKey, "TODO: Create this message");
            argumentValidator.Validate(arguments, Resources.TargetFolderArgumentKey, "TODO: Create this message");

            var sourceFolder = arguments[Resources.SourceFolderArgumentKey];
            var targetFolder = arguments[Resources.TargetFolderArgumentKey];
            var sourceNamespace = solutionNamespaceExtractor.ExtractNamespace(sourceFolder);
            var targetNamespace = folderNamespaceExtractor.ExtractNamespace(targetFolder);

            //String.Format(Resources.InvalidSolutionFolderExpression, folderType)
            namespaceArgumentValidator.Validate(sourceNamespace, "TODO");
            namespaceArgumentValidator.Validate(targetNamespace, "TODO");

            solutionCloner.CloneSolution(sourceFolder, sourceNamespace, targetFolder, targetNamespace);

            cloningInProgress = true;
            while (cloningInProgress)
                UpdateCurrentPath();
        }

        private static bool StartWizard()
        {
            var wizard = new WizardApplicationFacade(console, process, arguments, Resources.StartingWizardMessage);
            return wizard.Start();
        }

        private static void UpdateCurrentPath()
        {
            if (solutionCloner.CurrentPath == currentPath)
                return;

            currentPath = solutionCloner.CurrentPath;
            console.WriteLine(currentPath);
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
