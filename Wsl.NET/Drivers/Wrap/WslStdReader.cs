using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Wsl.NET.IPC;

namespace Wsl.NET.Drivers.Wrap
{
    /// <summary>
    /// 
    /// </summary>
    public class WslStdReader : IStdResultReader
    {
        private static readonly Lazy<WslStdReader> _instance =
            new Lazy<WslStdReader>(
                () => new WslStdReader(), 
                LazyThreadSafetyMode.ExecutionAndPublication
            );

        public static WslStdReader Instance => _instance.Value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stdin"></param>
        /// <param name="stderr"></param>
        /// <param name="exitCode"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<ProcessCommandResult> ReadAsync(
            StreamReader stdin, 
            StreamReader stderr, 
            int exitCode, 
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return ReadAsyncInternal(
                stdin,
                exitCode,
                cancellationToken
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stdin"></param>
        /// <param name="exitCode"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<ProcessCommandResult> ReadAsyncInternal(
            StreamReader stdin, 
            int exitCode, 
            CancellationToken cancellationToken
        )
        {
            string std = 
                await stdin
                    .ReadToEndAsUTF8Async(
                        cancellationToken
                    );            

            return new ProcessCommandResult(
                std,
                std, 
                exitCode
            );
        }
    }
}
