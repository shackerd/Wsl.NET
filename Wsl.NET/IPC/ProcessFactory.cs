using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wsl.NET.IPC
{
    // TODO : Args assertions
    public static class ProcessFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="args"></param>
        /// <param name="processor"></param>
        /// <param name="createNoWindow"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="TaskCanceledException"></exception>
        /// <returns></returns>
        public static Task<ProcessCommandResult> CreateManagedProcessAsync(
            string path,
            string args,
            IStdResultReader processor,
            bool createNoWindow = true,
            CancellationToken cancellationToken = default
        )
        {
            if (path.IsMissing())
            {
                throw new ArgumentNullException(
                    nameof(path)
                );
            }

            if (processor == null)
            {
                throw new ArgumentNullException(
                    nameof(processor)
                );
            }

            cancellationToken.ThrowIfCancellationRequested();

            return CreateManagedProcessAsyncInternal(
                path, 
                args, 
                processor, 
                createNoWindow, 
                cancellationToken
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="args"></param>
        /// <param name="processor"></param>
        /// <param name="createNoWindow"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async static Task<ProcessCommandResult> CreateManagedProcessAsyncInternal(
            string path,
            string args,
            IStdResultReader processor,
            bool createNoWindow,
            CancellationToken cancellationToken
        )
        {
            ProcessStartInfo startInfo =
                new ProcessStartInfo(path, args)
                {
                    CreateNoWindow = createNoWindow,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };
            
            Process process = 
                Process.Start(startInfo);

            await process.WaitForExitAsync(
                cancellationToken
            );

            ProcessCommandResult result = 
                await processor.ReadAsync(
                    process.StandardOutput, 
                    process.StandardError, 
                    process.ExitCode,
                    cancellationToken
                );

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="args"></param>
        /// <param name="processor"></param>
        /// <param name="createNoWindow"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<ProcessCommandResult<T>> CreateManagedProcessAsync<T>(
            string path,
            string args,
            IStdResultReader<T> processor,
            bool createNoWindow = true,
            CancellationToken cancellationToken = default
        )
        {
            if (path.IsMissing())
            {
                throw new ArgumentNullException(
                    nameof(path)
                );
            }

            if (processor == null)
            {
                throw new ArgumentNullException(
                    nameof(processor)
                );
            }

            cancellationToken.ThrowIfCancellationRequested();

            return CreateManagedProcessAsyncInternal(
                path,
                args,
                processor,
                createNoWindow,
                cancellationToken
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="args"></param>
        /// <param name="processor"></param>
        /// <param name="createNoWindow"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async static Task<ProcessCommandResult<T>> CreateManagedProcessAsyncInternal<T>(
            string path,
            string args,
            IStdResultReader<T> processor,
            bool createNoWindow,
            CancellationToken cancellationToken
        )
        {
            ProcessStartInfo startInfo =
                new ProcessStartInfo(path, args)
                {
                    CreateNoWindow = createNoWindow,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

            Process process =
                Process.Start(startInfo);

            await process.WaitForExitAsync(
                cancellationToken
            );

            ProcessCommandResult<T> result =
                await processor.ReadAsync(
                    process.StandardOutput,
                    process.StandardError,
                    process.ExitCode,
                    cancellationToken
                );

            return result;
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        /// <param name="args"></param>
        /// <param name="createNoWindow"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="TaskCanceledException"></exception>
        /// <returns></returns>
        public static Task<Process> CreateUnmanagedProcessAsync(
            string path,
            string args,
            bool createNoWindow = false,
            CancellationToken cancellationToken = default
        )
        {
            if (path.IsMissing())
            {
                throw new ArgumentNullException(
                    nameof(path)
                );
            }

            cancellationToken.ThrowIfCancellationRequested();

            return CreateUnmanagedProcessAsyncInternal(
                path,
                args,
                createNoWindow,
                cancellationToken
            );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        /// <param name="args"></param>
        /// <param name="createNoWindow"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private static async Task<Process> CreateUnmanagedProcessAsyncInternal(
            string path,
            string args,
            bool createNoWindow,
            CancellationToken cancellationToken
        )
        {
            ProcessStartInfo startInfo =
                new ProcessStartInfo(path, args)
                {
                    CreateNoWindow = createNoWindow
                };

            return
                await Task
                    .Factory
                    .StartNew(
                        () => Process.Start(startInfo),
                        cancellationToken
                    );
        }        
    }
}
