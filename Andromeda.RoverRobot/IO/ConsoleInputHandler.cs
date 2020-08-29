using System;
using System.Text;

namespace Andromeda.RoverRobot.IO
{
    public class ConsoleInputHandler : IInputHandler
    {
        private readonly IControlCenter _controlCenter;

        public ConsoleInputHandler(IControlCenter controlCenter)
        {
            _controlCenter = controlCenter;
        }

        public void Handle()
        {
            Console.WriteLine();
            Console.WriteLine("Please provide the commands, one command per line and when you are done enter #");
                    
            var commands = new StringBuilder();
            string line;
            do
            {
                line = Console.ReadLine();
                        
                if (line != "#")
                {
                    commands.AppendLine(line);
                }
            } while(!string.IsNullOrWhiteSpace(line) && line.ToLower() != "#");
                    
            _controlCenter.ExecuteCommands(commands.ToString());
        }
    }
}