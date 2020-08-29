using System.Drawing;

namespace Andromeda.RoverRobot
{
    public interface ITableTop
    {
        Size Size { get; }
        bool IsValidPoint(Point position);
    }
}
