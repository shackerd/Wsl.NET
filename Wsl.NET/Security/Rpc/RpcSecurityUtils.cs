using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Wsl.NET.Security.Rpc
{
    public static class RpcSecurityUtils
    {
        [DllImport("ole32.dll")]
        private static extern int CoInitializeSecurity(
            IntPtr pVoid,
            int cAuthSvc,
            IntPtr asAuthSvc,
            IntPtr pReserved1,
            RpcAuthnLevel level,
            RpcImpLevel impers,
            IntPtr pAuthList,
            EoAuthnCap dwCapabilities,
            IntPtr pReserved3
        );

        /// <summary>
        /// Registers security and sets the default security values for the process, must be called first once app starts, note: does not work in debug
        /// </summary>
        public static void CoInitializeSecurity()
        {
            // https://www.pinvoke.net/default.aspx/ole32.coinitializesecurity
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                CoInitializeSecurity(
                    IntPtr.Zero,
                    -1,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    RpcAuthnLevel.None,
                    RpcImpLevel.Impersonate,
                    IntPtr.Zero,
                    EoAuthnCap.None,
                    IntPtr.Zero
                );
            }
        }
    }
}
