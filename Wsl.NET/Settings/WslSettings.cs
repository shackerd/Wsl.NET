using System;
using System.Collections.Generic;
using System.Text;

namespace Wsl.NET.Settings
{
    public class WslSettings
    {
        /// <summary>
        /// Absolute Windows path to a custom Linux kernel.
        /// </summary>
        public WslSettingsPath KernelPath { get; set; }

        /// <summary>
        /// Defines how much memory to assign to the WSL2 VM.
        /// </summary>
        public WslSettingsSize Memory { get; set; } =
            new WslSettingsSize(2, WslSettingsSizeUnit.GB);

        /// <summary>
        /// Defines how many processors to assign to the WSL2 VM.
        /// </summary>
        public int Processors { get; set; } = 1;

        /// <summary>
        /// Define how much swap space to add to the WSL2 VM. 0 for no swap file.
        /// </summary>
        public WslSettingsSize Swap { get; set; } = 
            new WslSettingsSize(0, WslSettingsSizeUnit.GB);

        /// <summary>
        /// Absolute Windows path to the swap vhd.
        /// </summary>
        public WslSettingsPath SwapFile { get; set; }

        /// <summary>
        /// Specifies if ports bound to wildcard or localhost in the WSL2 VM should be connectable from the host via localhost:port(default true).
        /// </summary>
        public bool LocalhostForwarding { get; set; } = true;
    }
}
