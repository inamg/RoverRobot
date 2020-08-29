using System.Drawing;
using Andromeda.RoverRobot.Exceptions;
using Andromeda.RoverRobot.Validators;

namespace Andromeda.RoverRobot.Commands
{
    /// <summary>
    /// Represents PlaceRoverCommand
    /// </summary>
    public class PlaceRoverCommand : ISetRover, ISetTableTop
    {
        private readonly Point _position;

        private readonly Direction _direction;

        private IRobotRover _robotRover;

        private ITableTop _tableTop;

        public PlaceRoverCommand(Point position, Direction direction)
        {
            Check.NotNull(position, nameof(position));
            Check.NotNull(direction, nameof(direction));

            _position = position;
            _direction = direction;
        }

        public void Execute()
        {
            if (_robotRover == null || _tableTop == null)
            {
                throw new PlaceRoverException($"Set Rover-{_robotRover} and tabletop-{_tableTop} properly");
            }
            
            _robotRover.Place(_position, _direction, _tableTop);
        }

        public void SetRover(IRobotRover robotRover)
        {
            Check.NotNull(robotRover, nameof(robotRover));

            _robotRover = robotRover;
        }

        public void SetTableTop(ITableTop tableTop)
        {
            Check.NotNull(tableTop, nameof(tableTop));

            _tableTop = tableTop;
        }
    }
}
