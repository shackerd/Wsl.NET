namespace Wsl.NET.IPC
{
    public class ProcessCommandResult
    {
        public string Stdout { get; }
        public string Stderr { get; }

        public int ExitCode { get; }

        public ProcessCommandResult(
            string stdout,
            string stderr, 
            int exitCode
        )
        {
            Stdout = stdout;
            Stderr = stderr;
            ExitCode = exitCode;
        }
    }
}
