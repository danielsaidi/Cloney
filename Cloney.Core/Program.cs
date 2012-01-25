using System;
using System.Collections.Generic;
using NExtra;
using NExtra.Diagnostics;
using NExtra.Localization;

namespace Cloney.Core
{
    public class Program : IProgram
    {
        /*private static ICommandLineArgumentParser argumentParser;
        private static IDictionary<string,string> arguments;
        private static bool cloningInProgress;
        private static IConsole console;
        private static string currentPath;
        private static IFolderArgumentRetriever folderArgumentRetriever;
        private static ICanExtractNamespace folderNamespaceExtractor;
        private static IFolderNamespaceRetriever folderNamespaceRetriever;
        private static IProcess process;
        private static ICanCloneSolution solutionCloner;
        private static ICanExtractNamespace solutionNamespaceExtractor;*/


        public Program()
        {
            Console = new ConsoleFacade();
            ConsoleApplication = new ConsoleApplication();
            GuiApplication = new GuiApplication(new ProcessFacade());
            Translator = new ResourceManagerFacade(Language.ResourceManager);
        }


        public IConsole Console { get; set; }

        public IProgram ConsoleApplication { get; set; }

        public ITranslator Translator { get; set; }

        public IProgram GuiApplication { get; set; }


        public bool Start(IEnumerable<string> args)
        {
            try
            {
                ConsoleApplication.Start(args);
                GuiApplication.Start(args);
                throw new Exception("foo");
            }
            catch (Exception e)
            {
                Console.WriteLine(Translator.Translate("StartErrorMessage"));
                Console.WriteLine(e.Message);
                return false;
            }


            /*argumentParser = new CommandLineArgumentParser();
            arguments = argumentParser.ParseCommandLineArguments(args);
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
        }*/
    }
}
