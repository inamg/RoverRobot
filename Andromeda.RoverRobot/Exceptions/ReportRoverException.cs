using System;

namespace Andromeda.RoverRobot.Exceptions
{
    public class ReportRoverException : Exception
    {
        public ReportRoverException(string message) : base(message)
        {}
    }
}