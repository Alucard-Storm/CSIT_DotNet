# Experiment 08 — Memory and Performance: String vs StringBuilder

**Subject:** CSIT-406 .NET Framework Lab
**Location:** RGPV, Bhopal

---

## 1. Aim

To test and understand the major performance differences between the standard `String` type and the `StringBuilder` class.

## 2. Theory

In C#, there are two main ways to work with text, but they handle computer memory very differently. 

The standard `String` is **immutable**, meaning it cannot be changed once created. If you try to add text to an existing string, the computer actually destroys the old string and creates a completely new one in memory.

The `StringBuilder` is **mutable**, meaning it can be changed. If you use it to add text, it simply adds the new text to the existing memory space without destroying anything.

| Feature | `String` | `StringBuilder` |
|---|---|---|
| **Memory Storage** | Creates a newly allocated memory block every time you make a change. | Keeps a single "buffer" box and updates the text inside that box. |
| **Speed** | Very slow if you are changing or adding text inside a loop (`for` or `while`). | Very fast for changing text, especially inside large loops. |
| **Best Used For** | Small text like names, or text you do not plan to change. | Building large documents, generating logs, or appending text inside loops. |

*Instructional Example:* Using `String` is like writing a sentence on paper, realizing you need to add a word, throwing the whole paper in the trash, and re-writing the entire new sentence on a fresh piece of paper. Using `StringBuilder` is like using an erasable whiteboard where you can freely add words to the end without throwing the board away.

---

## 3. Implementation Code

### Part A: Standard String Performance Test

This code shows what happens when you accidentally use a regular string inside a large loop. It creates 10,000 new, separate memory objects.

```csharp
// File: StringTest.cs
using System;
using System.Diagnostics;

namespace StringVsBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running String test (Warning: This is slow)...");
            
            // Stopwatch helps us time how long the code takes
            Stopwatch timer = Stopwatch.StartNew();
            
            string normalString = "";
            
            // This loop forces the computer to create 10,000 new strings
            for (int i = 0; i < 10000; i++)
            {
                normalString += i.ToString();
            }
            
            timer.Stop();
            
            Console.WriteLine("Normal String Time: " + timer.ElapsedMilliseconds + " milliseconds.");
            Console.WriteLine("Final Text Length: " + normalString.Length);
        }
    }
}
```

### Part B: StringBuilder Performance Test

This code uses `StringBuilder` to do the exact same task but much faster.

```csharp
// File: StringBuilderTest.cs
using System;
using System.Text;
using System.Diagnostics;

namespace StringVsBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running StringBuilder test...");
            
            Stopwatch timer = Stopwatch.StartNew();
            
            // Instantiating the builder
            StringBuilder fastString = new StringBuilder();
            
            // This loop updates the text without creating new memory objects
            for (int i = 0; i < 10000; i++)
            {
                fastString.Append(i.ToString());
            }
            
            timer.Stop();
            
            Console.WriteLine("StringBuilder Time: " + timer.ElapsedMilliseconds + " milliseconds.");
            Console.WriteLine("Final Text Length: " + fastString.Length);
        }
    }
}
```

---

## 4. Expected Output

*(Exact times will vary based on computer speed)*

```text
Running String test (Warning: This is slow)...
Normal String Time: 85 milliseconds.
Final Text Length: 38890

Running StringBuilder test...
StringBuilder Time: 2 milliseconds.
Final Text Length: 38890
```

*Note:* Both methods resulted in a text string of the exact same length (`38890` characters), but the `StringBuilder` finished the job 40 times faster because it didn't have to keep allocating new memory.

---

## 5. Viva / Discussion Questions

1. **Definitions:** What do the terms "Immutable" and "Mutable" mean in programming memory?
2. **Namespaces:** What specific C# namespace do you need to import to use the `StringBuilder` class?
3. **Methods:** What is the difference between writing `str += "word"` and writing `fastStr.Append("word")`?
4. **Garbage Collection:** Why does using a regular `String` inside a 10,000-iteration loop cause the computer's Garbage Collector to work so hard?
5. **Practical Use:** If you only need to join three words together to create a simple sentence, should you use `String` or `StringBuilder`? Why?

---

[Back to Main Index](../README.md)
