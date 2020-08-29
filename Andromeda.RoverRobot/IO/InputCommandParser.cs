using System;
using System.Collections.Generic;
using Andromeda.RoverRobot.Commands;
using Andromeda.RoverRobot.Validators;

namespace Andromeda.RoverRobot.IO
{
    /// <summary>
    /// Parses the input command
    /// </summary>
    public class InputCommandParser : IInputParser
    {
        private readonly ICommandTypeMatcher _commandTypeMatcher;

        private readonly IDictionary<CommandType, ICommandParser> _commandParsers;

        public InputCommandParser(ICommandTypeMatcher commandTypeMatcher, IServiceProvider serviceProvider)
        {
            Check.NotNull(commandTypeMatcher, nameof(commandTypeMatcher));
            Check.NotNull(serviceProvider, nameof(serviceProvider));

            _commandTypeMatcher = commandTypeMatcher;

            _commandParsers = new Dictionary<CommandType, ICommandParser>
            {
                {CommandType.Place, (PlaceRoverCommandParser)serviceProvider.GetService(typeof(PlaceRoverCommandParser))},
                {CommandType.Move, (MoveRoverCommandParser)serviceProvider.GetService(typeof(MoveRoverCommandParser))},
                {CommandType.Rotate,(RotateRoverCommandParser)serviceProvider.GetService(typeof(RotateRoverCommandParser))},
                {CommandType.Report,(ReportRoverCommandParser)serviceProvider.GetService(typeof(ReportRoverCommandParser))}
            };
        }

        public IReadOnlyList<ICommand> Parse(string commandString)
        {
            Check.NotNullOrEmpty(commandString, nameof(commandString));

            var commandsList = new List<ICommand>();
            var commands = commandString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (var command in commands)
            {
                if (!string.IsNullOrWhiteSpace(command))
                {
                    var commandType = _commandTypeMatcher.GetCommandType(command);

                    commandsList.Add(_commandParsers[commandType].Parse(command));
                }
            }

            return commandsList;
        }
    }
}
