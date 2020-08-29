using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Andromeda.RoverRobot.Commands;
using Andromeda.RoverRobot.IO;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Andromeda.RoverRobot.Tests
{
    // In real world I would write many more unit tests
    public class ControlCenterTests
    {
        private readonly ControlCenter _controlCenter;
        private readonly IInputParser _parser;

        public ControlCenterTests()
        {
            _parser = Substitute.For<IInputParser>();
            ITableTop tableTop = new TableTop(5, 5);
            var logger = Substitute.For<ILogger<ControlCenter>>();

            _controlCenter = new ControlCenter(_parser, tableTop, logger);
        }

        [Fact]
        public void ExecuteCommand_WhenStringCommandIsEmpty_ShouldThrowException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => _controlCenter.ExecuteCommands(null));
        }

        [Fact]
        public void ExecuteCommand_WhenCommandsAreValid_ShouldReturnCorrectRovers()
        {
            //Arrange
            var commands = new List<ICommand>
            {
                new PlaceRoverCommand(new Point(1, 2), Direction.North),
                new MoveRoverCommand(),
                new RotateRoverCommand(Rotation.Left)
            };

            var input = new StringBuilder();
            input.AppendLine("PLACE 1,2,NORTH");
            input.AppendLine("MOVE");
            input.AppendLine("LEFT");

            _parser.Parse(input.ToString()).Returns(commands);

            //Act
            _controlCenter.ExecuteCommands(input.ToString());

            //Assert
            var rover = _controlCenter.Rover;
            Assert.NotNull(rover);
            Assert.Equal(Direction.West, rover.Direction);
            Assert.Equal(1, rover.Position.X);
            Assert.Equal(3, rover.Position.Y);
        }
        
        [Fact]
        public void ExecuteCommand_WhenPlaceIsNotFirstCommand_ShouldIgnoreCommandsBeforePlace()
        {
            //Arrange
            var commands = new List<ICommand>
            {
                new MoveRoverCommand(),
                new RotateRoverCommand(Rotation.Left),
                new PlaceRoverCommand(new Point(1, 2), Direction.North),
                new MoveRoverCommand(),
                new RotateRoverCommand(Rotation.Left)
            };

            var input = new StringBuilder();
            input.AppendLine("PLACE 1,2,NORTH");
            input.AppendLine("MOVE");
            input.AppendLine("LEFT");
            input.AppendLine("MOVE");
            input.AppendLine("LEFT");

            _parser.Parse(input.ToString()).Returns(commands);

            //Act
            _controlCenter.ExecuteCommands(input.ToString());

            //Assert
            var rover = _controlCenter.Rover;
            Assert.NotNull(rover);
            Assert.Equal(Direction.West, rover.Direction);
            Assert.Equal(1, rover.Position.X);
            Assert.Equal(3, rover.Position.Y);
        }
    }
}