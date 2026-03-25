# Experiment 04 — Windows Service | Notes

---

## What is a Windows Service?

A **Windows Service** is a background program that:
- Starts automatically when Windows starts
- Has no window or screen
- Runs silently until you tell it to stop

**Real examples you already know:**
- Anti-virus scanner (runs silently behind the scenes)
- Windows Update (checks for updates automatically)
- Automatic backup tools

---

## How a Windows Service Works (Lifecycle)

```
Windows Starts
      |
      v
[OnStart() is called]   ← Your setup code goes here
      |
      v
[Service keeps running] ← Timers, loops, file writing, etc.
      |
      v
[OnStop() is called]    ← Your cleanup code goes here
      |
      v
Service is stopped
```

---

## Minimal Code to Create a Service

```csharp
public class LoggerService : ServiceBase
{
    private Timer _timer;

    public LoggerService()
    {
        this.ServiceName = "FileLoggerService";  // Give it a name
    }

    protected override void OnStart(string[] args)
    {
        // This code runs when the service STARTS
        File.AppendAllText(@"C:\log.txt", "Started at: " + DateTime.Now + "\n");

        _timer = new Timer(5000);                          // Every 5 seconds
        _timer.Elapsed += (s, e) => File.AppendAllText(@"C:\log.txt", "Alive: " + DateTime.Now + "\n");
        _timer.Start();
    }

    protected override void OnStop()
    {
        // This code runs when the service STOPS
        _timer?.Stop();
        File.AppendAllText(@"C:\log.txt", "Stopped at: " + DateTime.Now + "\n");
    }
}
```

**What appears in the log file:**
```
Started at: 25-03-2026 10:00:00
Alive: 25-03-2026 10:00:05
Alive: 25-03-2026 10:00:10
Stopped at: 25-03-2026 10:00:12
```

---

## Installing the Service (Command Prompt — as Admin)

```bash
installutil LoggerService.exe    # Install
net start FileLoggerService      # Start
net stop FileLoggerService       # Stop
installutil /u LoggerService.exe # Uninstall
```

---

## Key Points to Remember

| Class | Role |
|---|---|
| `ServiceBase` | Parent class — every service must inherit this |
| `OnStart()` | Called when started — initialize here |
| `OnStop()` | Called when stopped — clean up here |
| `ServiceInstaller` | Registers your service in Windows |
| `installutil` | Tool to install/uninstall the `.exe` |
