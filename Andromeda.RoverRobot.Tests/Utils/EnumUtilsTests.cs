using System;
using Andromeda.RoverRobot.Utils;
using Xunit;

namespace Andromeda.RoverRobot.Tests.Utils
{
    public class EnumUtilsTests
    {
        [Theory]
        [InlineData("North", Direction.North)]
        [InlineData("SOUTH", Direction.South)]
        [InlineData("EAST", Direction.East)]
        [InlineData("WeST", Direction.West)]
        public void GetEnumFromDescription_WhenEnumHasDescription_ShouldReturnEnum(string value, Direction direction)
        {
            //Act
            var enumValue = EnumUtils.GetEnumFromDescription<Direction>(value);
            
            //Assert
            Assert.Equal(direction, enumValue);
        }
        
        [Fact]
        public void GetEnumFromDescription_WhenEnumDoesntHaveDescription_ShouldThrowException()
        {
            //Act && ASSERT
            Assert.Throws<ArgumentException>(() => EnumUtils.GetEnumFromDescription<Direction>("abctest"));
        }
    }
}