using System.Drawing;
using Andromeda.RoverRobot.Validators;

namespace Andromeda.RoverRobot
{
    /// <summary>
    /// Represents a Table top, exposing Size of the Table Top
    /// which in our case is 5X5
    /// </summary>
    public class TableTop : ITableTop
    {
        public TableTop(int height, int width)
        {
            Check.IsGreaterThan(height, 0, nameof(height));
            Check.IsGreaterThan(width, 0, nameof(width));
            
            Size = new Size(width, height);
        }
        public Size Size { get; }

        /// <summary>
        /// Checks if the point is valid.
        /// </summary>
        /// <param name="point">Point to be checked</param>
        /// <returns>true or false</returns>
        public bool IsValidPoint(Point point)
        {
            var isValidX = point.X >= 0 && point.X < Size.Width;
            var isValidY = point.Y >= 0 && point.Y < Size.Height;

            return isValidX && isValidY;
        }
    }
}
