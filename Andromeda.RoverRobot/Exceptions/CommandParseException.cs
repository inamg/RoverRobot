using System;

namespace Andromeda.RoverRobot.Exceptions
{
    public class CommandParseException : Exception
    {
        public CommandParseException(string message) : base(message)
        {

        }
    }
}
