# Extra Experiment 05 — Classes and Objects | Notes

---

## Class Anatomy

```csharp
class BankAccount
{
    // 1. Fields — internal state (keep private)
    private double _balance;

    // 2. Properties — controlled access to fields
    public string AccountHolder { get; private set; }

    // 3. Constructor — runs when you write: new BankAccount(...)
    public BankAccount(string holder, double deposit)
    {
        AccountHolder = holder;
        _balance      = deposit;
    }

    // 4. Methods — behaviour / actions
    public void Deposit(double amount)
    {
        _balance += amount;
    }
}

// Creating an object (instance)
BankAccount acc = new BankAccount("Akshay", 1000);
acc.Deposit(500);
```

---

## Access Modifiers

| Modifier | Visible To |
|---|---|
| `public` | Anyone, anywhere |
| `private` | Only inside the same class |
| `protected` | Same class AND derived (child) classes |
| `internal` | Same project/assembly only |

**Golden rule:** fields should be `private`; expose data through `public` properties.

---

## Properties — get and set

```csharp
private double _balance;

// Full property with validation
public double Balance
{
    get { return _balance; }           // read
    set
    {
        if (value >= 0) _balance = value;   // write with guard
    }
}

// Auto-property (when no validation needed)
public string Name { get; set; }

// Read-only property (only code inside the class can set it)
public string Id { get; private set; }
```

---

## Inheritance

```
Parent (base) class                 Child (derived) class
──────────────────────              ──────────────────────────────────
class Shape                         class Circle : Shape
{                                   {
    public string Colour;               public double Radius;
    public virtual double Area()        public override double Area()
    {                                   {
        return 0;                           return Math.PI * Radius * Radius;
    }                                   }
}                                   }
```

- `: Shape` means "Circle inherits everything from Shape"
- `: base(colour)` in the constructor calls the parent's constructor first
- `virtual` = parent says "children CAN replace this method"
- `override` = child says "I AM replacing this method"

---

## Abstract vs Interface

```
abstract class                      interface
────────────────────────────        ────────────────────────────
Can have implemented methods        All methods are abstract by default
Can have fields and constructor     No fields, no constructor
A class can inherit ONE only        A class can implement MANY interfaces
Use for "is-a" relationship         Use for "can-do" relationship
```

```csharp
// Abstract class
abstract class Payment
{
    public abstract void Process(double amount);   // must be overridden
    public void PrintReceipt() { ... }             // can be shared
}

// Interface
interface IPrintable
{
    void Print();    // contract — any class that implements this must provide Print()
}

// A class can do both
class CashPayment : Payment, IPrintable
{
    public override void Process(double amount) { ... }
    public void Print() { ... }
}
```

---

## The Four OOP Pillars — Quick Recall

```
Encapsulation  → private fields + public properties (hide internals)
Inheritance    → child class reuses parent code (: ParentClass)
Polymorphism   → same method, different behaviour (virtual + override)
Abstraction    → expose what matters, hide complexity (abstract / interface)
```

---

## Common Mistakes

```csharp
// 1. Calling a method on a null object
BankAccount acc = null;
acc.Deposit(100);    // NullReferenceException — always initialise before use

// 2. Forgetting the constructor call
BankAccount acc;     // declared but NOT created
acc.Deposit(100);    // ERROR — use: BankAccount acc = new BankAccount(...)

// 3. Infinite property loop
public string Name
{
    get { return Name; }    // calls itself forever → StackOverflowException
    // correct: return _name; (private field)
}
```
