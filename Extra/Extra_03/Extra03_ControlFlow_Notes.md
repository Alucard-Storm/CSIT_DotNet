# Extra Experiment 03 — Control Flow | Notes

---

## Decision Making — `if / else`

```csharp
if (condition)          // if condition is true → run this block
{
    // ...
}
else if (otherCondition)
{
    // ...
}
else                    // none of the above matched → fallback
{
    // ...
}
```

**Ternary shorthand** (for simple one-liner decisions):
```csharp
string result = (marks >= 33) ? "Pass" : "Fail";
//              condition         true    false
```

---

## `switch` vs `if/else`

Use `switch` when you are comparing one variable against **many exact values**:

```csharp
switch (day)
{
    case 1:  Console.WriteLine("Monday");   break;
    case 2:  Console.WriteLine("Tuesday");  break;
    default: Console.WriteLine("Other");    break;
}
```

**Rules:**
- Every `case` that has code needs a `break` (or `return`).
- `default` is optional but recommended as a safety net.
- Two cases can share a block (fall-through grouping): `case 6: case 7:`.

---

## The Three Loops

```
for (init; condition; update)    ← best when you KNOW how many times
while (condition)                 ← best when you DON'T know how many times
do { } while (condition)          ← body runs AT LEAST ONCE
```

### `for` loop anatomy

```csharp
for (int i = 0; i < 5; i++)
//   ─────────  ──────  ───
//   init       check   step
{
    Console.WriteLine(i);   // prints 0 1 2 3 4
}
```

### `while` vs `do-while`

```csharp
// while — might run 0 times
int x = 10;
while (x < 5)              // false immediately → body never runs
{ ... }

// do-while — always runs at least once
do
{
    Console.WriteLine("Runs once even if condition is false");
} while (x < 5);           // checks AFTER the first run
```

---

## Loop Control: `break` and `continue`

```csharp
for (int i = 1; i <= 10; i++)
{
    if (i == 5) continue;   // skip i=5, jump to next iteration
    if (i == 8) break;      // stop the loop entirely at i=8

    Console.Write(i + " ");
}
// Output: 1 2 3 4 6 7
```

---

## Nested Loops (Loop inside a Loop)

```csharp
// Print a 3x3 grid
for (int row = 1; row <= 3; row++)
{
    for (int col = 1; col <= 3; col++)
    {
        Console.Write($"({row},{col}) ");
    }
    Console.WriteLine();  // newline after each row
}
// Output:
// (1,1) (1,2) (1,3)
// (2,1) (2,2) (2,3)
// (3,1) (3,2) (3,3)
```

---

## Comparison and Logical Operators

| Operator | Meaning | Example |
|---|---|---|
| `==` | Equal to | `age == 18` |
| `!=` | Not equal | `grade != 'F'` |
| `>` / `<` | Greater / Less | `score > 90` |
| `>=` / `<=` | Greater-or-equal / Less-or-equal | `marks >= 33` |
| `&&` | AND (both must be true) | `age > 18 && hasFee` |
| `\|\|` | OR (at least one must be true) | `isAdmin \|\| isOwner` |
| `!` | NOT (inverts a bool) | `!isClosed` |

---

## Common Mistakes

```csharp
// 1. Assignment instead of comparison in condition
if (x = 5) { }        // WRONG — assigns 5 to x
if (x == 5) { }       // CORRECT

// 2. Off-by-one in for loop (printing 1-10 but loop ends at 9)
for (int i = 1; i < 10; i++)   // WRONG — runs for 1..9
for (int i = 1; i <= 10; i++)  // CORRECT

// 3. Infinite loop — condition never becomes false
while (true) { }               // intentional infinite loop — needs a break
while (x > 0) { x++; }        // accidental infinite loop — x keeps growing!
```
