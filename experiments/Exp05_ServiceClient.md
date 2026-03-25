# Experiment 05 — Client Application to Control a Windows Service

**Subject:** CSIT-406 .NET Framework Lab
**Location:** RGPV, Bhopal

---

## 1. Aim

To design a simple client console application that can successfully start, stop, and monitor a Windows Service running on the local computer.

## 2. Theory

The `.NET Framework` allows developers to control running services without needing to type commands in the terminal. You can write code to control background programs using the `System.ServiceProcess.ServiceController` class. 

### Core Properties and Methods of `ServiceController`

| Property or Method | Explanation |
|---|---|
| **`ServiceController("Name")`** | Connects your code to a specifically named Windows Service. |
| **`Start()` / `Stop()`** | Sends a command telling Windows to start or stop the chosen service. |
| **`Pause()` / `Continue()`** | Sends a command to temporarily freeze or resume the service. |
| **`Refresh()`** | Asks Windows for the very latest updates on whether the service is currently running or stopped. |
| **`Status`** | Tells you if the service is currently `Running`, `Stopped`, or `Paused`. |
| **`WaitForStatus()`** | Tells your code to wait patiently until the service fully reaches the state you requested (for example, wait until it is fully stopped). |

*Instructional Example:* Think about opening the Windows Task Manager. In Task Manager, you can right-click a program to end it immediately. The `ServiceController` class is simply the C# programming equivalent of doing that task.

---

## 3. Prerequisite Setup

Before testing this client code, please make sure the background service you created in **Experiment 04** (`FileLoggerService`) is successfully installed on your computer.

---

## 4. Implementation Code

### Console Client Controller

*Instruction:* Create a **Console Application (.NET Framework)** project and make sure to add a reference to `System.ServiceProcess`.

```csharp
// File: Program.cs
using System;
using System.ServiceProcess;

namespace ServiceControlClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // Enter the exact name of your installed background service
            string targetServiceName = "FileLoggerService";

            Console.WriteLine("=== Windows Service Manager ===");

            while (true)
            {
                Console.WriteLine("\nMenu Options:");
                Console.WriteLine("1. View Current Status");
                Console.WriteLine("2. Start Service");
                Console.WriteLine("3. Stop Service");
                Console.WriteLine("4. Exit Program");
                Console.Write("\nEnter number choice: ");

                string userChoice = Console.ReadLine();

                // Connect to the service
                using (ServiceController controller = new ServiceController(targetServiceName))
                {
                    try
                    {
                        // Get the latest status from Windows
                        controller.Refresh();

                        switch (userChoice)
                        {
                            case "1":
                                Console.WriteLine($"\nCurrent Status: {controller.Status}");
                                Console.WriteLine($"Display Name: {controller.DisplayName}");
                                Console.WriteLine($"Can Be Stopped: {controller.CanStop}");
                                break;

                            case "2":
                                if (controller.Status == ServiceControllerStatus.Stopped)
                                {
                                    Console.WriteLine("Starting service...");
                                    controller.Start();
                                    
                                    // Make sure it fully starts before moving on
                                    controller.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));
                                    Console.WriteLine("Success: Service is now running.");
                                }
                                else
                                {
                                    Console.WriteLine("Notice: Service is already running.");
                                }
                                break;

                            case "3":
                                if (controller.Status == ServiceControllerStatus.Running)
                                {
                                    Console.WriteLine("Stopping service...");
                                    controller.Stop();
                                    
                                    // Make sure it fully stops before moving on
                                    controller.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(10));
                                    Console.WriteLine("Success: Service was stopped.");
                                }
                                else
                                {
                                    Console.WriteLine("Notice: Service is not currently running.");
                                }
                                break;

                            case "4":
                                Console.WriteLine("Closing the program...");
                                return;

                            default:
                                Console.WriteLine("Error: Invalid choice entered.");
                                break;
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        // Happens if the service name is wrong or not installed
                        Console.WriteLine("\nError: The service could not be found. Is it installed?");
                    }
                    catch (UnauthorizedAccessException)
                    {
                        // Happens if you don't run Visual Studio as an Administrator
                        Console.WriteLine("\nSecurity Error: Please run this code as an Administrator.");
                    }
                }
            }
        }
    }
}
```

---

## 5. Expected Output

```text
=== Windows Service Manager ===

Menu Options:
1. View Current Status
2. Start Service
3. Stop Service
4. Exit Program

Enter number choice: 1

Current Status: Stopped
Display Name: Enterprise File Logger
Can Be Stopped: True

Enter number choice: 2
Starting service...
Success: Service is now running.

Enter number choice: 1

Current Status: Running
Display Name: Enterprise File Logger
Can Be Stopped: True
```

---

## 6. Viva / Discussion Questions

1. **Namespaces:** Which `.NET Framework` namespace provides the `ServiceController` class?
2. **Waiting:** Why is it safer to use `WaitForStatus()` rather than just calling `Start()`?
3. **Information Synchronization:** Why must you call the `Refresh()` method before checking the `Status` property?
4. **Error Scenarios:** What exception is thrown if you try to stop a service but your program is not running as an Administrator?
5. **Computer Verification:** Explain how you can check that the service successfully started, other than using this C# code (e.g., Services menu, Log files).

---

[Back to Main Index](../README.md)
