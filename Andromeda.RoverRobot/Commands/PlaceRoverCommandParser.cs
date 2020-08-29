using System;
using System.Drawing;
using Andromeda.RoverRobot.Exceptions;
using Andromeda.RoverRobot.Utils;
using Andromeda.RoverRobot.Validators;

namespace Andromeda.RoverRobot.Commands
{
    /// <summary>
    /// Parses the command and returns appropriate domain object
    /// </summary>
    public class PlaceRoverCommandParser : ICommandParser
    {
        public ICommand Parse(string commandString)
        {
            Check.NotNullOrEmpty(commandString, nameof(commandString));

            //Only adding try catch because class is public. so there is no guarantee commandString will be correct
            try
            {
                // split the command into parts, first part will be the command and second part will hae co-ordinates and position
                // separated by ,
                var commandParts = commandString.Split(' ');

                // first part should be PLACE and second part should be 'X,Y,F'
                var secondPart = commandParts[1];
                var arguments = secondPart.Split(',');
                var x = int.Parse(arguments[0]);
                var y = int.Parse(arguments[1]);

                var position = new Point(x, y);
                var direction = EnumUtils.GetEnumFromDescription<Direction>(arguments[2]);

                return new PlaceRoverCommand(position, direction);
            }
            catch (Exception exp)
            {
                throw new CommandParseException($"Invalid command {commandString} for command type PlaceRoverCommand. Detailed exception {exp.Message}");
            }

        }
    }
}
