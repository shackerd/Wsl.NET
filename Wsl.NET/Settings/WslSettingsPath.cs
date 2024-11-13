namespace Wsl.NET.Settings
{
    public readonly struct WslSettingsPath
    {
        private readonly string _path;
        public WslSettingsPath(string path)
        {
            _path = path;
        }

        public static implicit operator string(WslSettingsPath path)
        {
            // <path> entries must be absolute Windows paths with escaped backslashes, for example C:\\Users\\Ben\\kernel
            return path._path.Replace(@"\", @"\\");
        }
    }
}
