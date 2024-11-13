using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wsl.NET.IPC;

namespace Wsl.NET
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/wsl/release-notes
    /// TODO : \\wsl$\<distro_name>
    /// wslconfig.exe
    /// https://devblogs.microsoft.com/commandline/automatically-configuring-wsl/
    /// </summary>
    public partial class WslBinaryWrapDriver : IWslDriver
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="distro"></param>
        /// <param name="commandLine"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="TaskCanceledException"></exception>
        /// <exception cref="WslException"></exception>
        /// <returns></returns>
        public Task ExecAsync(
            string commandLine,
            IStdResultReader reader,
            CancellationToken cancellationToken,
            WslDistro distro = null,
            string user = null
        )
        {
            if (commandLine.IsMissing())
            {
                throw new ArgumentNullException(
                    nameof(commandLine)
                );
            }

            cancellationToken.ThrowIfCancellationRequested();

            return ExecAsyncInternal(
                commandLine,
                reader,
                cancellationToken,
                distro,
                user
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="distro"></param>
        /// <param name="commandLine"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="TaskCanceledException"></exception>
        /// <exception cref="WslException"></exception>
        /// <returns></returns>
        public Task<T> ExecAsync<T>(
            string commandLine,
            IStdResultReader<T> reader,
            CancellationToken cancellationToken,
            WslDistro distro = null,
            string user = null
        )
        {
            if (commandLine.IsMissing())
            {
                throw new ArgumentNullException(
                    nameof(commandLine)
                );
            }

            cancellationToken.ThrowIfCancellationRequested();

            return ExecAsyncInternal(
                commandLine,
                reader,
                cancellationToken,
                distro,
                user
            );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="distro"></param>
        /// <param name="path"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="TaskCanceledException"></exception>
        /// <exception cref="WslException"></exception>
        /// <returns></returns>
        public Task ExportAsync(
            WslDistro distro,
            string path,
            CancellationToken cancellationToken
        )
        {
            if (distro == null)
            {
                throw new ArgumentNullException(
                    nameof(distro)
                );
            }

            if (path.IsMissing())
            {
                throw new ArgumentNullException(
                    nameof(path)
                );
            }

            cancellationToken.ThrowIfCancellationRequested();

            return ExportAsyncInternal(
                distro,
                path,
                cancellationToken
            );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="TaskCanceledException"></exception>
        /// <exception cref="WslException"></exception>
        /// <returns></returns>
        public Task<IEnumerable<WslDistro>> FetchDistroAsync(
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return FetchAsyncInternal(cancellationToken);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        /// <param name="version"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="TaskCanceledException"></exception>
        /// <exception cref="WslException"></exception>
        /// <returns></returns>
        public Task ImportAsync(
            string path,
            string vhdxPath,
            string name,
            WslDistroVersion version,
            CancellationToken cancellationToken
        )
        {
            if (path.IsMissing())
            {
                throw new ArgumentNullException(
                    nameof(path)
                );
            }

            if (vhdxPath.IsMissing())
            {
                throw new ArgumentNullException(
                    nameof(vhdxPath)
                );
            }

            if (name.IsMissing())
            {
                throw new ArgumentNullException(
                    nameof(name)
                );
            }

            if (!File.Exists(path))
            {
                throw new FileNotFoundException(
                    "Cannot find file", 
                    path
                );
            }

            cancellationToken.ThrowIfCancellationRequested();


            return ImportAsyncInternal(
                path,
                vhdxPath,
                name,   
                version,
                cancellationToken
            );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="distro"></param>
        /// <param name="path"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="TaskCanceledException"></exception>
        /// <returns></returns>
        public Task<WslDistro> MoveAsync(
            WslDistro distro,
            string path,
            string vhdxPath,
            CancellationToken cancellationToken
        )
        {
            if (distro == null)
            {
                throw new ArgumentNullException(
                    nameof(distro)
                );
            }

            if (vhdxPath.IsMissing())
            {
                throw new ArgumentNullException(
                    nameof(vhdxPath)
                );
            }

            cancellationToken.ThrowIfCancellationRequested();

            return
                MoveAsyncInternal(
                    distro,
                    path,
                    vhdxPath,
                    cancellationToken
                );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="distro"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="TaskCanceledException"></exception>
        /// <exception cref="WslException"></exception>
        /// <returns></returns>
        public Task SetDefaultDistroAsync(
            WslDistro distro,
            CancellationToken cancellationToken
        )
        {
            if (distro == null)
            {
                throw new ArgumentNullException(
                    nameof(distro)
                );
            }

            cancellationToken.ThrowIfCancellationRequested();

            return
                SetDefaultDistroAsyncInternal(
                    distro,
                    cancellationToken
                );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="version"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="TaskCanceledException"></exception>
        /// <exception cref="WslException"></exception>
        /// <returns></returns>
        public Task SetDefaultVersionAsync(
            WslDistroVersion version,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return SetDefaultVersionAsyncInternal(
                version,
                cancellationToken
            );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="distro"></param>
        /// <param name="version"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="TaskCanceledException"></exception>
        /// <exception cref="WslException"></exception>
        /// <returns></returns>
        public Task SetVersionAsync(
            WslDistro distro,
            WslDistroVersion version,
            CancellationToken cancellationToken
        )
        {
            if (distro == null)
            {
                throw new ArgumentNullException(
                    nameof(distro)
                );
            }

            cancellationToken.ThrowIfCancellationRequested();

            return SetVersionAsyncInternal(
                distro,
                version,
                cancellationToken
            );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="TaskCanceledException"></exception>
        /// <exception cref="WslException"></exception>
        /// <returns></returns>
        public Task ShutdownAsync(
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return ShutdownAsyncInternal(
                cancellationToken
            );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="distro"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="TaskCanceledException"></exception>
        /// <returns></returns>
        public Task StartAsync(
            WslDistro distro,
            CancellationToken cancellationToken
        )
        {
            if (distro == null)
            {
                throw new ArgumentNullException(
                    nameof(distro)
                );
            }

            cancellationToken.ThrowIfCancellationRequested();

            return StartAsyncInternal(
                distro,
                cancellationToken
            );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="distro"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="TaskCanceledException"></exception>
        /// <returns></returns>
        public Task StartAsync(
            WslDistro distro,
            WslDistroCommand command,
            bool interactive,
            CancellationToken cancellationToken
        )
        {
            if (distro == null)
            {
                throw new ArgumentNullException(
                    nameof(distro)
                );
            }

            if (command == null)
            {
                throw new ArgumentNullException(
                    nameof(command)
                );
            }

            cancellationToken.ThrowIfCancellationRequested();

            return StartAsyncInternal(
                distro,
                command,
                interactive,
                cancellationToken
            );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="distro"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="TaskCanceledException"></exception>
        /// <exception cref="WslException"></exception>
        /// <returns></returns>
        public Task TerminateAsync(
            WslDistro distro,
            CancellationToken cancellationToken
        )
        {
            if (distro == null)
            {
                throw new ArgumentNullException(
                    nameof(distro)
                );
            }

            cancellationToken.ThrowIfCancellationRequested();

            return TerminateAsyncInternal(
                distro,
                cancellationToken
            );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="distro"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="TaskCanceledException"></exception>
        /// <exception cref="WslException"></exception>
        /// <returns></returns>
        public Task UnRegisterAsync(
            WslDistro distro,
            CancellationToken cancellationToken
        )
        {
            if (distro == null)
            {
                throw new ArgumentNullException(
                    nameof(distro)
                );
            }

            cancellationToken.ThrowIfCancellationRequested();

            return UnRegisterAsyncInternal(
                distro,
                cancellationToken
            );
        }
    }
}