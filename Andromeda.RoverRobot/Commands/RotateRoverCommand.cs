using Andromeda.RoverRobot.Exceptions;
using Andromeda.RoverRobot.Validators;

namespace Andromeda.RoverRobot.Commands
{
    public class RotateRoverCommand : ISetRover
    {
        private IRobotRover _robotRover;
        private readonly Rotation _rotation;

        public RotateRoverCommand(Rotation rotation)
        {
            _rotation = rotation;
        }

        public void Execute()
        {
            if (_robotRover == null)
            {
                throw new RotateRoverException($"Set Rover-{_robotRover} properly");
            }
            
            _robotRover.Rotate(_rotation);
            
        }

        public void SetRover(IRobotRover robotRover)
        {
            Check.NotNull(robotRover, nameof(robotRover));
            
            _robotRover = robotRover;
        }
    }
}