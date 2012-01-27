﻿using System;
using System.Collections.Generic;
using Cloney.Core.SubRoutines;
using NExtra;
using NExtra.Diagnostics;
using NExtra.Localization;

namespace Cloney.Core
{
    /// <summary>
    /// This class represents the main Cloney application.
    /// It will either trigger the console or the GUI app,
    /// according to the input argument it receives.
    /// </summary>
    public class Program : IProgram
    {
        /*
        private static IDictionary<string,string> arguments;
        private static bool cloningInProgress;
        private static string currentPath;
        private static IFolderArgumentRetriever folderArgumentRetriever;
        private static ICanExtractNamespace folderNamespaceExtractor;
        private static IFolderNamespaceRetriever folderNamespaceRetriever;
        private static ICanCloneSolution solutionCloner;
        private static ICanExtractNamespace solutionNamespaceExtractor;*/


        public Program()
            : this(new ConsoleFacade(), new Console.Program(new ConsoleFacade(), new CommandLineArgumentParser(), new LocalSubRoutineLocator()), new Gui.Program(new ProcessFacade()), new LanguageProvider())
        {
        }

        public Program(IConsole console, IProgram consoleProgram, IProgram guiProgram, ITranslator translator)
        {
            Console = console;
            ConsoleProgram = consoleProgram;
            GuiProgram = guiProgram;
            Translator = translator;
        }


        private IConsole Console { get; set; }

        private IProgram ConsoleProgram { get; set; }

        private IProgram GuiProgram { get; set; }

        private ITranslator Translator { get; set; }


        public bool Start(IEnumerable<string> args)
        {
            try
            {
                return ConsoleProgram.Start(args) || GuiProgram.Start(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(Translator.Translate("StartErrorMessage"));
                Console.WriteLine(e.Message);
                return false;
            }


            /*
            console = new ConsoleFacade();
            folderArgumentRetriever = new FolderArgumentRetriever();
            folderNamespaceExtractor = new FolderNamespaceExtractor();
            folderNamespaceRetriever = new FolderNamespaceRetriever();
            process = new ProcessFacade();
            solutionCloner = new ThreadedSolutionCloner(new SolutionCloner(CoreSettings.ExcludeFolderPatterns.AsEnumerable(), CoreSettings.ExcludeFilePatterns.AsEnumerable(), CoreSettings.PlainCopyFilePatterns.AsEnumerable()));
            solutionCloner.CloningEnded += solutionCloner_CloningEnded;
            solutionNamespaceExtractor = new SolutionFileNamespaceExtractor();*/
        }
        /*
        private static void Start()
        {
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
        }*/
    }
}
