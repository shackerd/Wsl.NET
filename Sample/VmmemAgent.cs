using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WslGUI
{
    public class VmmemAgent
    {
        private const string VMMEM_PROC = "vmmem";        
        public VmmemAgent() { }

        public string GetMemoryUsage(out int percent)
        {
            using Process process =
                Process
                    .GetProcessesByName(VMMEM_PROC)
                    .SingleOrDefault();

            if (process == null)
            {
                percent = 0;
                return "Idle";
            }

            MemoryMetrics metrics = 
                GetWindowsMetrics();

            double totalSystemSize = 
                ByteSizeCalculator
                    .DetectScaleAndCompute(
                        (long)metrics.Total, 
                        out var scale
                    );

            double usedSystemSize =
                ByteSizeCalculator
                    .Compute(
                        (long)metrics.Used,
                        scale
                    );

            double procSize = 
                ByteSizeCalculator
                    .Compute(
                        process.PrivateMemorySize64, 
                        scale
                    );

            percent = (int)(procSize / totalSystemSize * 100);
            int percentProcOfUsed = (int)(procSize / usedSystemSize * 100);
            int percentOfUsed = (int)(usedSystemSize / totalSystemSize * 100);
            

            return $"{Math.Round(procSize, 3)}/{Math.Round(totalSystemSize, 3)} {scale} ({percent}% of total system memory {percentProcOfUsed}% of global usage) {Math.Round(usedSystemSize, 3)}/{Math.Round(totalSystemSize, 3)} {scale} ({percentOfUsed}%)";
        }

        private MemoryMetrics GetWindowsMetrics()
        {
            var output = "";

            var info = new ProcessStartInfo
            {
                FileName = "wmic",
                Arguments = "OS get FreePhysicalMemory,TotalVisibleMemorySize /Value",
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            using (var process = Process.Start(info))
            {
                output = process.StandardOutput.ReadToEnd();
            }

            var lines = output.Trim().Split("\n");

            var freeMemoryParts = 
                lines[0].Split("=", StringSplitOptions.RemoveEmptyEntries);

            var totalMemoryParts = 
                lines[1].Split("=", StringSplitOptions.RemoveEmptyEntries);

            var metrics = new MemoryMetrics
            {
                Total = double.Parse(totalMemoryParts[1]) * 1024,
                Free = double.Parse(freeMemoryParts[1]) * 1024
            };
            metrics.Used = (metrics.Total - metrics.Free);

            return metrics;
        }
    }

    public class MemoryMetrics
    {
        public double Total;
        public double Used;
        public double Free;
        public SizeScale Scale;
    }
}
