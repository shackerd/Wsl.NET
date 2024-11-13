using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Wsl.NET.IPC
{
    public interface IStdResultReader
    {
        Task<ProcessCommandResult> ReadAsync(
            StreamReader stdin, 
            StreamReader stderr, 
            int exitCode, 
            CancellationToken cancellationToken
        );
    }
}
