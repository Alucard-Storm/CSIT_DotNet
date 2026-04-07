# Extra Experiment 04 — Arrays and Collections

**Subject:** CSIT-406 .NET Framework Lab
**Location:** RGPV, Bhopal

---

## 1. Aim

To declare and use arrays for fixed-size data storage, and to use generic collections (`List<T>` and `Dictionary<TKey, TValue>`) for dynamic data management in C#.

## 2. Theory

When you need to store multiple values of the same type, using separate variables (`student1`, `student2`, `student3`, …) is impractical. C# provides several structures.

| Structure | Namespace | Size | Key Feature |
|---|---|---|---|
| `Array` (`T[]`) | `System` | Fixed at creation | Fast, indexed access; size cannot change |
| `List<T>` | `System.Collections.Generic` | Dynamic | Add/Remove items at any time |
| `Dictionary<K,V>` | `System.Collections.Generic` | Dynamic | Key-value pairs; look up values by key |
| `int[,]` (2-D array) | `System` | Fixed | Rows and columns, like a table or matrix |

**Indexing** in C# arrays starts at **0** — the first element is at index `[0]`, the last is at `[Length - 1]`.

The `foreach` loop is a clean way to iterate over any collection without managing an index variable manually.

---

## 3. Implementation Code

### Part A: Single-Dimensional Array — Student Marks

```csharp
using System;

namespace ArrayDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Declare and initialise an array of 5 integers
            int[] marks = { 85, 72, 91, 60, 78 };

            Console.WriteLine("--- Mark Sheet ---");
            int total = 0;

            for (int i = 0; i < marks.Length; i++)
            {
                Console.WriteLine($"Student {i + 1}: {marks[i]}");
                total += marks[i];
            }

            double average = (double)total / marks.Length;
            Console.WriteLine($"\nTotal:   {total}");
            Console.WriteLine($"Average: {average:F2}");

            // Find highest and lowest using Array helper methods
            Array.Sort(marks);
            Console.WriteLine($"Lowest:  {marks[0]}");
            Console.WriteLine($"Highest: {marks[marks.Length - 1]}");
        }
    }
}
```

### Part B: Two-Dimensional Array — Matrix Addition

```csharp
using System;

namespace MatrixDemo
{
    class Program
    {
        static void PrintMatrix(int[,] m, string label)
        {
            Console.WriteLine(label);
            for (int r = 0; r < m.GetLength(0); r++)
            {
                for (int c = 0; c < m.GetLength(1); c++)
                    Console.Write($"{m[r, c],4}");
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            int[,] A = { { 1, 2, 3 }, { 4, 5, 6 } };
            int[,] B = { { 7, 8, 9 }, { 1, 2, 3 } };
            int[,] C = new int[2, 3];

            // Add corresponding elements
            for (int r = 0; r < 2; r++)
                for (int c = 0; c < 3; c++)
                    C[r, c] = A[r, c] + B[r, c];

            PrintMatrix(A, "Matrix A:");
            PrintMatrix(B, "Matrix B:");
            PrintMatrix(C, "A + B:");
        }
    }
}
```

### Part C: `List<T>` — Dynamic To-Do List

```csharp
using System;
using System.Collections.Generic;

namespace ListDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> tasks = new List<string>();

            // Add items
            tasks.Add("Complete lab record");
            tasks.Add("Study for viva");
            tasks.Add("Submit assignment");
            tasks.Add("Revise ADO.NET notes");

            Console.WriteLine($"Tasks ({tasks.Count} total):");
            foreach (string task in tasks)
                Console.WriteLine("  [ ] " + task);

            // Remove an item
            tasks.Remove("Study for viva");

            // Check if an item exists
            bool hasAdoNet = tasks.Contains("Revise ADO.NET notes");
            Console.WriteLine($"\nContains ADO.NET task: {hasAdoNet}");

            // Sort alphabetically
            tasks.Sort();
            Console.WriteLine("\nSorted tasks:");
            for (int i = 0; i < tasks.Count; i++)
                Console.WriteLine($"  {i + 1}. {tasks[i]}");

            // Convert to array if needed
            string[] arr = tasks.ToArray();
            Console.WriteLine($"\nConverted to array. Length: {arr.Length}");
        }
    }
}
```

### Part D: `Dictionary<TKey, TValue>` — Student Directory

```csharp
using System;
using System.Collections.Generic;

namespace DictionaryDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Key: roll number (int), Value: student name (string)
            Dictionary<int, string> directory = new Dictionary<int, string>();

            // Add entries
            directory.Add(101, "Akshay Sagar");
            directory.Add(102, "Diksha Pawar");
            directory.Add(103, "Pawan Tiwari");
            directory[104] = "Divya Khade";    // alternative add syntax

            // Look up by key
            Console.WriteLine("Roll 102: " + directory[102]);

            // Safe lookup with TryGetValue
            int searchRoll = 105;
            if (directory.TryGetValue(searchRoll, out string studentName))
                Console.WriteLine($"Found: {studentName}");
            else
                Console.WriteLine($"Roll {searchRoll} not found.");

            // Iterate over all entries
            Console.WriteLine("\n--- Full Directory ---");
            foreach (KeyValuePair<int, string> entry in directory)
                Console.WriteLine($"Roll {entry.Key}: {entry.Value}");

            // Check and remove
            directory.Remove(103);
            Console.WriteLine($"\nAfter removal, count: {directory.Count}");
        }
    }
}
```

---

## 4. Expected Output

**Output - Part A:**
```text
--- Mark Sheet ---
Student 1: 85
Student 2: 72
Student 3: 91
Student 4: 60
Student 5: 78

Total:   386
Average: 77.20
Lowest:  60
Highest: 91
```

**Output - Part B:**
```text
Matrix A:
   1   2   3
   4   5   6
Matrix B:
   7   8   9
   1   2   3
A + B:
   8  10  12
   5   7   9
```

**Output - Part C:**
```text
Tasks (4 total):
  [ ] Complete lab record
  [ ] Study for viva
  [ ] Submit assignment
  [ ] Revise ADO.NET notes

Contains ADO.NET task: True

Sorted tasks:
  1. Complete lab record
  2. Revise ADO.NET notes
  3. Submit assignment

Converted to array. Length: 3
```

**Output - Part D:**
```text
Roll 102: Diksha Pawar
Roll 105 not found.

--- Full Directory ---
Roll 101: Akshay Sagar
Roll 102: Diksha Pawar
Roll 103: Pawan Tiwari
Roll 104: Divya Khade

After removal, count: 3
```

---

## 5. Viva / Discussion Questions

1. What is the difference between an array and a `List<T>`?
2. What happens when you access an array at an index beyond its bounds?
3. How does `foreach` differ from a `for` loop for iterating a collection?
4. What is the purpose of `TryGetValue` in a `Dictionary`? Why is it safer than `dictionary[key]`?
5. What does the `<T>` in `List<T>` mean? What does "generic" mean?
6. How would you sort an array in descending order using `Array.Sort`?
7. What is `GetLength(0)` on a 2-D array? How does it differ from `Length`?
