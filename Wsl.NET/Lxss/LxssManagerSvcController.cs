using System.Threading;
using System.Threading.Tasks;
using Wsl.NET.IPC;

namespace Wsl.NET
{
    public static class LxssManagerSvcController
    {
        private static readonly string _serviceName = "LxssManager";

        private static async Task StartAsyncInternal(CancellationToken cancellationToken)
        {
            ProcessCommandResult result =
                await ProcessFactory
                    .CreateManagedProcessAsync(
                        "cmd",
                        $"/k net start {_serviceName}",
                        CommonStdReader.Instance,
                        true,
                        cancellationToken
                    );

            if (result.ExitCode != 0)
            {
                throw new LxssServiceException(
                    $"Cannot start {_serviceName} service, reason: {System.Environment.NewLine}{result.Stderr}"
                );
            }
        }

        private static async Task StopAsyncInternal(CancellationToken cancellationToken)
        {
            ProcessCommandResult result =
                await ProcessFactory
                    .CreateManagedProcessAsync(
                        "cmd",
                        $"/k net stop {_serviceName}",
                        CommonStdReader.Instance,
                        true,
                        cancellationToken
                    );

            if (result.ExitCode != 0)
            {
                throw new LxssServiceException(
                    $"Cannot stop {_serviceName} service, reason: {System.Environment.NewLine}{result.Stderr}"
                );
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="LxssServiceException"></exception>
        /// <returns></returns>
        public static Task StartAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return StartAsyncInternal(
                cancellationToken
            );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="LxssServiceException"></exception>
        /// <returns></returns>
        public static Task StopAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return StopAsyncInternal(
                cancellationToken
            );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="LxssServiceException"></exception>
        /// <returns></returns>
        public static Task RestartAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return StopAsyncInternal(cancellationToken)
                .ContinueWith(
                    (_, c) => StartAsyncInternal((CancellationToken)c),
                    cancellationToken
                );
        }
    }
}
