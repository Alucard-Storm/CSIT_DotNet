# Experiment 08 — String vs StringBuilder | Notes

---

## The Core Concept

`String` is **immutable** — it cannot be changed. Every time you "change" a string, C# throws the old one away and creates a brand new string in memory.

`StringBuilder` is **mutable** — it can be changed in place. It uses a single block of memory and simply updates it.

---

## Visual Analogy

```
Using String (Slow, wasteful):
"Hello"  →  THROW AWAY  →  Create new "Hello World"
"Hello World"  →  THROW AWAY  →  Create new "Hello World!"
(Every change creates garbage that the computer must clean up)

Using StringBuilder (Fast, efficient):
["Hello              "]   (one reusable box in memory)
["Hello World        "]   (box updated, no throwing away)
["Hello World!       "]   (box updated again)
```

---

## Performance Proof (10,000 loops)

```csharp
// --- Slow way using String ---
string normalText = "";
for (int i = 0; i < 10000; i++)
    normalText += i.ToString();     // Creates 10,000 throwaway strings!
// Time: ~85 milliseconds

// --- Fast way using StringBuilder ---
StringBuilder sb = new StringBuilder();
for (int i = 0; i < 10000; i++)
    sb.Append(i.ToString());        // Updates ONE string in memory
// Time: ~1 millisecond
```

**Output comparison:**
```
Normal String Time:  85 milliseconds.
StringBuilder Time:  1 milliseconds.
Final lengths match: True
```

Both produce the exact same result, but `StringBuilder` is approximately 80 times faster.

---

## Useful StringBuilder Methods

```csharp
StringBuilder sb = new StringBuilder("Hello");
sb.Append(" World");         // Adds text to the end
sb.Insert(0, ">> ");         // Inserts text at position 0
sb.Replace("World", "RGPV"); // Replace a word
sb.AppendLine(" Done.");     // Adds text + new line

Console.WriteLine(sb.ToString());   // Always convert with ToString() at the end
// Output: >> Hello RGPV Done.
```

---

## When to Use Which?

| Situation | Use |
|---|---|
| Fixing one or two words | `String` |
| Joining 5+ strings in a loop | `StringBuilder` |
| Building a large report or log file | `StringBuilder` |
| Simple variable storage | `String` |
