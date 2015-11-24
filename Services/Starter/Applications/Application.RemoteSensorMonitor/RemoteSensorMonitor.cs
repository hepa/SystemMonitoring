using System;
using System.Diagnostics;
using ApplicationSDK;
using SM.Core.Extensions;

namespace Application.RemoteSensorMonitor
{
    public class RemoteSensorMonitor : IApplication
    {
        public string Name => "Remote Sensor Monitor";

        public string EnvironmentVariable { get; set; } = "REMOTESENSORMONITOR";

        public bool OutputRedirectRequired => true;

        public bool IsRunning(Process process)
        {
            if (!process.ProcessName.Contains(EnvironmentVariable.ToLower(), StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            var webServerRunning = false;
            var isHwinfoEnabled = false;

            while (!process.StandardOutput.EndOfStream)
            {
                var line = process.StandardOutput.ReadLine();

                if (string.IsNullOrEmpty(line)) continue;

                if (line.ToLower().Contains("server running"))
                {
                    webServerRunning = true;
                }

                if (line.ToLower().Contains("enabling hwinfo"))
                {
                    isHwinfoEnabled = true;
                }

                if (line.ToLower().Contains("enter"))
                {
                    break;
                }
            }

            return webServerRunning && isHwinfoEnabled;            
        }
    }
}