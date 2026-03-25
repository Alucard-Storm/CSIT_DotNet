# Experiment 01 — Delegates and Callbacks

**Subject:** CSIT-406 .NET Framework Lab
**RGPV, Bhopal**

---

## Aim

Implement delegates and callbacks to enable methods to be passed as parameters.
Demonstrate single-cast delegates, multicast delegates, and asynchronous callbacks.

---

## Theory

A **delegate** in C# is a type-safe function pointer — it holds a reference to a method and can invoke it later. Delegates enable **callbacks**, where you pass a method as a parameter so another method can call it back when needed.

| Type | Description |
|------|-------------|
| Single-cast | Points to one method |
| Multicast | Points to multiple methods (uses `+=`) |
| Async callback | Invokes method on a background thread using `BeginInvoke` |

> Real-world analogy: An event handler in a button click is a delegate — you pass the method `OnClick` to the button, and it calls it back when clicked.

---

## Code

### Part A — Single-Cast Delegate

```csharp
using System;

namespace DelegateDemo
{
    // Step 1: Declare delegate
    delegate int MathOperation(int a, int b);

    class Program
    {
        static int Add(int a, int b) => a + b;
        static int Multiply(int a, int b) => a * b;

        static void Main()
        {
            // Single-cast: points to Add
            MathOperation op = Add;
            Console.WriteLine("Add(5, 3) = " + op(5, 3));

            // Reassign to Multiply
            op = Multiply;
            Console.WriteLine("Multiply(5, 3) = " + op(5, 3));
        }
    }
}
```

### Part B — Multicast Delegate

```csharp
using System;

namespace MulticastDemo
{
    delegate void Notify(string message);

    class Program
    {
        static void LogToConsole(string msg) =>
            Console.WriteLine("[Console] " + msg);

        static void LogToFile(string msg) =>
            Console.WriteLine("[File] Writing: " + msg);

        static void Main()
        {
            Notify notifier = LogToConsole;
            notifier += LogToFile;   // Multicast: both methods invoked

            notifier("Order Placed!");  // Calls both
        }
    }
}
```

### Part C — Asynchronous Callback

```csharp
using System;
using System.Threading;

namespace AsyncCallbackDemo
{
    delegate void WorkDelegate();

    class Program
    {
        static void DoWork()
        {
            Console.WriteLine("Working on background thread...");
            Thread.Sleep(2000);
            Console.WriteLine("Work done!");
        }

        static void Main()
        {
            WorkDelegate work = DoWork;

            // BeginInvoke runs method asynchronously
            IAsyncResult result = work.BeginInvoke(
                ar => Console.WriteLine("Callback: Async work completed!"),
                null
            );

            Console.WriteLine("Main thread continues...");
            work.EndInvoke(result);  // Wait for completion
        }
    }
}
```

---

## Expected Output

**Part A:**
```
Add(5, 3) = 8
Multiply(5, 3) = 15
```

**Part B:**
```
[Console] Order Placed!
[File] Writing: Order Placed!
```

**Part C:**
```
Main thread continues...
Working on background thread...
Work done!
Callback: Async work completed!
```

---

## Viva Questions

1. What is a delegate in C#? How is it different from a function pointer in C?
2. What happens when you use `+=` with a delegate?
3. What is the difference between `Invoke` and `BeginInvoke`?
4. Name two built-in generic delegates in .NET.
5. How is `Action<T>` different from `Func<T, TResult>`?

---

[Back to Index](../README.md)
