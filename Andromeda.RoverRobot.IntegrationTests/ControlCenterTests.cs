using System;
using System.Text;
using Andromeda.RoverRobot.Commands;
using Andromeda.RoverRobot.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Andromeda.RoverRobot.IntegrationTests
{
    // In real life i Would write many more integration Tests
    public class ControlCenterTests
    {
        private readonly ControlCenter _controlCenter;

        public ControlCenterTests()
        {
            var commandMatcher = new CommandTypeMatcher();
            var provider = ConfigureServices();
            var commandParser = new InputCommandParser(commandMatcher, provider);

            _controlCenter = new ControlCenter(commandParser, Substitute.For<ITableTop>(),
                Substitute.For<ILogger<ControlCenter>>());
        }

        [Fact]
        public void ExecuteCommand_WhenCommandsAreValid_ShouldReturnCorrectRovers()
        {
            //Act
            _controlCenter.ExecuteCommands(BuildCommandString());
            var rover = _controlCenter.Rover;

            //Assert
            Assert.Equal(3, rover.Position.X);
            Assert.Equal(3, rover.Position.Y);
            Assert.Equal(Direction.North, rover.Direction);
        }

        private static string BuildCommandString()
        {
            var commands = new StringBuilder();

            commands.AppendLine("PLACE 1,2,EAST");
            commands.AppendLine("MOVE");
            commands.AppendLine("MOVE");
            commands.AppendLine("LEFT");
            commands.AppendLine("MOVE");

            return commands.ToString();
        }

        private static IServiceProvider ConfigureServices()
        {
            //setup DI
            var serviceProvider = new ServiceCollection()
                .AddTransient<IControlCenter, ControlCenter>()
                .AddSingleton<IInputParser, InputCommandParser>()
                .AddSingleton<ICommandTypeMatcher, CommandTypeMatcher>()
                .AddSingleton<ConsoleInputHandler>()
                .AddSingleton<FileInputHandler>()
                .AddSingleton<IOutputComposer, ConsoleOutputComposer>()
                .AddTransient<PlaceRoverCommandParser>()
                .AddTransient<RotateRoverCommandParser>()
                .AddTransient<MoveRoverCommandParser>()
                .AddTransient<ReportRoverCommandParser>()
                .AddTransient<ITableTop>(x =>
                    new TableTop(5, 5))
                .BuildServiceProvider();


            return serviceProvider;
        }
    }
}