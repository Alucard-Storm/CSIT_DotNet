# Experiment 01 — Delegates and Callbacks

**Subject:** CSIT-406 .NET Framework Lab
**Location:** RGPV, Bhopal

---

## 1. Aim

To implement delegates and callbacks, demonstrating how methods can be passed as parameters using single-cast delegates, multicast delegates, and asynchronous callbacks.

## 2. Theory

A **delegate** in C# is a type-safe function pointer. It holds a reference to a method and can invoke that method at a later time. Delegates are essential for **callbacks**, a programming pattern where you pass a method as a parameter so that another method can execute it when a specific task is complete.

| Delegate Type | Explanation |
|---|---|
| **Single-Cast Delegate** | A delegate that refers to exactly one method. |
| **Multicast Delegate** | A delegate that refers to an invocation list of multiple methods. When invoked, it calls all connected methods in sequence (assembled using the `+=` operator). |
| **Asynchronous Callback** | A pattern where a method executes on a background thread using `BeginInvoke`, allowing the main program to continue running without freezing. |

*Instructional Example:* Consider a graphical user interface. A button does not need to know what action to perform when clicked. It simply uses a delegate. When the button is clicked, it invokes the delegate, which in turn calls the specific method you provided (the callback) to handle that click event.

---

## 3. Implementation Code

### Part A: Single-Cast Delegate

This code demonstrates how to declare a delegate, assign a single method to it, and execute it.

```csharp
using System;

namespace DelegateDemo
{
    // Step 1: Declare the delegate with a specific signature
    delegate int MathOperation(int a, int b);

    class Program
    {
        static int Add(int a, int b) => a + b;
        static int Multiply(int a, int b) => a * b;

        static void Main(string[] args)
        {
            // Point the delegate to the Add method
            MathOperation operation = Add;
            Console.WriteLine("Add(5, 3) = " + operation(5, 3));

            // Reassign the same delegate to the Multiply method
            operation = Multiply;
            Console.WriteLine("Multiply(5, 3) = " + operation(5, 3));
        }
    }
}
```

### Part B: Multicast Delegate

This segment shows how to attach multiple methods to a single delegate so they all run sequentially.

```csharp
using System;

namespace MulticastDemo
{
    delegate void NotificationHandler(string message);

    class Program
    {
        static void LogToConsole(string message)
        {
            Console.WriteLine("[Console] " + message);
        }

        static void LogToFile(string message)
        {
            Console.WriteLine("[File] Writing: " + message);
        }

        static void Main(string[] args)
        {
            // Assign the first method
            NotificationHandler notifier = LogToConsole;
            
            // Append the second method using +=
            notifier += LogToFile;   

            // Invoking the delegate calls both methods automatically
            Console.WriteLine("Sending notification...");
            notifier("Transaction complete.");
        }
    }
}
```

### Part C: Asynchronous Callback utilizing BeginInvoke

This example uses a delegate to run a long-lasting task in the background, preventing the main program from stopping.

```csharp
using System;
using System.Threading;

namespace AsyncCallbackDemo
{
    delegate void BackgroundTaskDelegate();

    class Program
    {
        static void ExecuteLongRunningTask()
        {
            Console.WriteLine("Worker Thread: Doing work...");
            Thread.Sleep(2000); // Simulate a 2-second delay
            Console.WriteLine("Worker Thread: Work finished.");
        }

        static void Main(string[] args)
        {
            BackgroundTaskDelegate backgroundTask = ExecuteLongRunningTask;

            Console.WriteLine("Main Thread: Starting background task.");

            // BeginInvoke starts the task on a separate thread
            IAsyncResult result = backgroundTask.BeginInvoke(
                new AsyncCallback(CompletionCallback),
                null
            );

            Console.WriteLine("Main Thread: Continuing with other work while background task runs...");
            
            // Wait for the background task to completely finish before closing
            backgroundTask.EndInvoke(result);  
        }

        static void CompletionCallback(IAsyncResult ar)
        {
            Console.WriteLine("Callback Thread: The background task has successfully completed!");
        }
    }
}
```

---

## 4. Expected Output

**Output - Part A:**
```text
Add(5, 3) = 8
Multiply(5, 3) = 15
```

**Output - Part B:**
```text
Sending notification...
[Console] Transaction complete.
[File] Writing: Transaction complete.
```

**Output - Part C:**
```text
Main Thread: Starting background task.
Main Thread: Continuing with other work while background task runs...
Worker Thread: Doing work...
Worker Thread: Work finished.
Callback Thread: The background task has successfully completed!
```

---

## 5. Viva / Discussion Questions

1. **Definition:** What is a delegate in C#, and how is it similar to a function pointer in C language?
2. **Multicast execution:** What happens when you use the `+=` operator with a delegate?
3. **Threading:** What is the primary difference between calling a delegate directly versus using `BeginInvoke()`?
4. **Built-in Delegates:** Name two default generic delegates provided correctly by the .NET Framework (for example, Action and Func).
5. **Generics:** How is `Action<T>` different in usage from `Func<T, TResult>`?

---

[Back to Main Index](../README.md)
