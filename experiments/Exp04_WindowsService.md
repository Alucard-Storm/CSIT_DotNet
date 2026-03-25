# Experiment 04 — Background Windows Service

**Subject:** CSIT-406 .NET Framework Lab
**RGPV, Bhopal**

---

## Aim

Create a background Windows Service and install it using Service Installer.

---

## Theory

A **Windows Service** is a long-running background process that starts with Windows, runs without user interaction, and can be controlled via the Services control panel.

Key classes in `System.ServiceProcess`:

| Class | Role |
|---|---|
| `ServiceBase` | Base class for all Windows Services |
| `ServiceInstaller` | Configures service name, display name, start type |
| `ServiceProcessInstaller` | Sets the account the service runs under |
| `OnStart()` | Called when the service starts |
| `OnStop()` | Called when the service stops |

> Real-world analogy: Windows Defender or a scheduled backup tool — they run silently in the background without you opening any window.

---

## Code

### Step 1 — Create a Windows Service Project

> In Visual Studio: **New Project → Windows Service (.NET Framework)**

```csharp
// LoggerService.cs
using System;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace LoggerService
{
    public partial class LoggerService : ServiceBase
    {
        private Timer _timer;
        private string _logPath = @"C:\ServiceLogs\log.txt";

        public LoggerService()
        {
            ServiceName = "FileLoggerService";
        }

        protected override void OnStart(string[] args)
        {
            Directory.CreateDirectory(@"C:\ServiceLogs");
            WriteLog("Service Started at " + DateTime.Now);

            _timer = new Timer(5000); // Every 5 seconds
            _timer.Elapsed += (s, e) => WriteLog("Heartbeat: " + DateTime.Now);
            _timer.Start();
        }

        protected override void OnStop()
        {
            _timer?.Stop();
            WriteLog("Service Stopped at " + DateTime.Now);
        }

        private void WriteLog(string message)
        {
            File.AppendAllText(_logPath, message + Environment.NewLine);
        }
    }
}
```

### Step 2 — Add Installer

```csharp
// LoggerServiceInstaller.cs
using System.ComponentModel;
using System.ServiceProcess;
using System.Configuration.Install;

namespace LoggerService
{
    [RunInstaller(true)]
    public class LoggerServiceInstaller : Installer
    {
        public LoggerServiceInstaller()
        {
            ServiceProcessInstaller processInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem
            };

            ServiceInstaller serviceInstaller = new ServiceInstaller
            {
                ServiceName = "FileLoggerService",
                DisplayName = "File Logger Background Service",
                StartType = ServiceStartMode.Automatic
            };

            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
```

### Step 3 — Program.cs Entry Point

```csharp
using System.ServiceProcess;

namespace LoggerService
{
    static class Program
    {
        static void Main()
        {
            ServiceBase.Run(new LoggerService());
        }
    }
}
```

### Step 4 — Install the Service

> Run **Developer Command Prompt as Administrator**:

```bash
# Install
installutil LoggerService.exe

# Start the service
net start FileLoggerService

# Verify log file
type C:\ServiceLogs\log.txt

# Stop the service
net stop FileLoggerService

# Uninstall
installutil /u LoggerService.exe
```

---

## Expected Output (log.txt)

```
Service Started at 25-03-2026 10:00:00
Heartbeat: 25-03-2026 10:00:05
Heartbeat: 25-03-2026 10:00:10
Service Stopped at 25-03-2026 10:00:15
```

---

## Viva Questions

1. What is a Windows Service? How does it differ from a normal application?
2. Which method runs when the service is started? Which method runs when stopped?
3. What is the purpose of `ServiceProcessInstaller`?
4. What command is used to install a Windows Service?
5. Under which account types can a service run? (LocalSystem, LocalService, NetworkService)

---

[Back to Index](../README.md)
