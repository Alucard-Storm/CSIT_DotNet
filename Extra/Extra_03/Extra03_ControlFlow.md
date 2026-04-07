# Extra Experiment 03 — Control Flow

**Subject:** CSIT-406 .NET Framework Lab
**Location:** RGPV, Bhopal

---

## 1. Aim

To implement control flow structures in C# — including `if/else`, `switch`, `for`, `while`, and `do-while` — to direct program execution based on conditions and to repeat blocks of code.

## 2. Theory

**Control flow** determines the order in which statements execute. By default, C# runs statements top-to-bottom. Control structures alter this flow.

| Structure | Category | Purpose |
|---|---|---|
| `if / else if / else` | Conditional | Execute code only when a condition is true |
| `switch` | Conditional | Select one of many code blocks by matching a value |
| `for` | Loop | Repeat a block a known number of times |
| `while` | Loop | Repeat as long as a condition remains true (checked before each iteration) |
| `do-while` | Loop | Like `while`, but the body runs **at least once** (condition checked after) |
| `break` | Jump | Exit the nearest loop or `switch` immediately |
| `continue` | Jump | Skip the rest of the current loop iteration and move to the next |

*Instructional Note:* Choose `for` when you know how many iterations you need. Choose `while` when the number of iterations depends on a runtime condition. Use `do-while` when the body must execute at least once (e.g., showing a menu).

---

## 3. Implementation Code

### Part A: `if / else if / else` — Grade Evaluator

```csharp
using System;

namespace IfElseDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your marks (0-100): ");
            int marks = int.Parse(Console.ReadLine());

            string grade;
            string remark;

            if (marks >= 90)
            {
                grade  = "O";
                remark = "Outstanding";
            }
            else if (marks >= 75)
            {
                grade  = "A";
                remark = "Excellent";
            }
            else if (marks >= 60)
            {
                grade  = "B";
                remark = "Good";
            }
            else if (marks >= 45)
            {
                grade  = "C";
                remark = "Average";
            }
            else if (marks >= 33)
            {
                grade  = "D";
                remark = "Pass";
            }
            else
            {
                grade  = "F";
                remark = "Fail — Better luck next time";
            }

            Console.WriteLine($"Grade: {grade} — {remark}");
        }
    }
}
```

### Part B: `switch` — Day of the Week

```csharp
using System;

namespace SwitchDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter day number (1-7): ");
            int day = int.Parse(Console.ReadLine());

            switch (day)
            {
                case 1:
                    Console.WriteLine("Monday — Start of the work week.");
                    break;
                case 2:
                    Console.WriteLine("Tuesday");
                    break;
                case 3:
                    Console.WriteLine("Wednesday — Mid-week!");
                    break;
                case 4:
                    Console.WriteLine("Thursday");
                    break;
                case 5:
                    Console.WriteLine("Friday — Almost the weekend!");
                    break;
                case 6:
                case 7:
                    // Two cases sharing the same block (fall-through grouping)
                    Console.WriteLine("Weekend — Rest and recharge!");
                    break;
                default:
                    Console.WriteLine("Invalid day. Enter 1 to 7.");
                    break;
            }
        }
    }
}
```

### Part C: `for` Loop — Multiplication Table

```csharp
using System;

namespace ForLoopDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a number to print its table: ");
            int n = int.Parse(Console.ReadLine());

            Console.WriteLine($"\nMultiplication table of {n}:");
            Console.WriteLine("--------------------------------");

            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine($"{n} x {i,2} = {n * i,3}");
            }
        }
    }
}
```

### Part D: `while` Loop — Guess the Number Game

```csharp
using System;

namespace WhileLoopDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rng     = new Random();
            int  secret    = rng.Next(1, 101);   // random number 1-100
            int  attempts  = 0;
            bool guessed   = false;

            Console.WriteLine("Guess the number between 1 and 100!");

            while (!guessed)
            {
                Console.Write("Your guess: ");
                int guess = int.Parse(Console.ReadLine());
                attempts++;

                if (guess < secret)
                    Console.WriteLine("Too low! Try higher.");
                else if (guess > secret)
                    Console.WriteLine("Too high! Try lower.");
                else
                {
                    guessed = true;
                    Console.WriteLine($"Correct! You guessed it in {attempts} attempt(s).");
                }
            }
        }
    }
}
```

### Part E: `do-while` Loop — Menu System

```csharp
using System;

namespace DoWhileDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice;

            do
            {
                Console.WriteLine("\n===== MENU =====");
                Console.WriteLine("1. Say Hello");
                Console.WriteLine("2. Show Date");
                Console.WriteLine("3. Exit");
                Console.Write("Enter choice: ");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1: Console.WriteLine("Hello, World!"); break;
                    case 2: Console.WriteLine("Today: " + DateTime.Now.ToLongDateString()); break;
                    case 3: Console.WriteLine("Goodbye!"); break;
                    default: Console.WriteLine("Invalid option."); break;
                }

            } while (choice != 3);   // keep looping until user chooses Exit
        }
    }
}
```

### Part F: `break` and `continue`

```csharp
using System;

namespace BreakContinueDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Odd numbers from 1 to 20 (skipping 13):");

            for (int i = 1; i <= 20; i++)
            {
                if (i % 2 == 0) continue;   // skip even numbers
                if (i == 13)    continue;   // also skip 13 (unlucky!)
                Console.Write(i + " ");
            }

            Console.WriteLine("\n\nSearching for first number divisible by 7 (from 1 to 100):");
            for (int i = 1; i <= 100; i++)
            {
                if (i % 7 == 0)
                {
                    Console.WriteLine("Found: " + i);
                    break;    // stop the loop as soon as we find it
                }
            }
        }
    }
}
```

---

## 4. Expected Output

**Output - Part A (marks = 82):**
```text
Enter your marks (0-100): 82
Grade: A — Excellent
```

**Output - Part B (day = 6):**
```text
Enter day number (1-7): 6
Weekend — Rest and recharge!
```

**Output - Part C (n = 5):**
```text
Enter a number to print its table: 5

Multiplication table of 5:
--------------------------------
5 x  1 =   5
5 x  2 =  10
5 x  3 =  15
5 x  4 =  20
5 x  5 =  25
5 x  6 =  30
5 x  7 =  35
5 x  8 =  40
5 x  9 =  45
5 x 10 =  50
```

**Output - Part F:**
```text
Odd numbers from 1 to 20 (skipping 13):
1 3 5 7 9 11 15 17 19

Searching for first number divisible by 7 (from 1 to 100):
Found: 7
```

---

## 5. Viva / Discussion Questions

1. What is the difference between `while` and `do-while`?
2. What happens if you forget a `break` statement in a `switch` block?
3. What is the difference between `break` and `continue`?
4. Can a `for` loop run zero times? Give an example.
5. What is an infinite loop? How can you intentionally create one in C#?
6. What is the ternary operator and how does it relate to `if/else`?
7. What is the difference between `==` (equality) and `=` (assignment) in a condition?
