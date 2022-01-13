using System;

namespace BotNet.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CommandAttribute : Attribute
    {
        public string Name { get; }

        public CommandAttribute()
        {
            Name = null;
        }

        public CommandAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "Command names cannot be null, empty, or all-whitespace.");

            Name = name;
        }
    }
}
