using System;
using System.ComponentModel;

namespace Andromeda.RoverRobot.Utils
{
    public static class EnumUtils
    {
        public static T GetEnumFromDescription<T>(string description)
        {
            var type = typeof(T);

            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)

                    if (description.Equals(attribute.Description, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return (T) field.GetValue(null);
                    }

                    else
                    {
                        if (description.Equals(field.Name, StringComparison.InvariantCultureIgnoreCase))
                        {
                            return (T) field.GetValue(null);
                        }
                    }
            }

            throw new ArgumentException("Not found.", nameof(description));
        }
    }
}