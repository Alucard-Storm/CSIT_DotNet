# Experiment 05 — Client Application to Control a Windows Service

**Subject:** CSIT-406 .NET Framework Lab
**RGPV, Bhopal**

---

## Aim

Create a client application to start, stop, and monitor a Windows Service.

---

## Theory

The `System.ServiceProcess.ServiceController` class allows any .NET application to interact with Windows Services programmatically — no command line needed.

| Method / Property | Description |
|---|---|
| `ServiceController(name)` | Connect to a named service |
| `Start()` | Start the service |
| `Stop()` | Stop the service |
| `Pause()` | Pause the service |
| `Continue()` | Resume a paused service |
| `Refresh()` | Update the `Status` property |
| `Status` | Returns current state: `Running`, `Stopped`, `Paused`, etc. |
| `WaitForStatus(state)` | Block until service reaches the given state |

> Real-world analogy: Task Manager lets you start/stop processes — `ServiceController` is the programmatic equivalent for services.

---

## Prerequisite

Make sure the service from **Experiment 04** (`FileLoggerService`) is installed before running this client.

---

## Code

### Console Client

```csharp
using System;
using System.ServiceProcess;

namespace ServiceControlClient
{
    class Program
    {
        static void Main()
        {
            string serviceName = "FileLoggerService";

            Console.WriteLine("=== Windows Service Controller ===\n");

            while (true)
            {
                Console.WriteLine("\nOptions:");
                Console.WriteLine("1. Check Status");
                Console.WriteLine("2. Start Service");
                Console.WriteLine("3. Stop Service");
                Console.WriteLine("4. Pause Service");
                Console.WriteLine("5. Resume Service");
                Console.WriteLine("6. Exit");
                Console.Write("\nEnter choice: ");

                string choice = Console.ReadLine();

                using (ServiceController sc = new ServiceController(serviceName))
                {
                    try
                    {
                        sc.Refresh();

                        switch (choice)
                        {
                            case "1":
                                Console.WriteLine($"Status: {sc.Status}");
                                Console.WriteLine($"Display Name: {sc.DisplayName}");
                                Console.WriteLine($"Can Stop: {sc.CanStop}");
                                Console.WriteLine($"Can Pause: {sc.CanPauseAndContinue}");
                                break;

                            case "2":
                                if (sc.Status == ServiceControllerStatus.Stopped)
                                {
                                    sc.Start();
                                    sc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));
                                    Console.WriteLine("Service started successfully.");
                                }
                                else
                                    Console.WriteLine("Service is already running.");
                                break;

                            case "3":
                                if (sc.Status == ServiceControllerStatus.Running)
                                {
                                    sc.Stop();
                                    sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(10));
                                    Console.WriteLine("Service stopped successfully.");
                                }
                                else
                                    Console.WriteLine("Service is not running.");
                                break;

                            case "4":
                                if (sc.CanPauseAndContinue && sc.Status == ServiceControllerStatus.Running)
                                {
                                    sc.Pause();
                                    sc.WaitForStatus(ServiceControllerStatus.Paused, TimeSpan.FromSeconds(10));
                                    Console.WriteLine("Service paused.");
                                }
                                else
                                    Console.WriteLine("Service cannot be paused.");
                                break;

                            case "5":
                                if (sc.Status == ServiceControllerStatus.Paused)
                                {
                                    sc.Continue();
                                    sc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));
                                    Console.WriteLine("Service resumed.");
                                }
                                else
                                    Console.WriteLine("Service is not paused.");
                                break;

                            case "6":
                                Console.WriteLine("Exiting...");
                                return;

                            default:
                                Console.WriteLine("Invalid choice.");
                                break;
                        }
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        Console.WriteLine("Make sure the service is installed.");
                    }
                }
            }
        }
    }
}
```

---

## Expected Output

```
=== Windows Service Controller ===

Options:
1. Check Status  2. Start  3. Stop  4. Pause  5. Resume  6. Exit
Enter choice: 1
Status: Stopped
Display Name: File Logger Background Service
Can Stop: True

Enter choice: 2
Service started successfully.

Enter choice: 1
Status: Running
```

---

## Viva Questions

1. Which namespace contains the `ServiceController` class?
2. What does `WaitForStatus()` do? Why is it needed?
3. What happens if you call `Start()` on an already running service?
4. How would you list all services installed on a machine using `ServiceController`?
5. What permission level is required to start/stop a Windows Service?

---

[Back to Index](../README.md)
