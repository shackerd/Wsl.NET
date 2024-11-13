using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wsl.NET.Drivers.Wrap;
using Wsl.NET.Extensions;
using Wsl.NET.IPC;

namespace Wsl.NET
{
    public partial class WslBinaryWrapDriver : IWslDriver
    {
        private static readonly WslDistroListStdReader _wslDistroListStdReader =
            new WslDistroListStdReader();

        /// <summary>
        ///
        /// </summary>
        /// <param name="args"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private static Task<ProcessCommandResult> RunCommand(
            string args,
            IStdResultReader reader,
            CancellationToken cancellationToken
        )
        {
            return
                ProcessFactory
                    .CreateManagedProcessAsync(
                        Paths.WslExe,
                        args,
                        reader,
                        cancellationToken: cancellationToken
                    );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="args"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private static Task<ProcessCommandResult<T>> RunCommand<T>(
            string args,
            IStdResultReader<T> reader,
            CancellationToken cancellationToken
        )
        {
            return
                ProcessFactory
                    .CreateManagedProcessAsync(
                        Paths.WslExe,
                        args,
                        reader,
                        cancellationToken: cancellationToken
                    );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandLine"></param>
        /// <param name="reader"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="distro"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private static async Task ExecAsyncInternal(
            string commandLine,
            IStdResultReader reader,
            CancellationToken cancellationToken,
            WslDistro distro,
            string user
        )
        {            
            StringBuilder builder =
                new StringBuilder($"-e \"{commandLine}\"");

            if (distro != null)
            {
                builder.Append($" -d {distro.Name}");
            }

            if (user.IsPresent())
            {
                builder.Append($" -u \"{user}\"");
            }

            ProcessCommandResult result =
                await RunCommand(
                    builder.ToString(), 
                    reader, 
                    cancellationToken
                );

            result.ThrowWslExceptionOnError();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandLine"></param>
        /// <param name="reader"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="distro"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private static async Task<T> ExecAsyncInternal<T>(
            string commandLine,
            IStdResultReader<T> reader,
            CancellationToken cancellationToken,
            WslDistro distro,
            string user
        )
        {
            StringBuilder builder =
                new StringBuilder($"-e \"{commandLine}\"");

            if (distro != null)
            {
                builder.Append($" -d {distro.Name}");
            }

            if (user.IsPresent())
            {
                builder.Append($" -u \"{user}\"");
            }

            ProcessCommandResult<T> result =
                await RunCommand(
                    builder.ToString(),
                    reader,
                    cancellationToken
                );

            result.ThrowWslExceptionOnError();

            return result.Value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="distro"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private static async Task TerminateAsyncInternal(
            WslDistro distro,
            CancellationToken cancellationToken
        )
        {
            ProcessCommandResult result =
                await RunCommand(
                    $"-t {distro.Name}",
                    WslStdReader.Instance,
                    cancellationToken
                );

            result.ThrowWslExceptionOnError();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private static async Task ShutdownAsyncInternal(
            CancellationToken cancellationToken
        )
        {
            ProcessCommandResult result =
                await RunCommand(
                    $"--shutdown",
                    WslStdReader.Instance,
                    cancellationToken
                );

            result.ThrowWslExceptionOnError();
        }  

        /// <summary>
        ///
        /// </summary>
        /// <param name="distro"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private static async Task<Process> StartAsyncInternal(
            WslDistro distro,
            CancellationToken cancellationToken
        )
        {
            return await ProcessFactory
                .CreateUnmanagedProcessAsync(
                    Paths.WslExe,
                    $"-d {distro.Name}",
                    createNoWindow: false,
                    cancellationToken: cancellationToken
                );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="distro"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private static async Task<Process> StartAsyncInternal(
            WslDistro distro,
            WslDistroCommand command,
            bool interactive,
            CancellationToken cancellationToken
        )
        {
            StringBuilder builder =
                new StringBuilder($"-d {distro.Name}");

            foreach (string segment in (string[])command)
            {
                builder.Append($" \"{segment}\"");
            }

            return await ProcessFactory
                .CreateUnmanagedProcessAsync(
                    Paths.WslExe,
                    builder.ToString(),
                    createNoWindow: !interactive,
                    cancellationToken: cancellationToken
                );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="distro"></param>
        /// <param name="path"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private static async Task<WslDistro> MoveAsyncInternal(
            WslDistro distro,
            string path,
            string vhdxPath,
            CancellationToken cancellationToken
        )
        {            
            await ExportAsyncInternal(distro, path, cancellationToken);
            // must control VHDX once export is done

            //ICSharpCode.SharpZipLib.Checksum.Crc32 checksum =
            //    new ICSharpCode.SharpZipLib.Checksum.Crc32();

            //using Stream stream = File.OpenRead(path);

            //byte[] buffer = new byte[0x1000];
            
            //await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
            
            //stream.Close();

            //checksum.Update(buffer);

            //string value = checksum.ToString();

            await UnRegisterAsyncInternal(distro, cancellationToken);

            return await ImportAsyncInternal(path, vhdxPath, distro.Name, distro.Version, cancellationToken);
            // https://github.com/pxlrbt/move-wsl/blob/master/move-wsl
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="distro"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private static async Task SetDefaultDistroAsyncInternal(
            WslDistro distro,
            CancellationToken cancellationToken
        )
        {

            ProcessCommandResult result =
                await RunCommand(
                    $"-s {distro.Name}",
                    WslStdReader.Instance,
                    cancellationToken
                );

            result.ThrowWslExceptionOnError();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="distro"></param>
        /// <param name="version"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async static Task SetVersionAsyncInternal(
            WslDistro distro,
            WslDistroVersion version,
            CancellationToken cancellationToken
        )
        {
            ProcessCommandResult result =
                await RunCommand(
                    $"--set-version {distro.Name} {(int)version}",
                    WslStdReader.Instance,
                    cancellationToken
                );

            result.ThrowWslExceptionOnError();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="version"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async static Task SetDefaultVersionAsyncInternal(
            WslDistroVersion version,
            CancellationToken cancellationToken
        )
        {
            ProcessCommandResult result =
                await RunCommand(
                    $"--set-default-version {(int)version}",
                    WslStdReader.Instance,
                    cancellationToken
                );

            result.ThrowWslExceptionOnError();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="distro"></param>
        /// <param name="path"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async static Task ExportAsyncInternal(
            WslDistro distro,
            string path,
            CancellationToken cancellationToken
        )
        {
            ProcessCommandResult result =
                await RunCommand(
                    $"--export {distro.Name} \"{ path }\"",
                    WslStdReader.Instance,
                    cancellationToken
                );

            result.ThrowWslExceptionOnError();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="version"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async static Task<WslDistro> ImportAsyncInternal(
            string path,
            string vhdxPath,
            string name,
            WslDistroVersion version,
            CancellationToken cancellationToken
        )
        {
            ProcessCommandResult result =
                await RunCommand(
                    $"--import {name} \"{vhdxPath}\" \"{path}\" --version {(int)version}",
                    WslStdReader.Instance,
                    cancellationToken
                );

            result.ThrowWslExceptionOnError();

            return new WslDistro(
                name, 
                WslDistroState.Stopped, 
                version, 
                false
            );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="distro"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async static Task UnRegisterAsyncInternal(
            WslDistro distro,
            CancellationToken cancellationToken
        )
        {
            ProcessCommandResult result =
                await RunCommand(
                    $"--unregister {distro.Name}",
                    WslStdReader.Instance,
                    cancellationToken
                );

            result.ThrowWslExceptionOnError();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private static async Task<IEnumerable<WslDistro>> FetchAsyncInternal(
            CancellationToken cancellationToken
        )
        {
            ProcessCommandResult<IEnumerable<WslDistro>> result =
                await ProcessFactory
                    .CreateManagedProcessAsync(
                        Paths.WslExe,
                        "-l -v --all",
                        _wslDistroListStdReader,
                        cancellationToken: cancellationToken
                    );

            result.ThrowWslExceptionOnError();

            return result.Value;
        }
    }
}