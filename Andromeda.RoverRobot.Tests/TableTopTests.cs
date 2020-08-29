using System.Drawing;
using AutoFixture;
using Xunit;

namespace Andromeda.RoverRobot.Tests
{
    public class TableTopTests
    {
        private readonly Fixture _fixture;

        public TableTopTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void TableTop_WhenProvidedWithHeightWidth_ShouldSetCorrectSize()
        {
            //Arrange
            _fixture.Customizations.Add(
                new RandomNumericSequenceGenerator(0, 100));
            var width = _fixture.Create<int>();
            var height = _fixture.Create<int>();
            
            //Act
            var tableTop = new TableTop(height,width);
            
            //Assert
            Assert.Equal(height, tableTop.Size.Height);
            Assert.Equal(width, tableTop.Size.Width);
        }

        [Theory]
        [InlineData(0,0,5,5)]
        [InlineData(4,4,5,5)]
        [InlineData(0,1,5,5)]
        [InlineData(2,3,5,5)]
        [InlineData(3,3,5,5)]
        [InlineData(3,2,3,4)]
        public void IsValidPoint_WhenPointFallsBetweenSize_ShouldReturnTrue(int x, int y, int height, int width)
        {
            //Arrange
            var tableTop = new TableTop(height,width);
            
            //Act
            var isValid = tableTop.IsValidPoint(new Point(x, y));

            //Assert
            Assert.True(isValid);
        }
        
        [Theory]
        [InlineData(-1,-1,5,5)]
        [InlineData(5,5,5,5)]
        [InlineData(6,1,5,5)]
        [InlineData(3,6,5,5)]
        [InlineData(0,-1,5,5)]
        [InlineData(3,4,3,4)]
        public void IsValidPoint_WhenPointFallsOutOfSize_ShouldReturnFalse(int x, int y, int height, int width)
        {
            //Arrange
            var tableTop = new TableTop(height,width);
            
            //Act
            var isValid = tableTop.IsValidPoint(new Point(x, y));

            //Assert
            Assert.False(isValid);
        }
    }
}