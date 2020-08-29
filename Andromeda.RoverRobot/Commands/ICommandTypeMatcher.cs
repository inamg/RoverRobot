namespace Andromeda.RoverRobot.Commands
{
    public interface ICommandTypeMatcher
    {
        CommandType GetCommandType(string command);
    }
}
