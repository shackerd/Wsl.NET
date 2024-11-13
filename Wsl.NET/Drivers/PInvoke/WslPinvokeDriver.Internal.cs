using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Wsl.NET.IPC;

namespace Wsl.NET
{
    public partial class WslPinvokeDriver : IWslDriver
    {
        // https://docs.microsoft.com/en-us/windows/wsl/compare-versions#whats-new-in-wsl-2
        // https://docs.microsoft.com/en-us/windows/win32/api/wslapi/
        // https://github.com/Microsoft/WSL-DistroLauncher/blob/master/DistroLauncher/WslApiLoader.cpp
        // https://github.com/microsoft/WSL/blob/master/diagnostics/networking.bat

        private const string _wslAPIDLL = "wslapi.dll";

        [DllImport(_wslAPIDLL, CharSet = CharSet.Ansi)]
        private static extern bool WslIsDistributionRegistered(
            [MarshalAs(UnmanagedType.LPWStr)] string distributionName
        );

        /// <summary>
        ///
        /// </summary>
        /// <param name="distributionName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<bool> WslIsDistributionRegisteredAsync(
            string distributionName,
            CancellationToken cancellationToken
        )
        {
            return Task
                .Factory
                .StartNew(
                    () => WslIsDistributionRegistered(distributionName),
                    cancellationToken
                );
        }

        [DllImport(_wslAPIDLL, CharSet = CharSet.Ansi)]
        private static extern uint WslUnregisterDistribution(
            [MarshalAs(UnmanagedType.LPWStr)] string distributionName
        );

        [DllImport(_wslAPIDLL, CharSet = CharSet.Ansi)]
        private static extern uint WslRegisterDistribution(
            [MarshalAs(UnmanagedType.LPWStr)] string distributionName,
            [MarshalAs(UnmanagedType.LPWStr)] string tarGzFilename
        );


        [DllImport(_wslAPIDLL, CharSet = CharSet.Ansi)]
        private static extern uint WslConfigureDistribution(
            [MarshalAs(UnmanagedType.LPWStr)] string distributionName,
            ulong defaultUID,
            int wslDistributionFlags
        );

        [DllImport(_wslAPIDLL, CharSet = CharSet.Ansi)]
        private static extern uint WslGetDistributionConfiguration(
            [MarshalAs(UnmanagedType.LPWStr)] string distributionName,
            out ulong distributionVersion,
            out ulong defaultUID,
            out uint wslDistributionFlags,
            out string defaultEnvironmentVariables,
            out uint defaultEnvironmentVariableCount
        );

        [DllImport(_wslAPIDLL, CharSet = CharSet.Ansi)]
        private static extern uint WslLaunchInteractive(
            [MarshalAs(UnmanagedType.LPWStr)] string distributionName,
            [MarshalAs(UnmanagedType.LPWStr)] string command,
            [MarshalAs(UnmanagedType.U1)] bool useCurrentWorkingDirectory,
            out uint exitCode
        );         
    }
}