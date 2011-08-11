using System.Diagnostics;
using System.Security;
using Cloney.Core.Diagnostics.Abstractions;

namespace Cloney.Core.Diagnostics
{
    public class ProcessFacade : IProcess
    {
        public void EnterDebugMode()
        {
            Process.EnterDebugMode();
        }

        public Process GetCurrentProcess()
        {
            return Process.GetCurrentProcess();
        }

        public Process GetProcessById(int processId)
        {
            return Process.GetProcessById(processId);
        }

        public Process GetProcessById(int processId, string machineName)
        {
            return Process.GetProcessById(processId, machineName);
        }

        public Process[] GetProcesses()
        {
            return Process.GetProcesses();
        }

        public Process[] GetProcesses(string machineName)
        {
            return Process.GetProcesses(machineName);
        }

        public Process[] GetProcessesByName(string processName)
        {
            return GetProcessesByName(processName);
        }

        public Process[] GetProcessesByName(string processName, string machineName)
        {
            return GetProcessesByName(processName, machineName);
        }

        public void LeaveDebugMode()
        {
            Process.LeaveDebugMode();
        }

        public void Start(ProcessStartInfo startInfo)
        {
            Process.Start(startInfo);
        }

        public void Start(string fileName)
        {
            Process.Start(fileName);
        }

        public void Start(string fileName, string arguments)
        {
            Process.Start(fileName, arguments);
        }

        public void Start(string fileName, string arguments, string userName, SecureString password, string domain)
        {
            Process.Start(fileName, arguments, userName, password, domain);
        }

        public void Start(string fileName, string userName, SecureString password, string domain)
        {
            Process.Start(fileName, userName, password, domain);
        }
    }
}
