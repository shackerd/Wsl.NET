namespace Wsl.NET
{
    public class WslDistro
    {
        public string Name { get; }
        public WslDistroState State { get; }
        public WslDistroVersion Version { get; }
        public bool IsDefault { get; }

        public WslDistro(
            string name, 
            WslDistroState state,
            WslDistroVersion version, 
            bool isDefault
        )
        {
            Name = name;
            State = state;
            Version = version;
            IsDefault = isDefault;
        }
    }
}
