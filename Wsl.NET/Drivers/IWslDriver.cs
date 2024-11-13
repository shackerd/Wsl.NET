using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Wsl.NET.IPC;

namespace Wsl.NET
{
    public interface IWslDriver
    {
        Task ExecAsync(string commandLine, IStdResultReader reader,  CancellationToken cancellationToken, WslDistro distro = null, string user = null);
        Task<T> ExecAsync<T>(string commandLine, IStdResultReader<T> reader,  CancellationToken cancellationToken, WslDistro distro = null, string user = null);
        Task StartAsync(WslDistro distro, CancellationToken cancellationToken);
        Task StartAsync(WslDistro distro, WslDistroCommand command, bool interactive, CancellationToken cancellationToken);
        Task TerminateAsync(WslDistro distro, CancellationToken cancellationToken);
        Task ShutdownAsync(CancellationToken cancellationToken);
        Task<WslDistro> MoveAsync(WslDistro distro, string path, string vhdxPath, CancellationToken cancellationToken);
        Task SetDefaultDistroAsync(WslDistro distro, CancellationToken cancellationToken);
        Task SetVersionAsync(WslDistro distro, WslDistroVersion version, CancellationToken cancellationToken);
        Task SetDefaultVersionAsync(WslDistroVersion version, CancellationToken cancellationToken);
        Task ExportAsync(WslDistro distro, string path, CancellationToken cancellationToken);
        Task ImportAsync(string path, string vhdxPath, string name, WslDistroVersion version, CancellationToken cancellationToken);
        Task UnRegisterAsync(WslDistro distro, CancellationToken cancellationToken);
        Task<IEnumerable<WslDistro>> FetchDistroAsync(CancellationToken cancellationToken);
    }    
}