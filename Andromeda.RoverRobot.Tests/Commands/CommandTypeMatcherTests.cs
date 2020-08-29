using System;
using Andromeda.RoverRobot.Commands;
using Andromeda.RoverRobot.Exceptions;
using Xunit;

namespace Andromeda.RoverRobot.Tests.Commands
{
    public class CommandTypeMatcherTests
    {
        private readonly CommandTypeMatcher _matcher;

        public CommandTypeMatcherTests()
        {
            _matcher = new CommandTypeMatcher();
        }

        [Fact]
        public void GetCommandType_WhenNull_ShouldThrowException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => _matcher.GetCommandType(string.Empty));
        }

        [Theory]
        [InlineData("PLACE 0,0,EAST", CommandType.Place)]
        [InlineData("MOVE", CommandType.Move)]
        [InlineData("RIGHT", CommandType.Rotate)]
        [InlineData("REPORT", CommandType.Report)]

        public void GetCommandType_WhenCommandMatchesRegex_ShouldReturnCommandType(string command, CommandType result)
        {
            //Act
            var commandType = _matcher.GetCommandType(command);

            //Assert
            Assert.Equal(result, commandType);
        }

        [Theory]
        [InlineData("PLACE0,0,EAST")]
        [InlineData("MOVE123")]
        [InlineData("UP")]
        [InlineData("NOREPORT")]
        public void GetCommandType_WhenInValidCommand_ShouldThrowException(string command)
        {
            //Assert
            Assert.Throws<CommandParseException>(() => _matcher.GetCommandType(command));
        }
    }
}
