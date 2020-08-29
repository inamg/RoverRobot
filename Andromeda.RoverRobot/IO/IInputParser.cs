using System.Collections.Generic;
using Andromeda.RoverRobot.Commands;

namespace Andromeda.RoverRobot.IO
{
    public interface IInputParser
    {
        IReadOnlyList<ICommand> Parse(string inputString);
    }
}
