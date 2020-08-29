using System;
using Andromeda.RoverRobot.Exceptions;
using Andromeda.RoverRobot.Utils;
using Andromeda.RoverRobot.Validators;

namespace Andromeda.RoverRobot.Commands
{
    /// <summary>
    /// Parses the string into MoveRoverCommand
    /// </summary>
    public class RotateRoverCommandParser : ICommandParser
    {
        public ICommand Parse(string commandString)
        {
            Check.NotNullOrEmpty(commandString, nameof(commandString));

            //Only adding try catch because class is public. so there is no guarantee commandString will be correct
            try
            {
                var rotation = EnumUtils.GetEnumFromDescription<Rotation>(commandString);
               
                return new RotateRoverCommand(rotation);
            }
            catch (Exception exp)
            {
                throw new CommandParseException($"Invalid command {commandString} for command type RotateRoverCommand. detailed Exception :{exp.Message}");
            }

        }
    }
}
