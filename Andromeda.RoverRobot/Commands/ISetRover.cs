namespace Andromeda.RoverRobot.Commands
{
    public interface ISetRover : ICommand
    {
        void SetRover(IRobotRover robotRover);
    }
}
