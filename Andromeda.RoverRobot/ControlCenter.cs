using System;
using System.Reflection;
using Andromeda.RoverRobot.Commands;
using Andromeda.RoverRobot.IO;
using Andromeda.RoverRobot.Validators;
using Microsoft.Extensions.Logging;

namespace Andromeda.RoverRobot
{
    /// <summary>
    /// Executes commands from input
    /// </summary>
    public class ControlCenter : IControlCenter
    {
        private readonly IInputParser _commandParser;

        public IRobotRover Rover { get; }

        private readonly ITableTop _tableTop;
        private readonly ILogger<ControlCenter> _logger;

        public ControlCenter(IInputParser commandParser,
            ITableTop tableTop, ILogger<ControlCenter> logger)
        {
            Check.NotNull(commandParser, nameof(commandParser));
            Check.NotNull(tableTop, nameof(tableTop));
            _commandParser = commandParser;
            
            _logger = logger;
            Rover = new RobotRover(_logger);
            _tableTop = tableTop;
        }

        /// <summary>
        /// Parses and executes the commands pass as string
        /// </summary>
        /// <param name="commandStrings">Multiple commands as string</param>
        /// <returns>List of rovers</returns>
        public void ExecuteCommands(string commandStrings)
        {
            Check.NotNullOrEmpty(commandStrings, nameof(commandStrings));

            var commands = _commandParser.Parse(commandStrings.ToUpper());

            foreach (var command in commands)
            {
                switch (command)
                {
                    case PlaceRoverCommand roverCommand:
                        roverCommand.SetRover(Rover);
                        roverCommand.SetTableTop(_tableTop);
                        break;
                    case MoveRoverCommand roverCommand:
                        roverCommand.SetRover(Rover);
                        break;
                    case RotateRoverCommand roverCommand:
                        roverCommand.SetRover(Rover);
                        break;
                    case ReportRoverCommand roverCommand:
                        roverCommand.SetRover(Rover);
                        break;
                    default:
                        throw new Exception("Invalid Command exception");
                }

                command.Execute();
            }
        }
    }
}