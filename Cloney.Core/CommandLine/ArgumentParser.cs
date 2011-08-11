using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using Cloney.Core.CommandLine.Abstractions;

namespace Cloney.Core.CommandLine
{
    /// <summary>
    /// This class can be used to parse any arguments that
    /// are passed into a command line application. It can
    /// be used to parse any argument collection where the
    /// argument names follow any of the following formats:
    /// 
    /// /name=Stefan
    /// --name=Stefan
    /// --name="Multiple words"
    /// -name 'Stefan'
    /// </summary>
    /// <remarks>
    /// The original implementation, made by Richard Lopes,
    /// has been refactored to fit the code conventions of
    /// the .NET Extensions library and is implementing an
    /// interface that describes its functionality.
    /// 
    /// The original implementation can be found here:
    /// http://www.codeproject.com/KB/recipes/command_line.aspx 
    /// </remarks>
    public class ArgumentParser : ICanParseArguments
    {
        /// <summary>
        /// Parse an argument collection.
        /// </summary>
        /// <param name="args">The argument collection to parse.</param>
        /// <returns>The resulting argument dictionary.</returns>
        public StringDictionary ParseArguments(IEnumerable<string> args)
        {
            var parameters = new StringDictionary();

            var splitter = new Regex(@"^-{1,2}|^/|=|:", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            var remover = new Regex(@"^['""]?(.*?)['""]?$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string parameter = null;
            string[] parts;

            // Valid parameters forms:
            // {-,/,--}param{ ,=,:}((",')value(",'))
            // Examples: 
            // -param1 value1 --param2 /param3:"Test-:-work" /param4=happy -param5 '--=nice=--'
            foreach (var arg in args)
            {
                // Look for new parameters (-,/ or --) and a possible enclosed value (=,:)
                parts = splitter.Split(arg, 3);

                switch (parts.Length)
                {
                    // Found a value (for the last parameter found (space separator))
                    case 1:
                        if (parameter != null && !parameters.ContainsKey(parameter))
                        {
                            parts[0] = remover.Replace(parts[0], "$1");
                            parameters.Add(parameter, parts[0]);
                        }
                        parameter = null;

                        // else Error: no parameter waiting for a value (skipped)
                        break;

                    // Found just a parameter
                    case 2:

                        // The last parameter is still waiting. With no value, set it to true.
                        if (parameter != null && !parameters.ContainsKey(parameter)) parameters.Add(parameter, "true");
                        parameter = parts[1];
                        break;

                    // Parameter with enclosed value
                    case 3:

                        // The last parameter is still waiting. With no value, set it to true.
                        if (parameter != null)
                        {
                            if (!parameters.ContainsKey(parameter))
                                parameters.Add(parameter, "true");
                        }

                        parameter = parts[1];

                        // Remove possible enclosing characters (",')
                        if (!parameters.ContainsKey(parameter))
                        {
                            parts[2] = remover.Replace(parts[2], "$1");
                            parameters.Add(parameter, parts[2]);
                        }

                        parameter = null;
                        break;
                }
            }

            if (parameter != null && !parameters.ContainsKey(parameter))
                parameters.Add(parameter, "true");

            return parameters;
        }
    }
}
