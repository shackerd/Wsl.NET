using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wsl.NET.IPC
{
    public class CommonStdReader : IStdResultReader
    {
        private static readonly Lazy<CommonStdReader> _instance =
            new Lazy<CommonStdReader>(
                () => new CommonStdReader(),
                LazyThreadSafetyMode.ExecutionAndPublication
            );

        public static CommonStdReader Instance => _instance.Value;

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
                stderr,
                exitCode                
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
            StreamReader stderr,
            int exitCode
        )
        {
            string @out =
                await stdin
                    .ReadToEndAsync();

            string err =
                await stderr
                    .ReadToEndAsync();

            return new ProcessCommandResult(
                @out,
                err,
                exitCode
            );
        }
    }
}
