# Experiment 06 — Reflection | Notes

---

## What is Reflection?

Reflection is a feature that lets a running program **look at its own code**. Normally, code just runs. With Reflection, code can also ask questions like:
- What methods does this class have?
- What are the parameter types?
- Can I create an object and run a method by just typing its name as text?

**Think of it this way:** Reflection is like picking up a mirror and looking at yourself. The program examines its own structure while it runs.

---

## Getting Info About a Class

Use `typeof()` to get the reflection data (called `Type`) for any class:

```csharp
Type info = typeof(Calculator);   // Get the mirror for Calculator

Console.WriteLine(info.Name);        // Output: Calculator
Console.WriteLine(info.Namespace);   // Output: ReflectionDemo
```

---

## Listing All Methods of a Class

```csharp
foreach (MethodInfo method in info.GetMethods())
{
    Console.WriteLine("Found method: " + method.Name);
}
```

**Output (for a Calculator class with Add, Subtract):**
```
Found method: Add
Found method: Subtract
Found method: GetType
Found method: ToString
Found method: Equals
Found method: GetHashCode
```

*(The extra methods like `GetType`, `ToString` are inherited from the base `Object` class in C#)*

---

## Creating an Object and Running a Method Dynamically

This is the most powerful part of Reflection. You can create an object and call its method **without using the `new` keyword directly** and **without writing the method name directly**.

```csharp
Type type = typeof(Calculator);

// Create the object dynamically
object calcObj = Activator.CreateInstance(type);

// Find the method by its name as a string
MethodInfo addMethod = type.GetMethod("Add");

// Run it with parameters (10, 20)
object result = addMethod.Invoke(calcObj, new object[] { 10, 20 });

Console.WriteLine("Result: " + result);
// Output: Result: 30
```

---

## Key Points to Remember

| Term | Meaning |
|---|---|
| `typeof(MyClass)` | Gets the reflection `Type` object for a class |
| `type.GetMethods()` | Returns an array of all public methods |
| `type.GetProperties()` | Returns an array of all properties |
| `Activator.CreateInstance(type)` | Creates a new object without using `new` |
| `methodInfo.Invoke(obj, args)` | Runs the method dynamically |

**Where is Reflection used in real projects?**
- Visual Studio IntelliSense (the pop-up list of methods)
- Object-Relational Mappers like Entity Framework
- Testing frameworks like NUnit
