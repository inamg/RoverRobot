using System;
using System.Collections.Generic;
using System.Drawing;
using Andromeda.RoverRobot.Validators;
using Microsoft.Extensions.Logging;

namespace Andromeda.RoverRobot
{
    /// <summary>
    /// Represents a rover, exposing present Position and Direction of the rover
    /// </summary>
    public class RobotRover : IRobotRover
    {
        public Point Position { get; private set; }
        public Direction Direction { get; private set; }

        private readonly ILogger _logger;
        
        private ITableTop _tableTop;
        
        private readonly IDictionary<Rotation, Action> _rotationActions;

        private readonly IDictionary<Direction, Action> _leftMoveActions;

        private readonly IDictionary<Direction, Action> _rightMoveActions;

        private readonly IDictionary<Direction, Action> _forwardMoveActions;

        public RobotRover(ILogger logger)
        {
            _logger = logger;
            
            _rotationActions = new Dictionary<Rotation, Action>
            {
                {Rotation.Left, () => _leftMoveActions[Direction].Invoke()},
                {Rotation.Right, () => _rightMoveActions[Direction].Invoke()}
            };
            
            _leftMoveActions = new Dictionary<Direction, Action>
            {
                {Direction.North, () => Direction = Direction.West},
                {Direction.East, () => Direction = Direction.North},
                {Direction.South, () => Direction = Direction.East},
                {Direction.West, () => Direction = Direction.South}
            };

            _rightMoveActions = new Dictionary<Direction, Action>
            {
                {Direction.North, () => Direction = Direction.East},
                {Direction.East, () => Direction = Direction.South},
                {Direction.South, () => Direction = Direction.West},
                {Direction.West, () => Direction = Direction.North}
            };

            _forwardMoveActions = new Dictionary<Direction, Action>
            {
                {Direction.North, ForwardNorth},
                {Direction.South, ForwardSouth},
                {Direction.East, ForwardEast},
                {Direction.West, ForwardWest}
            };
        }
        /// <summary>
        /// Places rover on passed position and direction.If deployment not possible logs the exception
        /// </summary>
        /// <param name="position">Position to put the rover on</param>
        /// <param name="direction">Initial direction of the rover</param>
        /// <param name="tableTop">tabletop on which to deploy the rover</param>
        public void Place(Point position, Direction direction, ITableTop tableTop)
        {
            if (tableTop.IsValidPoint(position))
            {
                Position = position;
                Direction = direction;
                _tableTop = tableTop;
            }
            else
            {
                _logger.LogInformation($"Can not place rover on the specified position, as it will fall and die. Position  :: ({position.X}, {position.Y})");
            }
        }

        /// <summary>
        /// Moves the rover one step forward based on the direction and if it is not safe to move rover ignores the command
        /// </summary>
        public void Move()
        {
            if (_tableTop != null) // checks if rover is placed
            {
                _forwardMoveActions[Direction].Invoke();
            }
            else
            {
                _logger.LogInformation("Rover is not yet placed on the table top.So cannot move.");
            }
        }
        
        /// <summary>
        /// rotates the rover
        /// </summary>
        /// <param name="rotation"></param>

        public void Rotate(Rotation rotation)
        {
            Check.NotNull(rotation, nameof(rotation));

            if (_tableTop != null)
            {
                _rotationActions[rotation].Invoke();
            }
            else
            {
                _logger.LogInformation("Rover is not yet placed on the table top.So cannot rotate.");
            }
        }

        private void ForwardNorth()
        {
            var newPosition = new Point(Position.X, Position.Y + 1);

            if (_tableTop.IsValidPoint(newPosition))
            {
                Position = newPosition;
            }
            else
            {
                _logger.LogInformation($"Can't move Rover. Invalid new co-ordinates -{newPosition}");
            }
        }

        private void ForwardSouth()
        {
            var newPosition = new Point(Position.X, Position.Y - 1);

            if (_tableTop.IsValidPoint(newPosition))
            {
                Position = newPosition;
            }
            else
            {
                _logger.LogInformation($"Can't move Rover. Invalid new co-ordinates -{newPosition}");
            }
        }

        private void ForwardEast()
        {
            var newPosition = new Point(Position.X + 1, Position.Y);

            if (_tableTop.IsValidPoint(newPosition))
            {
                Position = newPosition;
            }
            else
            {
                _logger.LogInformation($"Can't move Rover. Invalid new co-ordinates -{newPosition}");
            }
        }

        private void ForwardWest()
        {
            var newPosition = new Point(Position.X - 1, Position.Y);

            if (_tableTop.IsValidPoint(newPosition))
            {
                Position = newPosition;
            }
            else
            {
                _logger.LogInformation($"Can't move Rover. Invalid new co-ordinates -{newPosition}");
            }
        }
    }
}
