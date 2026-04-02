# Experiment 04 — Background Windows Service

**Subject:** CSIT-406 .NET Framework Lab
**Location:** RGPV, Bhopal

---

## 1. Aim

To successfully build a Windows Service that runs in the background and install it on your computer using the Service Installer tool.

## 2. Theory

A **Windows Service** is a long-running background program that starts independently when Windows booting up. It continues to run without requiring a user to log in or click any buttons, and it is hidden from the normal desktop view.

You can manage all background services in Windows through the Services control panel.

### Important Classes (`System.ServiceProcess` Namespace)

| Class / Method | Explanation |
|---|---|
| **`ServiceBase`** | The main parent class. Any Windows Service you create must inherit from this class. |
| **`ServiceInstaller`** | The tool that tells Windows the name of your service and how it should start (automatically or manually). |
| **`ServiceProcessInstaller`** | The tool that tells Windows what security account your service should run under (such as LocalSystem). |
| **`OnStart(string[] args)`** | The method that Windows calls when the service is started. Put your setup code here. |
| **`OnStop()`** | The method that Windows calls when the service is stopped. Put your cleanup code here. |

*Instructional Example:* Think of anti-virus software like Windows Defender, or an automatic backup tool. They run silently behind the scenes to protect your computer or save files without you ever having to open a window.

---

## 3. Implementation Code

### Step 1: Create the Windows Service Project

*Instruction:* Inside Visual Studio, create a new **Windows Service (.NET Framework)** project.

```csharp
// File: LoggerService.cs
using System;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace WindowsServiceDemo
{
    public partial class LoggerService : ServiceBase
    {
        private Timer _intervalTimer;
        private readonly string _logFilePath = @"C:\ServiceLogs\SystemLog.txt";

        public LoggerService()
        {
            // Give your service a name so Windows recognizes it
            this.ServiceName = "FileLoggerService";
        }

        protected override void OnStart(string[] args)
        {
            // Create a folder if it does not exist
            Directory.CreateDirectory(@"C:\ServiceLogs");
            
            WriteLogEntry("The service started successfully at: " + DateTime.Now);

            // Set up a timer to run every 5 seconds (5000 milliseconds)
            _intervalTimer = new Timer(5000); 
            _intervalTimer.Elapsed += (sender, eventArgs) => WriteLogEntry("Heartbeat Check: " + DateTime.Now);
            _intervalTimer.Start();
        }

        protected override void OnStop()
        {
            // Clean up and stop the timer
            if (_intervalTimer != null)
            {
                _intervalTimer.Stop();
            }
            
            WriteLogEntry("The service was stopped at: " + DateTime.Now);
        }

        private void WriteLogEntry(string message)
        {
            // Write a new line of text to the log file on the hard drive
            File.AppendAllText(_logFilePath, message + Environment.NewLine);
        }
    }
}
```

### Step 2: Add Installers for Windows

Windows needs explicit instructions on how to install your service into the system registry.

```csharp
// File: LoggerServiceInstaller.cs
using System.ComponentModel;
using System.ServiceProcess;
using System.Configuration.Install;

namespace WindowsServiceDemo
{
    // The [RunInstaller] attribute tells the installation tool to read this class
    [RunInstaller(true)]
    public class LoggerServiceInstaller : Installer
    {
        public LoggerServiceInstaller()
        {
            // Tell Windows to use the highest level local machine account
            ServiceProcessInstaller processInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem
            };

            // Set the public display names that users will see in the Services menu
            ServiceInstaller serviceInstaller = new ServiceInstaller
            {
                ServiceName = "FileLoggerService",
                DisplayName = "Enterprise File Logger",
                Description = "A background service that records heartbeats every 5 seconds.",
                StartType = ServiceStartMode.Automatic
            };

            // Combine both installers so they run together
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
```

### Step 3: Service Entry Point

```csharp
// File: Program.cs
using System.ServiceProcess;

namespace WindowsServiceDemo
{
    static class Program
    {
        static void Main()
        {
            // The starting point that passes the service to the Windows operating system
            ServiceBase.Run(new LoggerService());
        }
    }
}
```

### Step 4: Install and Run the Service

*Instruction:* Since you are altering the system, you must open the **Developer Command Prompt** with **Administrator Rights**.

```bash
# 1. Install the service into Windows
installutil WindowsServiceDemo.exe

# 2. Start running the service
net start FileLoggerService

# 3. Check the text file to see if the service is writing heartbeats
type C:\ServiceLogs\SystemLog.txt

# 4. Stop running the service
net stop FileLoggerService

# 5. Uninstall the service to clean up your computer
installutil /u WindowsServiceDemo.exe
```

---

## 4. Expected Output Analysis (`SystemLog.txt`)

If you look inside the `C:\ServiceLogs\SystemLog.txt` file after 15 seconds, you will see output like this:

```text
The service started successfully at: 25-03-2026 10:00:00
Heartbeat Check: 25-03-2026 10:00:05
Heartbeat Check: 25-03-2026 10:00:10
The service was stopped at: 25-03-2026 10:00:12
```

---

## 5. Viva / Discussion Questions

1. **Service Definition:** How does a Windows Service operate differently from a traditional Desktop program (such as Microsoft Word)?
2. **Lifecycle Steps:** When do the `OnStart()` and `OnStop()` methods get called by the operating system?
3. **Security Contexts:** What is the `ServiceProcessInstaller` used for?
4. **Tool Commands:** What is the purpose of the `installutil` command-line tool?
5. **Operating System Accounts:** Name two different account types (such as LocalSystem) that a Windows service is permitted to run under.

---

[Back to Main Index](../README.md)
