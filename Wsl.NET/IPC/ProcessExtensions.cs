using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wsl.NET.IPC
{
    public static class ProcessExtensions
    {
        public static Task WaitForExitAsync(
            this Process process, 
            CancellationToken cancellationToken
        )
        {
            return Task
                .Factory
                .StartNew(
                    () => process.WaitForExit(),
                    cancellationToken
                );
        }
    }
}
