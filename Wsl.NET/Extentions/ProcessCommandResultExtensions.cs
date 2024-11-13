using System;
using Wsl.NET.IPC;

namespace Wsl.NET.Extensions
{
    public static class ProcessCommandResultExtensions
    {
        public static void ThrowWslExceptionOnError(this ProcessCommandResult result)
        {
            if (result.ExitCode != 0)
            {
                throw new WslException(
                    result.Stderr
                );
            }
        }
    }
}
