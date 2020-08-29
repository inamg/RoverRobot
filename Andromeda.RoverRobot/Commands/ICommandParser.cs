namespace Andromeda.RoverRobot.Commands
{
    public interface ICommandParser
    {
        ICommand Parse(string commandString);
    }
}
