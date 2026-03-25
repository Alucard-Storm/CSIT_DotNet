# Experiment 01 — Delegates and Callbacks | Notes

---

## What is a Delegate?

Think of a delegate like a **contact in your phonebook**. Instead of calling someone yourself, you give your phone to a friend and say "call this person for me." The delegate holds the reference to a method so someone else can call it.

```
Normal call:  Add(5, 3)           → you call directly
Delegate:     operation(5, 3)     → delegate calls Add for you
```

---

## Types of Delegates

### 1. Single-Cast — points to ONE method

```csharp
delegate int MathOperation(int a, int b);    // Declaration

MathOperation op = Add;                       // Point to Add
Console.WriteLine(op(5, 3));                 // Output: 8

op = Multiply;                               // Now point to Multiply
Console.WriteLine(op(5, 3));                 // Output: 15
```

### 2. Multicast — points to MULTIPLE methods at once

Use `+=` to attach more methods. When you call it, all attached methods run in order.

```csharp
NotificationHandler notifier = LogToConsole;
notifier += LogToFile;      // Now BOTH methods are attached

notifier("Order placed!");

// Output:
// [Console] Order placed!
// [File] Writing: Order placed!
```

### 3. Async Callback — runs a method on a BACKGROUND THREAD

Useful when a task takes a long time (downloading, processing) and you don't want the screen to freeze.

```csharp
work.BeginInvoke(callbackMethod, null);   // Starts in background
Console.WriteLine("Main keeps running!"); // Main thread doesn't wait

// Output (order may vary):
// Main keeps running!
// Working on background thread...
// Work done!
// Callback: Async work completed!
```

---

## Key Points to Remember

- A delegate must match the exact **return type and parameters** of the method it holds.
- Multicast delegates call all methods in the order they were added with `+=`.
- `BeginInvoke()` runs the method on a new thread (background); `Invoke()` runs it on the same thread.
- `Action<T>` is a delegate that returns nothing (`void`). `Func<T, TResult>` is a delegate that returns a value.

---

## Quick Reference Table

| Topic | Key Word |
|---|---|
| One method | Single-cast |
| Multiple methods | Multicast + `+=` |
| Background execution | `BeginInvoke()` |
| No return value | `Action<T>` |
| With return value | `Func<T, TResult>` |
