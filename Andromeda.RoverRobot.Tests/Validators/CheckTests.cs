using System;
using System.Text;
using Xunit;
using Andromeda.RoverRobot.Validators;

namespace Andromeda.RoverRobot.Tests.Validators
{
    public class CheckTests
    {
        [Fact]
        public void NotNull_WhenValueIsNull_ShowThrowsException()
        {
            //Arrange
            object obj = null;

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => Check.NotNull(obj, nameof(obj)));
        }
        
        [Fact]
        public void NotNull_WhenValueIsNotNull_ShowNotThrowsException()
        {
            //Arrange
            object obj = new StringBuilder();
            
            //Act & Assert
             Check.NotNull(obj, nameof(obj));
        }
        
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NotNullOrEmpty_WhenStringIsNullOrEmpty_ShowThrowsException(string value)
        {
            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => Check.NotNullOrEmpty(value, nameof(value)));
        }
        
        [Fact]
        public void NotNullOrEmpty_WhenStringIsNotNullOrEmpty_ShowNotThrowsException()
        {
            //Arrange
            var value = "data";
            
            //Act & Assert
            Check.NotNull(value, nameof(value));
        }

        [Fact]
        public void IsGreaterThan_WhenValueIsNotGreaterThan_ShouldThrowException()
        {
            // Arrange
            var value = -1; // should use autofaq here
            
            //Act & Assert
            Assert.Throws<ArgumentException>(() => Check.IsGreaterThan(value, 0,nameof(value)));
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        [InlineData(1)]
        public void IsGreaterThan_WhenValueIsGreaterThan_ShouldNotThrowException(int value)
        {
            //Act & Assert
            Check.IsGreaterThan(value, 0,nameof(value));
        }
    }
}