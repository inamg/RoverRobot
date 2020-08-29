using Andromeda.RoverRobot.Exceptions;
using Andromeda.RoverRobot.Validators;

namespace Andromeda.RoverRobot.Commands
{
    /// <summary>
    /// Represents MoveRoverCommand
    /// </summary>
    public class MoveRoverCommand : ISetRover
    {
        private IRobotRover _robotRover;

        public void Execute()
        {
            if (_robotRover == null)
            {
                throw new MoveRoverException($"Set Rover-{_robotRover} properly");
            }

            _robotRover.Move();
        }

        public void SetRover(IRobotRover robotRover)
        {
            Check.NotNull(robotRover, nameof(robotRover));

            _robotRover = robotRover;
        }
    }
}
