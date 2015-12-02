using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.RemoteSensorMonitor;
using ApplicationSDK;
using SM.Configuration.Configuration;
using SM.Contracts.Models.Result;
using SM.Contracts.Models.Codes;
using SM.Core.Extensions;
using SM.Core.Services;
using SM.Logs;
using Timer = System.Timers.Timer;

namespace SM.Monitoring.Service
{
    public class StarterServiceHost : SMServiceHost
    {
        private Process _remoteSensorMonitorProcess;

        public StarterServiceHost()
        {
        }

        public async override void OnStartUp()
        {
            var remoteSensorMonitorApp = new RemoteSensorMonitor();

            Logger.Info("Trying to start RemoteSensor.");
            var remoteSensorMonitorProcessResult = await StartApplication(remoteSensorMonitorApp, true);

            if (remoteSensorMonitorProcessResult != null)
            {
                while (remoteSensorMonitorProcessResult.ResultCode.ErrorCode != ErrorCodes.NoError)
                {
                    Logger.Info("Trying to start RemoteSensor.");
                    remoteSensorMonitorProcessResult = await StartApplication(remoteSensorMonitorApp, true);
                    await Task.Delay(SMConfigurations.Current.GlobalConfigurations.ProcessStartRetryRateInMilliSeconds);
                }

                _remoteSensorMonitorProcess = remoteSensorMonitorProcessResult.Data;
            }
        }

        private static async Task<Result<Process>> StartApplication(IApplication app, bool createNoWindow = false)
        {
            Process process = null;

            var processes = Process.GetProcesses();
            process = processes.FirstOrDefault(p => p.ProcessName.Trim().ToLower().Contains(app.EnvironmentVariable.ToLower()));
            if (process != null)
            {
                Logger.Warn(string.Format("The process is already running: {0}", app.EnvironmentVariable));

                return new Result<Process>
                {
                    Data = process,
                    ResultCode = new ResultCode
                    {
                        ErrorCode = ErrorCodes.NoError
                    }
                };
            }

            var variable = Environment.GetEnvironmentVariable(app.EnvironmentVariable, EnvironmentVariableTarget.Machine);
            if (variable == null)
            {
                Logger.Error(string.Format("The system environment variable has not been found for: {0}", app));
                return null;
            }

            try
            {
                process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        CreateNoWindow = createNoWindow,
                        WindowStyle = createNoWindow ? ProcessWindowStyle.Hidden : ProcessWindowStyle.Normal,
                        FileName = variable,
                        RedirectStandardOutput = app.OutputRedirectRequired,
                        UseShellExecute = !app.OutputRedirectRequired
                    }
                };
                var isStarted = process.Start();

                if (!isStarted)
                {
                    Logger.Error(string.Format("The process could not be started: {0}", process.ProcessName));
                    return null;
                }

                if (!app.IsRunning(process))
                {
                    Logger.Error("The web server has not been started.");

                    try
                    {
                        process.Kill();
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex.ToString());
                        return new Result<Process>
                        {
                            Data = null,
                            ResultCode = new ResultCode
                            {
                                ErrorCode = ErrorCodes.GenericError
                            }
                        };
                    }  

                    return new Result<Process>
                    {                        
                        Data = null,
                        ResultCode = new ResultCode
                        {
                            ErrorCode = ErrorCodes.WebServerStartFailure
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }

            return new Result<Process>
            {
                Data = process,
                ResultCode = new ResultCode
                {
                    ErrorCode = ErrorCodes.NoError
                }
            };
        }

        private async static Task<Result<Process>> StartApplication(IApplication app, Process proc, bool createNoWindow = false)
        {
            if (proc != null)
            {
                while (!proc.IsRunning())
                {
                    await Task.Delay(200);
                }
            }

            return await StartApplication(app, createNoWindow);
        }

        public override void OnStop()
        {
            if (_remoteSensorMonitorProcess != null)
            {
                try
                {
                    _remoteSensorMonitorProcess.Kill();
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.ToString());
                }
            }            
        }
    }
}