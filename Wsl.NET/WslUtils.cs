using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Wsl.NET
{
    public static class WslUtils
    {
        // TODO : Start in working directory 
        // TODO : RunAs
        public static bool MeetPrerequises()
        {
            return false;
        }

        public static Task InstallPrerequisesAsync()
        {
            return Task.CompletedTask;
        }
    }
}
