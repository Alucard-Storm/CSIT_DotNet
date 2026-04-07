# Extra Experiment 02 — Variables and Data Types | Notes

---

## Value Types vs Reference Types

C# splits all types into two fundamental categories:

```
Value Types                     Reference Types
────────────────────────────    ────────────────────────────
int, double, float, decimal     string, object, arrays
char, bool, byte, struct        class instances

Stored directly in memory.      Stored as a pointer to heap memory.
Copying creates a NEW value.    Copying shares the SAME data.
```

---

## The Most Used Types — Quick Reference

```csharp
int    count   = 42;          // whole numbers (most common)
double ratio   = 3.14;        // decimals (default for decimal literals)
decimal money  = 9.99m;       // money — always use decimal for currency
bool   active  = true;        // true / false only
char   letter  = 'C';         // single character — use single quotes
string text    = "Hello";     // text — use double quotes
```

---

## Type Conversion Cheat Sheet

```
Widening (safe, automatic)        Narrowing (manual, may lose data)
──────────────────────────        ─────────────────────────────────
int  → double   (auto)            double → int    → cast: (int)value
int  → long     (auto)            long   → int    → cast: (int)value
float → double  (auto)            double → float  → cast: (float)value
```

```csharp
// Implicit (widening) — compiler handles it
int i = 10;
double d = i;     // safe: 10 → 10.0

// Explicit (narrowing) — you must write the cast
double pi = 3.99;
int n = (int)pi;  // truncates: 3.99 → 3  (NOT rounded!)

// String ↔ Number
int age     = int.Parse("25");          // throws exception if invalid
int age2    = Convert.ToInt32("25");    // returns 0 if null (safer)
bool ok     = int.TryParse("25", out int result); // safest for user input
string text = (42).ToString();
```

---

## When to Use Which Numeric Type

| Situation | Use |
|---|---|
| Counting things, indexes, IDs | `int` |
| Very large whole numbers | `long` |
| Scientific calculations | `double` |
| Money, tax, banking | `decimal` |
| Memory-tight arrays of small numbers | `byte` or `short` |

---

## `var` — Let the Compiler Figure It Out

```csharp
var name  = "Akshay"; // compiler sees string literal → string
var score = 95;        // compiler sees integer literal → int
var ratio = 3.14;      // compiler sees decimal literal → double
var flag  = true;      // compiler sees bool literal → bool
```

**Rules for `var`:**
- Must be initialised on the same line — `var x;` is invalid.
- Type is **fixed after assignment** — you cannot later assign a different type.
- Identical to explicitly typed variables at runtime (no performance difference).

---

## `const` vs `readonly`

```csharp
const double PI = 3.14159;         // compile-time constant, can never change
readonly int maxRetries;            // set once at runtime (in constructor only)
```

---

## Common Mistakes

```csharp
// 1. Forgetting the suffix for float literals
float f = 3.14;    // ERROR — 3.14 is a double by default
float f = 3.14f;   // CORRECT

// 2. Forgetting the suffix for decimal literals
decimal d = 9.99;    // ERROR
decimal d = 9.99m;   // CORRECT

// 3. int division discards the remainder
int result = 7 / 2;         // result = 3, NOT 3.5
double r   = 7.0 / 2;      // r = 3.5  — one operand must be double
```
