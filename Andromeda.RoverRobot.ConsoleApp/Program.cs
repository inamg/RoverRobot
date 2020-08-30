using System;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Andromeda.RoverRobot.Commands;
using Andromeda.RoverRobot.Exceptions;
using Andromeda.RoverRobot.IO;

namespace Andromeda.RoverRobot.ConsoleApp
{
    class Program
    {
        private static ILogger<Program> _logger;
        private const int TableTopHeight = 5;
        private const int TableTopWidth = 5;

        static void Main(string[] args)
        {
            try
            {
                var serviceProvider = ConfigureServices();
                _logger = serviceProvider.GetService<ILogger<Program>>();
                IInputHandler handler;

                Console.WriteLine("Press 1 for console input and 2 for specifying file");
                var input = Console.ReadKey();
               
                if (input.KeyChar == '1')
                {
                    handler = serviceProvider.GetService<ConsoleInputHandler>();
                    handler.Handle();
                }
                else if(input.KeyChar == '2')
                {
                    handler = serviceProvider.GetService<FileInputHandler>();
                    handler.Handle();
                }
                else
                {
                    Console.WriteLine("Wrong input.");
                }
            }
            catch (PlaceRoverException e)
            {
                Console.WriteLine("Couldn't place rover. Please check the logs.");
                _logger.LogError(e.ToString());
            }
            catch (CommandParseException e)
            {
                Console.WriteLine("Couldn't parse command. Please check the logs. please see the sample input commands here https://github.com/inamg/RoverRobot#sample-input");
                _logger.LogError(e.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong. Please check the logs.");
                _logger.LogError(e.ToString());
            }
            finally
            {
		Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
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
                .AddTransient<ITableTop>(x=>
                    new TableTop(TableTopHeight, TableTopWidth))
                .AddLogging(configure => configure.AddConsole())
                .BuildServiceProvider();
            
            return serviceProvider;
        }

        private static string BuildCommandString()
        {
            var commands = new StringBuilder();
            commands.AppendLine("PLACE 0,0,NORTH");
            commands.AppendLine("MOVE");
            commands.AppendLine("REPORT");
            
            commands.AppendLine("PLACE 0,0,NORTH");
            commands.AppendLine("LEFT");
            commands.AppendLine("REPORT");
            
            commands.AppendLine("PLACE 1,2,EAST");
            commands.AppendLine("MOVE");
            commands.AppendLine("MOVE");
            commands.AppendLine("LEFT");
            commands.AppendLine("MOVE");
            commands.AppendLine("REPORT");

            return commands.ToString();
        }
    }
}
