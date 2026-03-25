# Experiment 06 — Reflection: Inspect Assemblies, Types, and Methods

**Subject:** CSIT-406 .NET Framework Lab
**RGPV, Bhopal**

---

## Aim

Use reflection to inspect assemblies, types, methods, and invoke them dynamically.

---

## Theory

**Reflection** is the ability of a program to inspect and interact with its own metadata at runtime. It is available via the `System.Reflection` namespace.

| Class | Purpose |
|---|---|
| `Assembly` | Load and inspect a .NET assembly |
| `Type` | Get type info (class, methods, properties, fields) |
| `MethodInfo` | Inspect and invoke a method |
| `PropertyInfo` | Read/write properties dynamically |
| `ConstructorInfo` | Create object instances dynamically |

> Real-world analogy: An IDE like Visual Studio uses reflection to show IntelliSense — it inspects your classes at runtime to suggest methods and properties.

---

## Code

### Part A — Inspect the Current Assembly

```csharp
using System;
using System.Reflection;

namespace ReflectionDemo
{
    public class Calculator
    {
        public int Add(int a, int b) => a + b;
        public int Subtract(int a, int b) => a - b;
        private double Divide(double a, double b) => a / b;
        public string Name { get; set; } = "Basic Calculator";
    }

    class Program
    {
        static void Main()
        {
            // Get type info
            Type type = typeof(Calculator);

            Console.WriteLine("=== Type Info ===");
            Console.WriteLine("Name: " + type.Name);
            Console.WriteLine("Namespace: " + type.Namespace);
            Console.WriteLine("Full Name: " + type.FullName);

            // List all public methods
            Console.WriteLine("\n=== Public Methods ===");
            foreach (MethodInfo method in type.GetMethods())
                Console.WriteLine($" - {method.Name}({string.Join(", ", Array.ConvertAll(method.GetParameters(), p => p.ParameterType.Name + " " + p.Name))}) : {method.ReturnType.Name}");

            // List all properties
            Console.WriteLine("\n=== Properties ===");
            foreach (PropertyInfo prop in type.GetProperties())
                Console.WriteLine($" - {prop.Name} : {prop.PropertyType.Name}");
        }
    }
}
```

### Part B — Dynamic Method Invocation

```csharp
using System;
using System.Reflection;

namespace ReflectionInvoke
{
    public class Calculator
    {
        public int Add(int a, int b) => a + b;
        public int Multiply(int a, int b) => a * b;
    }

    class Program
    {
        static void Main()
        {
            Type type = typeof(Calculator);

            // Create instance dynamically (no 'new' keyword)
            object instance = Activator.CreateInstance(type);

            // Invoke Add method dynamically
            MethodInfo addMethod = type.GetMethod("Add");
            object result = addMethod.Invoke(instance, new object[] { 10, 20 });
            Console.WriteLine("Add(10, 20) = " + result);

            // Invoke Multiply method dynamically
            MethodInfo multiplyMethod = type.GetMethod("Multiply");
            result = multiplyMethod.Invoke(instance, new object[] { 4, 5 });
            Console.WriteLine("Multiply(4, 5) = " + result);
        }
    }
}
```

### Part C — Load External Assembly & Inspect

```csharp
using System;
using System.Reflection;

namespace AssemblyInspect
{
    class Program
    {
        static void Main()
        {
            // Load the currently executing assembly
            Assembly asm = Assembly.GetExecutingAssembly();

            Console.WriteLine("Assembly: " + asm.FullName);
            Console.WriteLine("\nTypes defined:");

            foreach (Type t in asm.GetTypes())
            {
                Console.WriteLine("\n [Type] " + t.Name);
                foreach (MethodInfo m in t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                    Console.WriteLine("   Method: " + m.Name);
            }
        }
    }
}
```

---

## Expected Output

**Part A:**
```
=== Type Info ===
Name: Calculator
Namespace: ReflectionDemo
Full Name: ReflectionDemo.Calculator

=== Public Methods ===
 - Add(Int32 a, Int32 b) : Int32
 - Subtract(Int32 a, Int32 b) : Int32
 - get_Name() : String
 ...

=== Properties ===
 - Name : String
```

**Part B:**
```
Add(10, 20) = 30
Multiply(4, 5) = 20
```

---

## Viva Questions

1. What is Reflection in .NET? Name its main namespace.
2. How do you create an instance of a class using Reflection?
3. What is `BindingFlags` used for in Reflection?
4. What is the difference between `GetMethod()` and `GetMethods()`?
5. Name two practical uses of Reflection in real applications.

---

[Back to Index](../README.md)
