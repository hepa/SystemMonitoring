using System.Diagnostics;

namespace ApplicationSDK
{
    public interface IApplication
    {
        string Name { get; }

        string EnvironmentVariable { get; set; }

        bool OutputRedirectRequired { get; }

        bool IsRunning(Process process);
    }
}