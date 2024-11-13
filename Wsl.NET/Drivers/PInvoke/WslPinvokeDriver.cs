using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Wsl.NET.IPC;

namespace Wsl.NET
{
    // TODO : Remove, pinvoke api wslapi.h cannot provide enought feature, this set of method stubs are used for launchers such as wsldl (https://github.com/yuk7/wsldl)
    public partial class WslPinvokeDriver : IWslDriver
    {
        public Task ExecAsync(string commandLine, IStdResultReader reader, CancellationToken cancellationToken, WslDistro distro = null, string user = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> ExecAsync<T>(string commandLine, IStdResultReader<T> reader, CancellationToken cancellationToken, WslDistro distro = null, string user = null)
        {
            throw new NotImplementedException();
        }

        public Task ExportAsync(WslDistro distro, string path, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<WslDistro>> FetchDistroAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ImportAsync(string path, string vhdxPath, string name, WslDistroVersion version, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<WslDistro> MoveAsync(WslDistro distro, string path, string vhdxPath, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetDefaultDistroAsync(WslDistro distro, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetDefaultVersionAsync(WslDistroVersion version, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetVersionAsync(WslDistro distro, WslDistroVersion version, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ShutdownAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StartAsync(WslDistro distro, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StartAsync(WslDistro distro, WslDistroCommand command, bool interactive, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task TerminateAsync(WslDistro distro, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UnRegisterAsync(WslDistro distro, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}