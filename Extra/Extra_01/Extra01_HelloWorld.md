# Extra Experiment 01 — Hello World

**Subject:** CSIT-406 .NET Framework Lab
**Location:** RGPV, Bhopal

---

## 1. Aim

To write the first C# console application that prints "Hello, World!" to the screen, and to understand the basic anatomy of a C# program.

## 2. Theory

Every C# program starts with a **namespace**, contains one or more **classes**, and has a **`Main` method** as its entry point — the first method the runtime calls when the program starts.

| Keyword / Concept | Explanation |
|---|---|
| `using System` | Imports the `System` namespace so we can use built-in classes like `Console` without typing the full path. |
| `namespace` | A logical container that groups related classes together and avoids name conflicts. |
| `class` | A blueprint that contains data and behaviour. Every C# program needs at least one class. |
| `static void Main(string[] args)` | The entry point of the application. `static` means it belongs to the class, not an instance. `args` holds any command-line arguments. |
| `Console.WriteLine()` | Writes a line of text to the console window and moves the cursor to the next line. |
| `Console.ReadLine()` | Pauses execution and waits for the user to press Enter — useful to keep the console window open. |

*Instructional Note:* The `System` namespace is part of the .NET Framework Class Library (FCL) — a vast collection of pre-built classes Microsoft provides so programmers do not have to build everything from scratch.

---

## 3. Implementation Code

### Part A: Classic Hello World

The simplest possible C# program.

```csharp
using System;

namespace HelloWorldDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
```

### Part B: Interactive Greeting

This version reads the user's name from the keyboard and prints a personalised message.

```csharp
using System;

namespace InteractiveHello
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Hello, " + name + "! Welcome to .NET Framework.");
            Console.WriteLine("Today's date: " + DateTime.Now.ToShortDateString());

            Console.WriteLine("\nPress Enter to exit...");
            Console.ReadLine();
        }
    }
}
```

### Part C: Console Formatting

Demonstrates changing console text colour and using `Console.Clear()`.

```csharp
using System;

namespace ConsoleFormattingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "My First .NET App";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=============================");
            Console.WriteLine("  Hello from .NET Framework! ");
            Console.WriteLine("=============================");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Running on .NET Framework " + Environment.Version);
            Console.ResetColor();

            Console.WriteLine("\nPress Enter to exit...");
            Console.ReadLine();
        }
    }
}
```

---

## 4. Expected Output

**Output - Part A:**
```text
Hello, World!
```

**Output - Part B (user types "Akshay"):**
```text
Enter your name: Akshay
Hello, Akshay! Welcome to .NET Framework.
Today's date: 07/04/2026

Press Enter to exit...
```

**Output - Part C:**
```text
=============================
  Hello from .NET Framework!
=============================
Running on .NET Framework 4.0.30319.42000

Press Enter to exit...
```
*(The first three lines appear in green, the version line in cyan.)*

---

## 5. Viva / Discussion Questions

1. What is the purpose of the `Main` method in a C# program?
2. What does `using System;` do? What happens if you remove it?
3. What is the difference between `Console.Write()` and `Console.WriteLine()`?
4. What is the difference between a `namespace` and a `class`?
5. What does `static` mean in the context of the `Main` method?
6. How would you print the current date and time using `DateTime.Now`?
7. What is the .NET Framework Class Library (FCL)?
