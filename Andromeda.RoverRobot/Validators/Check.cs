using System;
using System.Collections.Generic;
using System.Linq;

namespace Andromeda.RoverRobot.Validators
{
    public static class Check
    {
        public static void NotNull(object input, string name)
        {
            if (input == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void NotNullOrEmpty(string input, string name)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void IsGreaterThan(int input, int minValue, string name)
        {
            if (input < minValue)
            {
                throw new ArgumentException(name);
            }
        }
    }
}