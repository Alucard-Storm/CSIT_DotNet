# Extra Experiment 01 — Hello World | Notes

---

## The Skeleton of Every C# Program

Every C# file you will ever write follows this skeleton. Memorise it.

```
using System;               ← import built-in tools

namespace YourAppName        ← logical container (your folder)
{
    class Program            ← the blueprint
    {
        static void Main()   ← START HERE — runtime calls this first
        {
            // your code
        }
    }
}
```

---

## The Three Print Methods

| Method | Behaviour | Example |
|---|---|---|
| `Console.Write("Hi")` | Prints, cursor stays on same line | `HiHi` |
| `Console.WriteLine("Hi")` | Prints + moves to next line | `Hi` then new line |
| `Console.ReadLine()` | Waits for user to type + press Enter | Returns a `string` |

```csharp
Console.Write("Hello ");      // cursor stays on same line
Console.Write("World");       // prints right after
// Output: Hello World

Console.WriteLine("Hello");  // cursor moves to next line
Console.WriteLine("World");
// Output:
// Hello
// World
```

---

## String Concatenation (Joining text together)

Three ways to combine strings and variables:

```csharp
string name = "Akshay";

// Method 1: + operator (simple)
Console.WriteLine("Hello, " + name + "!");

// Method 2: String.Format (cleaner for multiple values)
Console.WriteLine(String.Format("Hello, {0}!", name));

// Method 3: String interpolation (modern, preferred)
Console.WriteLine($"Hello, {name}!");

// All three output: Hello, Akshay!
```

---

## Key Points to Remember

- A C# program must have **exactly one `Main` method** as the entry point.
- `Console.WriteLine()` adds a **newline** at the end; `Console.Write()` does not.
- `Console.ReadLine()` returns a **string** — even if the user types a number.
- `DateTime.Now` gives the current date and time.
- `Environment.Version` shows the .NET runtime version.

---

## Common Mistake

Forgetting `using System;` at the top:

```csharp
// Without "using System;" you must write the full path:
System.Console.WriteLine("Hello");  // works but verbose

// With "using System;" at the top:
Console.WriteLine("Hello");         // cleaner
```

---

## Quick Reference

```csharp
Console.WriteLine("Hello, World!");            // print text
Console.Write("Enter name: ");                // print without newline
string input = Console.ReadLine();            // read user input
Console.ForegroundColor = ConsoleColor.Green; // change text colour
Console.ResetColor();                          // restore default colour
Console.Clear();                               // clear the console window
Console.Title = "My App";                     // set window title bar text
```
