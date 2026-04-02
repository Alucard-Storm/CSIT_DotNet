# Experiment 06 — Reflection: Inspecting Assemblies, Types, and Dynamic Invocation

**Subject:** CSIT-406 .NET Framework Lab
**Location:** RGPV, Bhopal

---

## 1. Aim

To learn how to use the .NET Reflection subsystem to inspect your C# code while it is running, and to execute methods dynamically without requiring direct programming.

## 2. Theory

**Reflection** is a very powerful feature in the .NET Framework that allows a running program to look at its own structure. Instead of just running the code, Reflection allows your C# program to inspect what classes, methods, and properties exist inside your application while it is active.

### Core Reflection Subsystem Classes

| Reflection Class | Explanation |
|---|---|
| **`Assembly`** | Represents an entire compiled project (like a .DLL or .EXE file). |
| **`Type`** | Represents a specific class or data type. You can use this to find out everything about a class. |
| **`MethodInfo`** | Represents a single method on a class. You can use this to run the method dynamically using `Invoke()`. |
| **`PropertyInfo`** | Represents a single property on a class. You can use this to read or write the property's value. |
| **`Activator`** | A static helper class used to create a new object instance dynamically (instead of using the standard `new` keyword). |

*Instructional Example:* Have you ever typed a dot (`.`) in Visual Studio and seen a pop-up list of available methods? Visual Studio uses Reflection in the background. It dynamically inspects your class files to see exactly what methods they contain, which allows it to suggest them to you.

---

## 3. Implementation Code

### Part A: Inspecting a Class to See its Structure

This code demonstrates how to take a simple completely standard class and use Reflection to view all of its information.

```csharp
// File: TypeInspector.cs
using System;
using System.Reflection;

namespace ReflectionDemo
{
    // A simple math class we want to inspect
    public class Calculator
    {
        public int Add(int a, int b) => a + b;
        public int Subtract(int a, int b) => a - b;
        private double InternalDivide(double a, double b) => a / b;
        public string Version { get; set; } = "1.0.4";
    }

    class Program
    {
        static void Main(string[] args)
        {
            // The typeof keyword gets the reflection data for the class
            Type inspectionTarget = typeof(Calculator);

            Console.WriteLine("=== Class Details ===");
            Console.WriteLine("Class Name: " + inspectionTarget.Name);
            Console.WriteLine("Namespace: " + inspectionTarget.Namespace);

            // Print out every single public method the class has
            Console.WriteLine("\n=== Available Methods ===");
            foreach (MethodInfo method in inspectionTarget.GetMethods())
            {
                Console.WriteLine(" -> Found Method: " + method.Name);
            }

            // Print out every property the class has
            Console.WriteLine("\n=== Available Properties ===");
            foreach (PropertyInfo prop in inspectionTarget.GetProperties())
            {
                Console.WriteLine(" -> Found Property: " + prop.Name);
            }
        }
    }
}
```

### Part B: Executing a Method Dynamically

This code proves that you can run a method inside a class without ever formally writing the method's name in your direct code logic.

```csharp
// File: DynamicInvokeDemo.cs
using System;
using System.Reflection;

namespace ReflectionInvoke
{
    public class SecretCalculator
    {
        public int Multiply(int x, int y) => x * y;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Type targetType = typeof(SecretCalculator);

            Console.WriteLine("Creating the object dynamically...");
            
            // Create a brand new instance without using the 'new' keyword
            object dynamicObject = Activator.CreateInstance(targetType);

            // Find the multiplying method by searching its text name
            MethodInfo mathMethod = targetType.GetMethod("Multiply");
            
            // Invoke (run) the method by passing in the number 5 and 6
            object answer = mathMethod.Invoke(dynamicObject, new object[] { 5, 6 });
            
            Console.WriteLine("Dynamic Execution says: 5 x 6 = " + answer);
        }
    }
}
```

---

## 4. Expected Output

**Output - Part A:**
```text
=== Class Details ===
Class Name: Calculator
Namespace: ReflectionDemo

=== Available Methods ===
 -> Found Method: Add
 -> Found Method: Subtract
 -> Found Method: get_Version
 -> Found Method: set_Version
 -> Found Method: GetType
 -> Found Method: ToString
 -> Found Method: Equals
 -> Found Method: GetHashCode

=== Available Properties ===
 -> Found Property: Version
```

**Output - Part B:**
```text
Creating the object dynamically...
Dynamic Execution says: 5 x 6 = 30
```

---

## 5. Viva / Discussion Questions

1. **Definitions:** What does the term Reflection mean in the C# language? 
2. **Namespaces:** What specific .NET namespace contains the Reflection classes?
3. **Information Discovery:** What is the technical difference between using the `typeof` keyword and looking at a `MethodInfo` object?
4. **Execution:** Explain the difference between using `new SecretCalculator()` and using `Activator.CreateInstance()`.
5. **Practical Use Cases:** Where have you seen Reflection being used in real programming applications (for example, in Visual Studio IDE menus)?

---

[Back to Main Index](../README.md)
