using Andromeda.RoverRobot.Exceptions;
using Andromeda.RoverRobot.IO;
using Andromeda.RoverRobot.Validators;

namespace Andromeda.RoverRobot.Commands
{

    public class ReportRoverCommand : ISetRover
    {
        private IRobotRover _robotRover;
        private readonly IOutputComposer _outputComposer;

        public ReportRoverCommand(IOutputComposer outputComposer)
        {
            _outputComposer = outputComposer;
        }

        public void Execute()
        {
            if (_robotRover == null)
            {
                throw new ReportRoverException($"Set Rover-{_robotRover} properly");
            }

            _outputComposer.Compose(_robotRover);
        }

        public void SetRover(IRobotRover robotRover)
        {
            Check.NotNull(robotRover, nameof(robotRover));

            _robotRover = robotRover;
        }
    }
}
