using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SM.Contracts.Models.Result;
using SM.Contracts.Models.Codes;
using SM.Core.Extensions;
using SM.Core.Services;
using Timer = System.Timers.Timer;

namespace SM.Monitoring.Service
{
    public class StarterServiceHost : SMServiceHost
    {
        private Process _hwInfoProcess;
        private Process _remoteSensorMonitorProcess;
        private const string HWInfoPath = "HWINFO";
        private const string RemoteSensorMonitorPath = "REMOTESENSORMONITOR";

        public StarterServiceHost()
        {
        }

        public async override void OnStartUp()
        {
            _hwInfoProcess = StartProcess(HWInfoPath);
            _remoteSensorMonitorProcess = await StartProcess(RemoteSensorMonitorPath, _hwInfoProcess, true);
        }

        private static Process StartProcess(string path, bool createNoWindow = false)
        {
            Process process = null;

            var processes = Process.GetProcesses();
            var isRunning = processes.Any(p => p.ProcessName.Trim().ToLower().Contains(path.ToLower()));
            if (isRunning)
            {
                return null;
            }

            var variable = Environment.GetEnvironmentVariable(path, EnvironmentVariableTarget.User);
            if (variable == null)
            {
                return null;
            }

            try
            {
                process = new Process {StartInfo = new ProcessStartInfo
                {
                    CreateNoWindow = createNoWindow,
                    WindowStyle = createNoWindow ? ProcessWindowStyle.Hidden : ProcessWindowStyle.Normal,
                    FileName = variable
                } };
                process.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return process;
        }

        private async static Task<Process> StartProcess(string path, Process proc, bool createNoWindow = false)
        {
            if (proc != null)
            {
                while (!proc.IsRunning())
                {
                    await Task.Delay(200);
                }
            }            

            await Task.Delay(1000);

            return StartProcess(path, createNoWindow);
        }

        public override void OnStop()
        {
            //_hwInfoProcess.Kill();
            //_remoteSensorMonitorProcess.Kill();
        }
    }
}