using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Andromeda.RoverRobot.Exceptions;
using Andromeda.RoverRobot.Validators;

namespace Andromeda.RoverRobot.Commands
{
    /// <summary>
    /// Matches the commands based on regex.
    /// </summary>
    public class CommandTypeMatcher : ICommandTypeMatcher
    {
        private readonly IDictionary<string, CommandType> _commandTypeRegexs;

        public CommandTypeMatcher()
        {
            _commandTypeRegexs = new Dictionary<string, CommandType>
            {
                { @"^PLACE \d+,\d+,(NORTH|SOUTH|EAST|WEST)$", CommandType.Place },
                { @"^MOVE$", CommandType.Move },
                { @"^(LEFT|RIGHT)$", CommandType.Rotate },
                { @"^REPORT$", CommandType.Report }
            };
        }

        /// <summary>
        /// Returns the matched CommandType
        /// </summary>
        /// <param name="command">Command string</param>
        /// <returns>CommandType enum</returns>
        public CommandType GetCommandType(string command)
        {
            Check.NotNullOrEmpty(command, nameof(command));

            try
            {
                var commandType = _commandTypeRegexs.First(item => new Regex(item.Key).IsMatch(command));

                return commandType.Value;
            }
            catch (InvalidOperationException exp)
            {
                throw new CommandParseException($"Invalid command format-{command} : detailed message exception {exp}");
            }
        }
    }
}
