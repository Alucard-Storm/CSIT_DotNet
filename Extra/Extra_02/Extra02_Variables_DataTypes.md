# Extra Experiment 02 — Variables and Data Types

**Subject:** CSIT-406 .NET Framework Lab
**Location:** RGPV, Bhopal

---

## 1. Aim

To explore the fundamental data types available in C#, declare and initialise variables, perform type conversions, and understand constants and the `var` keyword.

## 2. Theory

A **variable** is a named memory location that stores a value. Every variable in C# has a **type** that determines the kind of data it can hold and how much memory it occupies.

C# is a **statically typed** language — the type of a variable must be known at compile time.

| Category | Type | Size | Range / Description |
|---|---|---|---|
| Integer | `byte` | 1 byte | 0 to 255 |
| Integer | `short` | 2 bytes | −32,768 to 32,767 |
| Integer | `int` | 4 bytes | −2.1 billion to +2.1 billion |
| Integer | `long` | 8 bytes | Very large whole numbers |
| Floating-point | `float` | 4 bytes | 7-digit precision (suffix `f`) |
| Floating-point | `double` | 8 bytes | 15-digit precision (default decimal) |
| Floating-point | `decimal` | 16 bytes | 28-digit precision, for money (suffix `m`) |
| Text | `char` | 2 bytes | A single Unicode character (`'A'`) |
| Text | `string` | Variable | Sequence of characters (`"Hello"`) |
| Boolean | `bool` | 1 byte | `true` or `false` only |
| Object | `object` | Variable | Base type of all types in .NET |

**Type Conversion** is the process of changing a value from one type to another:
- **Implicit conversion** — done automatically when no data is lost (e.g., `int` → `double`).
- **Explicit conversion (casting)** — done manually when data could be lost (e.g., `double` → `int`).
- **Parsing** — converting text (`string`) to a numeric type using methods like `int.Parse()` or `Convert.ToInt32()`.

---

## 3. Implementation Code

### Part A: Declaring and Printing Variables

```csharp
using System;

namespace VariablesDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Integer types
            byte  age       = 20;
            int   year      = 2026;
            long  population = 1_400_000_000L;   // underscore separator for readability

            // Floating-point types
            float  pi      = 3.14159f;
            double gravity = 9.80665;
            decimal price  = 1299.99m;

            // Text types
            char   grade  = 'A';
            string name   = "Akshay Sagar";

            // Boolean type
            bool isPassed = true;

            Console.WriteLine("--- Student Record ---");
            Console.WriteLine("Name    : " + name);
            Console.WriteLine("Age     : " + age);
            Console.WriteLine("Grade   : " + grade);
            Console.WriteLine("Year    : " + year);
            Console.WriteLine("Passed? : " + isPassed);
            Console.WriteLine("Pi      : " + pi);
            Console.WriteLine("Gravity : " + gravity);
            Console.WriteLine("Price   : " + price);
            Console.WriteLine("Population: " + population);
        }
    }
}
```

### Part B: Type Conversion

```csharp
using System;

namespace TypeConversionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // --- Implicit conversion (safe, automatic) ---
            int    intValue    = 100;
            double doubleValue = intValue;   // int fits in double, no data loss
            Console.WriteLine("Implicit int -> double: " + doubleValue);

            // --- Explicit cast (may lose data) ---
            double pi     = 3.99;
            int    piInt  = (int)pi;          // decimal part is truncated, NOT rounded
            Console.WriteLine("Explicit double -> int: " + piInt);   // prints 3

            // --- Parse: string -> number ---
            string ageText = "25";
            int    age     = int.Parse(ageText);
            Console.WriteLine("Parsed age: " + age + 1);    // prints 26

            // --- Convert class (handles null safely) ---
            string priceText = "499.95";
            double productPrice = Convert.ToDouble(priceText);
            Console.WriteLine("Converted price: " + productPrice);

            // --- ToString: number -> string ---
            int score = 87;
            string scoreText = score.ToString();
            Console.WriteLine("Score as text: " + scoreText);
        }
    }
}
```

### Part C: Constants and `var`

```csharp
using System;

namespace ConstantsAndVar
{
    class Program
    {
        // const: value is fixed at compile time and can never change
        const double GravitationalConstant = 9.80665;
        const string UniversityName        = "RGPV, Bhopal";

        static void Main(string[] args)
        {
            // var: compiler infers the type from the assigned value
            var studentName = "Diksha";         // inferred as string
            var rollNumber  = 101;              // inferred as int
            var percentage  = 91.5;             // inferred as double
            var isEligible  = true;             // inferred as bool

            Console.WriteLine("University: "   + UniversityName);
            Console.WriteLine("Gravity: "      + GravitationalConstant);
            Console.WriteLine($"Student: {studentName}, Roll: {rollNumber}");
            Console.WriteLine($"Percentage: {percentage}%, Eligible: {isEligible}");

            // Demonstrating GetType() to see the inferred types
            Console.WriteLine("\n--- Inferred Types ---");
            Console.WriteLine($"studentName is: {studentName.GetType()}");
            Console.WriteLine($"rollNumber  is: {rollNumber.GetType()}");
            Console.WriteLine($"percentage  is: {percentage.GetType()}");
        }
    }
}
```

---

## 4. Expected Output

**Output - Part A:**
```text
--- Student Record ---
Name    : Akshay Sagar
Age     : 20
Grade   : A
Year    : 2026
Passed? : True
Pi      : 3.14159
Gravity : 9.80665
Price   : 1299.99
Population: 1400000000
```

**Output - Part B:**
```text
Implicit int -> double: 100
Explicit double -> int: 3
Parsed age: 26
Converted price: 499.95
Score as text: 87
```

**Output - Part C:**
```text
University: RGPV, Bhopal
Gravity: 9.80665
Student: Diksha, Roll: 101
Percentage: 91.5%, Eligible: True

--- Inferred Types ---
studentName is: System.String
rollNumber  is: System.Int32
percentage  is: System.Double
```

---

## 5. Viva / Discussion Questions

1. What is the difference between `float`, `double`, and `decimal`? When would you use `decimal`?
2. What happens when you cast `double pi = 3.99` to `int`? Is it rounded or truncated?
3. What is the difference between `int.Parse()` and `Convert.ToInt32()`?
4. Can a `var` variable change its type after declaration? Why or why not?
5. What is the difference between `const` and `readonly` in C#?
6. Why is `string` a reference type even though it behaves like a value type?
7. What is the default value of an unassigned `int`, `bool`, and `string`?
