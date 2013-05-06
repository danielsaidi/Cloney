using System.Collections.Generic;
using Cloney.Core.Console;

namespace Cloney.Core.Wizard
{
    /// <summary>
    /// This class defines app args for the Cloney Wizard.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://www.dotnextra.com
    /// </remarks>
    public class ApplicationCommandLineArgumentParser : ICommandLineArgumentParser<ApplicationArguments>
    {
        private readonly ICommandLineArgumentParser<IDictionary<string, string>> baseParser;


        public ApplicationCommandLineArgumentParser()
        {
            baseParser = Default.DictionaryCommandLineArgumentParser;
        }


        public ApplicationArguments ParseCommandLineArguments(IEnumerable<string> args)
        {
            var arguments = baseParser.ParseCommandLineArguments(args);

            var appArgs = new ApplicationArguments();
            ParseSourcePath(appArgs, arguments);
            ParseModalMode(appArgs, arguments);

            return appArgs;
        }


        private static void ParseModalMode(ApplicationArguments appArgs, IDictionary<string, string> arguments)
        {
            appArgs.ModalMode = arguments.ContainsKey("modal") && arguments["modal"] == "true" ? true : false;
        }

        private static void ParseSourcePath(ApplicationArguments appArgs, IDictionary<string, string> args)
        {
            appArgs.SourcePath = args.ContainsKey("source") ? args["source"] : null;
        }
    }
}
