using System;
using System.Drawing;
using Andromeda.RoverRobot.Validators;
using Andromeda.RoverRobot.Extensions;

namespace Andromeda.RoverRobot.IO
{
    /// <summary>
    /// Formats the output for console
    /// </summary>
    public class ConsoleOutputComposer : IOutputComposer
    {
        public void Compose(IRobotRover rover)
        {
            Check.NotNull(rover, nameof(rover));

            var output = Compose(rover.Position, rover.Direction);
            
            Console.WriteLine($"Rovers current position is : {output}");
        }

        private static string Compose(Point position, Direction direction)
        {
            return $"{position.X},{position.Y},{direction.GetDescription()}";
        }
    }
}
