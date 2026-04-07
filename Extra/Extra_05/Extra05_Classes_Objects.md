# Extra Experiment 05 — Classes and Objects

**Subject:** CSIT-406 .NET Framework Lab
**Location:** RGPV, Bhopal

---

## 1. Aim

To implement the fundamentals of Object-Oriented Programming (OOP) in C# by defining classes with fields, properties, constructors, and methods, and exploring the four pillars: **Encapsulation**, **Inheritance**, **Polymorphism**, and **Abstraction**.

## 2. Theory

**Object-Oriented Programming (OOP)** models software as a collection of interacting objects — each combining related data and behaviour.

| OOP Concept | Meaning | C# Mechanism |
|---|---|---|
| **Class** | A blueprint or template | `class ClassName { }` |
| **Object** | A concrete instance of a class | `ClassName obj = new ClassName()` |
| **Encapsulation** | Hiding internal data; exposing only what is needed | `private` fields + `public` properties |
| **Inheritance** | A child class reuses and extends a parent class | `: ParentClass` |
| **Polymorphism** | The same method behaves differently in different classes | `virtual` / `override` |
| **Abstraction** | Showing only the relevant interface; hiding implementation | `abstract` class / `interface` |

**Properties** in C# expose private fields safely through `get` and `set` accessors, allowing validation logic to be added without changing the public interface.

**Constructors** are special methods with the same name as the class and no return type. They run automatically when an object is created with `new`.

---

## 3. Implementation Code

### Part A: Class, Constructor, Properties, Methods — `BankAccount`

```csharp
using System;

namespace OOPDemo
{
    class BankAccount
    {
        // Private field — not directly accessible outside the class
        private double _balance;

        // Auto-implemented property (no private field needed for simple cases)
        public string AccountHolder { get; private set; }
        public string AccountNumber { get; private set; }

        // Constructor
        public BankAccount(string holder, string accountNumber, double initialDeposit)
        {
            AccountHolder = holder;
            AccountNumber = accountNumber;
            _balance      = initialDeposit;
        }

        // Property with read-only access (get only)
        public double Balance
        {
            get { return _balance; }
        }

        // Method: Deposit
        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Deposit amount must be positive.");
                return;
            }
            _balance += amount;
            Console.WriteLine($"Deposited ₹{amount:F2}. New balance: ₹{_balance:F2}");
        }

        // Method: Withdraw
        public void Withdraw(double amount)
        {
            if (amount > _balance)
                Console.WriteLine("Insufficient funds.");
            else
            {
                _balance -= amount;
                Console.WriteLine($"Withdrawn ₹{amount:F2}. New balance: ₹{_balance:F2}");
            }
        }

        // Method: Display info
        public void PrintStatement()
        {
            Console.WriteLine("─────────────────────────────────");
            Console.WriteLine($"  Account Holder : {AccountHolder}");
            Console.WriteLine($"  Account Number : {AccountNumber}");
            Console.WriteLine($"  Balance        : ₹{_balance:F2}");
            Console.WriteLine("─────────────────────────────────");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BankAccount acc = new BankAccount("Akshay Sagar", "SB-1001", 5000.00);
            acc.PrintStatement();

            acc.Deposit(1500);
            acc.Withdraw(800);
            acc.Withdraw(10000);

            acc.PrintStatement();
        }
    }
}
```

### Part B: Inheritance — `Shape` → `Circle` and `Rectangle`

```csharp
using System;

namespace InheritanceDemo
{
    // Base (parent) class
    class Shape
    {
        public string Colour { get; set; }

        public Shape(string colour)
        {
            Colour = colour;
        }

        // virtual means child classes CAN override this method
        public virtual double Area()
        {
            return 0;
        }

        public void Describe()
        {
            Console.WriteLine($"I am a {Colour} shape with area {Area():F2}");
        }
    }

    // Derived (child) class — inherits everything from Shape
    class Circle : Shape
    {
        public double Radius { get; set; }

        public Circle(string colour, double radius) : base(colour)
        {
            Radius = radius;
        }

        // override replaces the parent's Area() with our own calculation
        public override double Area()
        {
            return Math.PI * Radius * Radius;
        }
    }

    class Rectangle : Shape
    {
        public double Width  { get; set; }
        public double Height { get; set; }

        public Rectangle(string colour, double width, double height) : base(colour)
        {
            Width  = width;
            Height = height;
        }

        public override double Area()
        {
            return Width * Height;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Shape[] shapes =
            {
                new Circle("Red",    7.0),
                new Rectangle("Blue", 5.0, 3.0),
                new Circle("Green",  4.5)
            };

            foreach (Shape s in shapes)
                s.Describe();
        }
    }
}
```

### Part C: `abstract` Class and `interface` — Payment System

```csharp
using System;

namespace AbstractInterfaceDemo
{
    // Interface: defines a CONTRACT — any class that implements it MUST
    // provide these methods
    interface IPayable
    {
        void ProcessPayment(double amount);
        string GetPaymentSummary();
    }

    // Abstract class: provides shared code but CANNOT be instantiated directly
    abstract class Payment : IPayable
    {
        public string Payer   { get; protected set; }
        public double Amount  { get; protected set; }

        protected Payment(string payer)
        {
            Payer = payer;
        }

        // Abstract method — subclasses MUST implement this
        public abstract void ProcessPayment(double amount);

        // Concrete method — shared by all subclasses
        public string GetPaymentSummary()
        {
            return $"{Payer} paid ₹{Amount:F2} via {GetType().Name}";
        }
    }

    class CashPayment : Payment
    {
        public CashPayment(string payer) : base(payer) { }

        public override void ProcessPayment(double amount)
        {
            Amount = amount;
            Console.WriteLine($"Cash payment of ₹{amount:F2} received from {Payer}.");
        }
    }

    class UpiPayment : Payment
    {
        private string _upiId;

        public UpiPayment(string payer, string upiId) : base(payer)
        {
            _upiId = upiId;
        }

        public override void ProcessPayment(double amount)
        {
            Amount = amount;
            Console.WriteLine($"UPI payment of ₹{amount:F2} from {_upiId} processed.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Payment[] payments =
            {
                new CashPayment("Akshay"),
                new UpiPayment("Diksha", "diksha@upi")
            };

            foreach (Payment p in payments)
            {
                p.ProcessPayment(500.00);
                Console.WriteLine(p.GetPaymentSummary());
                Console.WriteLine();
            }
        }
    }
}
```

---

## 4. Expected Output

**Output - Part A:**
```text
─────────────────────────────────
  Account Holder : Akshay Sagar
  Account Number : SB-1001
  Balance        : ₹5000.00
─────────────────────────────────
Deposited ₹1500.00. New balance: ₹6500.00
Withdrawn ₹800.00. New balance: ₹5700.00
Insufficient funds.
─────────────────────────────────
  Account Holder : Akshay Sagar
  Account Number : SB-1001
  Balance        : ₹5700.00
─────────────────────────────────
```

**Output - Part B:**
```text
I am a Red shape with area 153.94
I am a Blue shape with area 15.00
I am a Green shape with area 63.62
```

**Output - Part C:**
```text
Cash payment of ₹500.00 received from Akshay.
Akshay paid ₹500.00 via CashPayment

UPI payment of ₹500.00 from diksha@upi processed.
Diksha paid ₹500.00 via UpiPayment
```

---

## 5. Viva / Discussion Questions

1. What are the four pillars of OOP? Give a one-line definition for each.
2. What is the difference between `private`, `public`, and `protected` access modifiers?
3. Why should fields be `private` and accessed through properties?
4. What is the difference between `virtual`/`override` and `abstract`?
5. What is the difference between an `abstract` class and an `interface`?
6. What does the `: base(...)` call in a derived constructor do?
7. What is method overloading? How does it differ from method overriding?
8. Can you instantiate an `abstract` class directly with `new`?
