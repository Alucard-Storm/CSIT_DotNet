# Experiment 08 — String vs StringBuilder Comparison

**Subject:** CSIT-406 .NET Framework Lab
**RGPV, Bhopal**

---

## Aim

Compare string operations using `String` and `StringBuilder` for performance and behavior.

---

## Theory

| Feature | `String` | `StringBuilder` |
|---|---|---|
| Mutability | Immutable — new object on every change | Mutable — modifies in place |
| Memory | New allocation per concatenation | Single buffer, resized as needed |
| Performance | Slow in loops (many small objects) | Fast in loops |
| Namespace | `System` | `System.Text` |
| Thread Safety | Thread-safe (immutable) | Not thread-safe |
| Use Case | Few, fixed concatenations | Many, dynamic concatenations |

**Key Rule:** Use `String` for ≤ 10 concatenations; use `StringBuilder` for loops or dynamic building.

> Real-world analogy: `String` is like erasing and rewriting on a whiteboard (costly). `StringBuilder` is like appending to a notepad (efficient).

---

## Code

### Part A — Basic Operations Comparison

```csharp
using System;
using System.Text;

namespace StringVsBuilder
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== String Operations ===");
            string str = "Hello";
            str += " World";       // Creates new object
            str = str.ToUpper();
            str = str.Replace("WORLD", "RGPV");
            str = str.Trim();
            Console.WriteLine(str);
            Console.WriteLine("Length: " + str.Length);
            Console.WriteLine("Contains 'RGPV': " + str.Contains("RGPV"));
            Console.WriteLine("Substring(6): " + str.Substring(6));

            Console.WriteLine("\n=== StringBuilder Operations ===");
            StringBuilder sb = new StringBuilder("Hello");
            sb.Append(" World");       // Modifies in-place
            sb.Replace("World", "RGPV");
            sb.Insert(0, ">> ");
            sb.AppendLine(" <<");
            sb.AppendFormat("Length: {0}", sb.Length);
            Console.WriteLine(sb.ToString());
        }
    }
}
```

### Part B — Performance Benchmark

```csharp
using System;
using System.Text;
using System.Diagnostics;

namespace PerformanceTest
{
    class Program
    {
        static void Main()
        {
            int iterations = 10000;

            // Test String
            Stopwatch sw = Stopwatch.StartNew();
            string result = "";
            for (int i = 0; i < iterations; i++)
                result += i.ToString();   // 10,000 new objects created!
            sw.Stop();
            Console.WriteLine($"String ({iterations} concat): {sw.ElapsedMilliseconds} ms");

            // Test StringBuilder
            sw.Restart();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < iterations; i++)
                sb.Append(i);             // Single buffer
            sw.Stop();
            Console.WriteLine($"StringBuilder ({iterations} append): {sw.ElapsedMilliseconds} ms");

            Console.WriteLine("\nFinal lengths match: " + (result.Length == sb.Length));
        }
    }
}
```

### Part C — Common String Methods

```csharp
using System;

namespace StringMethods
{
    class Program
    {
        static void Main()
        {
            string s = "  CSIT-406 Dot Net Lab  ";

            Console.WriteLine("Original     : '" + s + "'");
            Console.WriteLine("Trim         : '" + s.Trim() + "'");
            Console.WriteLine("ToLower      : " + s.Trim().ToLower());
            Console.WriteLine("Split('-')[0]: " + s.Trim().Split('-')[0]);
            Console.WriteLine("IndexOf('Net'): " + s.IndexOf("Net"));
            Console.WriteLine("StartsWith(' '): " + s.StartsWith(" "));
            Console.WriteLine("PadLeft(30)  : '" + s.Trim().PadLeft(30) + "'");

            // String.Format vs interpolation
            string name = "RGPV";
            int year = 2026;
            Console.WriteLine(String.Format("\nFormat: {0} - {1}", name, year));
            Console.WriteLine($"Interpolation: {name} - {year}");
        }
    }
}
```

---

## Expected Output

**Part A:**
```
=== String Operations ===
HELLO RGPV
Length: 10
Contains 'RGPV': True
Substring(6): RGPV

=== StringBuilder Operations ===
>> Hello RGPV <<
Length: 17
```

**Part B (approximate):**
```
String (10000 concat): 85 ms
StringBuilder (10000 append): 1 ms

Final lengths match: True
```

---

## Viva Questions

1. Why is `String` immutable in C#? What are the advantages?
2. In what scenario is `String` preferred over `StringBuilder`?
3. What is the default capacity of `StringBuilder`? How does it grow?
4. What does `sb.ToString()` do?
5. What is the difference between `String.Format()` and string interpolation (`$""`)?

---

[Back to Index](../README.md)
