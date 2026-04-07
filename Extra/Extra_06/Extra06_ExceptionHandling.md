# Extra Experiment 06 — Exception Handling

**Subject:** CSIT-406 .NET Framework Lab
**Location:** RGPV, Bhopal

---

## 1. Aim

To implement robust error handling in C# using `try/catch/finally` blocks, to distinguish between different exception types, and to create custom exception classes for domain-specific error reporting.

## 2. Theory

An **exception** is an unexpected event that disrupts the normal flow of a program (e.g., dividing by zero, accessing a missing file, parsing invalid input). Without handling, exceptions cause a program to crash and display an unformatted error message.

C# uses a structured exception handling (SEH) model:

| Block | Purpose |
|---|---|
| `try` | Wraps the code that might throw an exception |
| `catch (ExceptionType e)` | Handles a specific type of exception; multiple `catch` blocks can be chained |
| `finally` | **Always executes**, whether an exception occurred or not — ideal for releasing resources (closing files, connections) |
| `throw` | Manually raises an exception |

Common built-in exception types:

| Exception Class | Triggered When |
|---|---|
| `DivideByZeroException` | Integer division by zero |
| `FormatException` | Parsing text that is not a valid number |
| `IndexOutOfRangeException` | Array access outside its bounds |
| `NullReferenceException` | Using an object that is `null` |
| `FileNotFoundException` | Requesting a file that does not exist |
| `ArgumentException` | A method argument is invalid |
| `OverflowException` | Arithmetic result exceeds the type's range |

**Exception hierarchy:** All exception classes inherit from `System.Exception`. Catching `Exception` catches everything, but specific catches are preferred.

---

## 3. Implementation Code

### Part A: Basic `try / catch / finally`

```csharp
using System;

namespace ExceptionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a number: ");
            string input = Console.ReadLine();

            try
            {
                int number  = int.Parse(input);    // may throw FormatException
                int result  = 100 / number;         // may throw DivideByZeroException

                Console.WriteLine($"100 / {number} = {result}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Please enter a valid integer.");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Error: Cannot divide by zero.");
            }
            catch (Exception ex)
            {
                // Catch-all for any other unexpected exception
                Console.WriteLine("Unexpected error: " + ex.Message);
            }
            finally
            {
                // Runs regardless of success or failure
                Console.WriteLine("Program finished.");
            }
        }
    }
}
```

### Part B: Multiple Exception Scenarios — Safe Calculator

```csharp
using System;

namespace SafeCalculator
{
    class Program
    {
        static double Divide(double a, double b)
        {
            if (b == 0)
                throw new ArgumentException("Divisor cannot be zero.", nameof(b));
            return a / b;
        }

        static int ParseAndValidate(string text)
        {
            if (!int.TryParse(text, out int value))
                throw new FormatException($"'{text}' is not a valid integer.");
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(text), "Value must be non-negative.");
            return value;
        }

        static void Main(string[] args)
        {
            string[] testInputs = { "25", "-5", "abc", "0" };

            foreach (string input in testInputs)
            {
                try
                {
                    int value = ParseAndValidate(input);
                    double result = Divide(100, value);
                    Console.WriteLine($"100 / {value} = {result}");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"[Format Error] {ex.Message}");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"[Range Error] {ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Argument Error] {ex.Message}");
                }
            }
        }
    }
}
```

### Part C: Custom Exception Class — `InsufficientFundsException`

```csharp
using System;

namespace CustomExceptionDemo
{
    // Custom exception — inherits from Exception
    class InsufficientFundsException : Exception
    {
        public double RequiredAmount { get; }
        public double AvailableBalance { get; }

        public InsufficientFundsException(double required, double available)
            : base($"Insufficient funds. Required: ₹{required:F2}, Available: ₹{available:F2}")
        {
            RequiredAmount    = required;
            AvailableBalance  = available;
        }
    }

    class BankAccount
    {
        private double _balance;
        public string Owner { get; }

        public BankAccount(string owner, double initialBalance)
        {
            Owner    = owner;
            _balance = initialBalance;
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be positive.");

            if (amount > _balance)
                throw new InsufficientFundsException(amount, _balance);

            _balance -= amount;
            Console.WriteLine($"Withdrew ₹{amount:F2}. Remaining balance: ₹{_balance:F2}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BankAccount account = new BankAccount("Akshay", 1000.00);

            double[] attempts = { 300.00, 800.00, -50.00 };

            foreach (double amount in attempts)
            {
                try
                {
                    account.Withdraw(amount);
                }
                catch (InsufficientFundsException ex)
                {
                    Console.WriteLine($"[Insufficient Funds] {ex.Message}");
                    Console.WriteLine($"  Shortfall: ₹{ex.RequiredAmount - ex.AvailableBalance:F2}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Invalid Input] {ex.Message}");
                }
            }
        }
    }
}
```

---

## 4. Expected Output

**Output - Part A (user types "0"):**
```text
Enter a number: 0
Error: Cannot divide by zero.
Program finished.
```

**Output - Part A (user types "abc"):**
```text
Enter a number: abc
Error: Please enter a valid integer.
Program finished.
```

**Output - Part B:**
```text
100 / 25 = 4
[Range Error] Value must be non-negative. (Parameter 'text')
[Format Error] 'abc' is not a valid integer.
[Argument Error] Divisor cannot be zero. (Parameter 'b')
```

**Output - Part C:**
```text
Withdrew ₹300.00. Remaining balance: ₹700.00
[Insufficient Funds] Insufficient funds. Required: ₹800.00, Available: ₹700.00
  Shortfall: ₹100.00
[Invalid Input] Withdrawal amount must be positive.
```

---

## 5. Viva / Discussion Questions

1. What is the difference between an error and an exception in C#?
2. Why should you catch specific exception types rather than just `Exception`?
3. What happens in the `finally` block if an exception is **not** caught by any `catch`?
4. What is the difference between `throw` and `throw ex` when re-throwing an exception?
5. When would you create a custom exception class? What does it inherit from?
6. What is the purpose of `int.TryParse()` compared to `int.Parse()`?
7. Can a `try` block exist without a `catch`? What would be the purpose?
8. What does the `nameof()` expression do in `throw new ArgumentException(..., nameof(b))`?
