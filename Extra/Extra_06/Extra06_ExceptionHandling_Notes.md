# Extra Experiment 06 — Exception Handling | Notes

---

## The try / catch / finally Pattern

```csharp
try
{
    // code that MIGHT throw
    int result = 10 / number;
}
catch (DivideByZeroException ex)
{
    // handle ONE specific exception type
    Console.WriteLine("Divide by zero: " + ex.Message);
}
catch (Exception ex)
{
    // catch-all safety net — put LAST
    Console.WriteLine("Unexpected: " + ex.Message);
}
finally
{
    // ALWAYS runs — use for cleanup (close files, connections)
    Console.WriteLine("Done.");
}
```

**Order matters:** put the most specific `catch` first, the most general (`Exception`) last.

---

## Exception Object Properties

```csharp
catch (Exception ex)
{
    ex.Message        // human-readable description
    ex.StackTrace     // call stack showing where it happened
    ex.InnerException // the original exception (if this one wraps another)
    ex.GetType().Name // name of the exception class
}
```

---

## `throw` vs `throw ex`

```csharp
// throw        — re-throws and PRESERVES the original stack trace (preferred)
catch (Exception ex)
{
    Log(ex);
    throw;         // keeps original stack trace
}

// throw ex     — re-throws but RESETS the stack trace (lose original location)
catch (Exception ex)
{
    throw ex;      // stack trace now points to this line — avoid this
}
```

---

## Safe Input Parsing

```csharp
// int.Parse — throws FormatException if invalid
int age = int.Parse("abc");     // CRASH

// int.TryParse — returns false instead of crashing
if (int.TryParse(input, out int age))
    Console.WriteLine("Age: " + age);
else
    Console.WriteLine("Invalid input.");

// Convert.ToInt32 — returns 0 for null, throws for non-numeric
int x = Convert.ToInt32(null);   // returns 0 (safe for null)
int y = Convert.ToInt32("abc");  // throws FormatException
```

---

## Custom Exception — Template

```csharp
// Step 1: inherit from Exception (or a more specific base)
class InsufficientFundsException : Exception
{
    // Step 2: add custom properties
    public double Shortfall { get; }

    // Step 3: call base constructor with the message
    public InsufficientFundsException(double shortfall)
        : base($"Insufficient funds. Shortfall: ₹{shortfall:F2}")
    {
        Shortfall = shortfall;
    }
}

// Step 4: throw it
throw new InsufficientFundsException(150.00);

// Step 5: catch it
catch (InsufficientFundsException ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine("Need ₹" + ex.Shortfall + " more.");
}
```

---

## Common Built-In Exceptions — Quick Recall

```
NullReferenceException       → used object that is null
IndexOutOfRangeException     → array index beyond bounds
FormatException              → "abc" parsed as int
DivideByZeroException        → integer ÷ 0
FileNotFoundException        → file does not exist on disk
InvalidOperationException    → method called at wrong state
ArgumentNullException        → null passed where not allowed
ArgumentOutOfRangeException  → number outside allowed range
```

---

## `finally` — What It's For

```csharp
StreamReader reader = null;
try
{
    reader = new StreamReader("file.txt");
    string content = reader.ReadToEnd();
}
catch (FileNotFoundException)
{
    Console.WriteLine("File not found.");
}
finally
{
    // This runs even if an exception occurred or even if we used return
    reader?.Close();    // ?. is the null-safe operator — only calls Close if reader != null
}
```

**Rule of thumb:** if you open something (file, database connection, network socket), close it in `finally`. Modern C# also uses `using` blocks for this automatically.

---

## Common Mistakes

```csharp
// 1. Catching Exception and swallowing it silently
catch (Exception) { }     // BAD — hides bugs; always at least log the error

// 2. Catching too broadly
catch (Exception ex)      // catches EVERYTHING — use specific types first
{ ... }

// 3. Placing more general catch before specific ones
catch (Exception ex) { }         // WRONG ORDER — this catches everything
catch (FormatException ex) { }   // this line is UNREACHABLE
```
