using System;
using System.IO;

namespace Andromeda.RoverRobot.IO
{
    public class FileInputHandler : IInputHandler
    {
        private readonly IControlCenter _controlCenter;

        public FileInputHandler(IControlCenter controlCenter)
        {
            _controlCenter = controlCenter;
        }

        public void Handle()
        {
            Console.WriteLine();
            Console.WriteLine(
                "Please provide path to the input file(.txt) formats. All other formats will be rejected." +
                "Input commands.txt for default file");
            var filePath = Console.ReadLine();

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Sorry system couldn't find the file.");
            }
            
            if (Path.GetExtension(filePath) != ".txt")
            {
                throw new FileLoadException("Incorrect file extension");
            }
            
            ProcessFile(filePath);
        }

        private void ProcessFile(string filePath)
        {
            var commands = File.ReadAllText(filePath);

            _controlCenter.ExecuteCommands(commands);
        }
    }
}