using System.Drawing;

namespace Andromeda.RoverRobot
{
    public interface IRobotRover
    {
        void Place(Point position, Direction direction, ITableTop tableTop);
        void Move();
        void Rotate(Rotation rotation);
        Point Position { get; }
        Direction Direction { get; }
    }
}
