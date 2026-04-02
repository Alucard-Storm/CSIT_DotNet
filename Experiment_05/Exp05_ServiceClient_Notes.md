# Experiment 05 — Controlling a Windows Service from Code | Notes

---

## What is ServiceController?

`ServiceController` allows you to write C# code that controls a running Windows Service. Instead of going to the Services screen in Windows, you can write a program that starts, stops, or monitors a service for you.

Think of it like a **TV remote control** — you don't need to physically touch the TV (the service). You just press a button on the remote (your C# code) and it responds.

---

## How to Connect to a Service

```csharp
ServiceController controller = new ServiceController("FileLoggerService");
```

Replace `"FileLoggerService"` with the exact `ServiceName` you used when creating your service.

---

## Reading Current Status

Always call `Refresh()` first to make sure you have the latest information.

```csharp
controller.Refresh();
Console.WriteLine("Current Status: " + controller.Status);  
// Output: Current Status: Stopped
```

---

## Starting and Stopping a Service

```csharp
// Starting
if (controller.Status == ServiceControllerStatus.Stopped)
{
    controller.Start();
    controller.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));
    Console.WriteLine("Service started.");
}
```

```csharp
// Stopping
if (controller.Status == ServiceControllerStatus.Running)
{
    controller.Stop();
    controller.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(10));
    Console.WriteLine("Service stopped.");
}
```

**Why use `WaitForStatus()`?**
Starting or stopping a service takes a few seconds. Without `WaitForStatus()`, your code would check the status immediately before the service has even had time to fully start. It would get the wrong answer. `WaitForStatus()` pauses your code until the service is fully ready.

**Sample Console Session:**
```
Check Status → Current Status: Stopped
Start Service → Starting... → Service started.
Check Status → Current Status: Running
Stop Service  → Stopping... → Service stopped.
```

---

## Key Points to Remember

| Method or Property | What it does |
|---|---|
| `controller.Refresh()` | Gets the latest status from Windows |
| `controller.Status` | Tells if service is Running/Stopped/Paused |
| `controller.Start()` | Sends a "Start" signal to the service |
| `controller.Stop()` | Sends a "Stop" signal to the service |
| `WaitForStatus(state, timeout)` | Waits patiently until the service changes to the target state |
