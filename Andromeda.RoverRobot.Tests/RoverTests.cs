using System.Drawing;
using Andromeda.RoverRobot.Exceptions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;


namespace Andromeda.RoverRobot.Tests
{
    // in real world. I would write many more tests, with ArgumentNullException, multiple movements etc
    public class RoverTests
    {
        private readonly RobotRover _rover;

        public RoverTests()
        {
            var logger = Substitute.For<ILogger>();
            _rover = new RobotRover(logger);
        }

        [Theory]
        [InlineData(1, 1, Direction.North)]
        [InlineData(2, 1, Direction.North)]
        [InlineData(2, 1, Direction.South)]
        [InlineData(2, 1, Direction.East)]
        [InlineData(2, 2, Direction.West)]
        public void Deploy_WhenPositionIsValid_ShouldUpdateDirectionPosition(int x, int y, Direction direction)
        {
            //Arrange
            var position = new Point(x, y);
            var tableTop = Substitute.For<ITableTop>();
            tableTop.IsValidPoint(position).Returns(true);

            // Act
            _rover.Place(position, direction, tableTop);

            //Assert
            Assert.Equal(direction, _rover.Direction);
            Assert.Equal(position.X, _rover.Position.X);
            Assert.Equal(position.Y, _rover.Position.Y);
        }

        [Theory]
        [InlineData(Direction.South, Rotation.Left, Direction.East)]
        [InlineData(Direction.North, Rotation.Left, Direction.West)]
        [InlineData(Direction.North, Rotation.Right, Direction.East)]
        [InlineData(Direction.South, Rotation.Right, Direction.West)]
        public void Move_WhenRotation_ShouldUpdateDirection(Direction initial, Rotation input, Direction result)
        {
            //Arrange
            var position = new Point(2, 2);
            var tableTop = Substitute.For<ITableTop>();
            tableTop.IsValidPoint(position).Returns(true);
            _rover.Place(position,initial, tableTop);

            //Act
            _rover.Rotate(input);

            //Assert
            Assert.Equal(result, _rover.Direction);
        }

        [Theory]
        [InlineData(1,1,Direction.North,1,2)]
        [InlineData(2, 2, Direction.South, 2, 1)]
        [InlineData(1, 1, Direction.East, 2, 1)]
        [InlineData(2, 2, Direction.West, 1, 2)]
        public void Move_WhenMoveCommand_ShouldUpdatePosition(
            int initialX,
            int initialY,
            Direction initial,
            int finalX,
            int finalY)
        {
            //Arrange
            var position = new Point(initialX, initialY);
            var tableTop = Substitute.For<ITableTop>();
            tableTop.IsValidPoint(Arg.Any<Point>()).Returns(true);
            _rover.Place(position, initial, tableTop);

            //Act
            _rover.Move();

            //Assert
            Assert.Equal(finalX, _rover.Position.X);
            Assert.Equal(finalY, _rover.Position.Y);
        }
    }
}
