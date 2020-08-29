using System;
using System.Drawing;
using Andromeda.RoverRobot.Commands;
using Andromeda.RoverRobot.Exceptions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;


namespace Andromeda.RoverRobot.Tests.Commands
{
    public class DeployRoverCommandTests
    {
        private readonly PlaceRoverCommand _placeRoverCommand;
        private readonly IRobotRover _rover;

        public DeployRoverCommandTests()
        {
            _placeRoverCommand = new PlaceRoverCommand(new Point(2, 3), Direction.North);
            _rover = new RobotRover(Substitute.For<ILogger>());
        }

        [Fact]
        public void Execute_WhenRoverIsNotSet_ShouldThrowException()
        {
            //Assert
            Assert.Throws<PlaceRoverException>(() => _placeRoverCommand.Execute());
        }

        [Fact]
        public void Execute_WhenTableTopIsNotSet_ShouldThrowException()
        {
            //Arrange
            _placeRoverCommand.SetRover(_rover);

            //Assert
            Assert.Throws<PlaceRoverException>(() => _placeRoverCommand.Execute());
        }

        [Fact]
        public void Execute_PositionIsCorrect_ShouldDeployRover()
        {
            //Arrange
            _placeRoverCommand.SetRover(_rover);
            var tableTop = Substitute.For<ITableTop>();
            tableTop.IsValidPoint(Arg.Any<Point>()).Returns(true);
            tableTop.Size.Returns(new Size(5, 5));
            _placeRoverCommand.SetTableTop(tableTop);

            //Act
            _placeRoverCommand.Execute();

            //Assert
            Assert.Equal(2, _rover.Position.X);
            Assert.Equal(3, _rover.Position.Y);
            Assert.Equal(Direction.North, _rover.Direction);
        }
    }
}