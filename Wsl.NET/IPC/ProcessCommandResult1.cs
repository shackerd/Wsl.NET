namespace Wsl.NET.IPC
{
    public class ProcessCommandResult<T> : ProcessCommandResult
    {
        public T Value { get; }

        public ProcessCommandResult(
            T value, 
            string stdin,
            string stderr, 
            int exitCode
        ) : base(stdin, stderr, exitCode)
        {
            Value = value;
        }
    }
}
