namespace Wsl.NET.Settings
{
    public readonly struct WslSettingsSize
    {
        private readonly int _size;
        private readonly WslSettingsSizeUnit _unit;
        public WslSettingsSize(int size, WslSettingsSizeUnit unit)
        {
            _size = size;
            _unit = unit;
        }

        public static implicit operator string(WslSettingsSize memory)
        {
            return $"{memory._size}{memory._unit}";
        }
    }
}
