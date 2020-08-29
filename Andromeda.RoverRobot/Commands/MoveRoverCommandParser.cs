using Andromeda.RoverRobot.Validators;

namespace Andromeda.RoverRobot.Commands
{
    /// <summary>
    /// Parses the command and returns appropriate domain object
    /// </summary>
    public class MoveRoverCommandParser : ICommandParser
    {
        public ICommand Parse(string commandString)
        {
            Check.NotNullOrEmpty(commandString, nameof(commandString));

            return new MoveRoverCommand();
        }
    }
}