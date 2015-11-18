using System;
using System.Diagnostics;

namespace SM.Core.Extensions
{
    public static class ProcessExtension
    {
        public static bool IsRunning(this Process process)
        {
            try
            {
                Process.GetProcessById(process.Id);
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            return true;
        }
    }
}