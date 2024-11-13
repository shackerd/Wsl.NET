using System;

namespace Wsl.NET
{   
    public class WslDistroCommand
    {
        private readonly string[] _segments;

        private WslDistroCommand(string[] segments)
        {
            _segments = segments;
        }

        public static implicit operator WslDistroCommand(string commandLine)
        {
            if (commandLine.IsMissing())
            {
                throw new ArgumentNullException(
                    nameof(commandLine)
                );
            }

            return new WslDistroCommand(
                commandLine.Split(
                    (char)0x20, 
                    StringSplitOptions.RemoveEmptyEntries
                )
            );
        }

        public static implicit operator string[](WslDistroCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(
                    nameof(command)
                );
            }

            return command._segments;
        }
    }
}