using System.Diagnostics;
using System.Security;

namespace Cloney.Core.Diagnostics.Abstractions
{
    public interface IProcess
    {
        void EnterDebugMode();
        Process GetCurrentProcess();
        Process GetProcessById(int processId);
        Process GetProcessById(int processId, string machineName);
        Process[] GetProcesses();
        Process[] GetProcesses(string machineName);
        Process[] GetProcessesByName(string processName);
        Process[] GetProcessesByName(string processName, string machineName);
        void LeaveDebugMode();
        void Start(ProcessStartInfo startInfo);
        void Start(string fileName);
        void Start(string fileName, string arguments);
        void Start(string fileName, string arguments, string userName, SecureString password, string domain);
        void Start(string fileName, string userName, SecureString password, string domain);
    }
}
