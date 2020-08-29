using Andromeda.RoverRobot.IO;
using Andromeda.RoverRobot.Validators;

namespace Andromeda.RoverRobot.Commands
{
    /// <summary>
    /// Parses the command and returns appropriate domain object
    /// </summary>
    public class ReportRoverCommandParser : ICommandParser
    {
        private readonly IOutputComposer _outputComposer;

        public ReportRoverCommandParser(IOutputComposer outputComposer)
        {
            _outputComposer = outputComposer;
        }

        public ICommand Parse(string commandString)
        {
            Check.NotNullOrEmpty(commandString, nameof(commandString));

            return new ReportRoverCommand(_outputComposer);
        }
    }
}