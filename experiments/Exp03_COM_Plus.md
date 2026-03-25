# Experiment 03 — COM+ Component Registration

**Subject:** CSIT-406 .NET Framework Lab
**RGPV, Bhopal**

---

## Aim

Register a COM+ component and use it in a client application.

---

## Theory

**COM+** (Component Object Model Plus) is a Microsoft middleware technology for building distributed, transactional components. It extends COM with services like:

| Service | Description |
|---|---|
| Transactions | Automatic commit/rollback across components |
| Object Pooling | Reuse component instances for performance |
| Role-Based Security | Restrict component access by user role |
| Just-in-Time Activation | Activate/deactivate objects on demand |

Key classes in .NET:
- `ServicedComponent` — Base class for COM+ components (from `System.EnterpriseServices`)
- `[Transaction]` — Attribute to define transaction behavior
- `[ObjectPooling]` — Enables object pooling
- `ContextUtil.SetComplete()` — Commits the transaction
- `ContextUtil.SetAbort()` — Rolls back the transaction

> Real-world analogy: A bank's money transfer — debit from Account A and credit to Account B must happen together. COM+ ensures if one fails, both are rolled back.

---

## Code

### Step 1 — Create the COM+ Component (Class Library Project)

> Create a **Class Library** project named `BankComponent`.

```csharp
// BankComponent.cs
using System;
using System.EnterpriseServices;
using System.Runtime.InteropServices;

// Required for COM+ registration
[assembly: ApplicationName("BankCOMPlusApp")]
[assembly: ApplicationActivation(ActivationOption.Server)]

namespace BankComponent
{
    [Transaction(TransactionOption.Required)]
    [EventTrackingEnabled(true)]
    public class BankService : ServicedComponent
    {
        [AutoComplete]  // Automatically calls SetComplete or SetAbort
        public string Transfer(string from, string to, double amount)
        {
            try
            {
                // Simulate debit and credit
                Console.WriteLine($"Debiting {amount} from {from}");
                Console.WriteLine($"Crediting {amount} to {to}");

                // Signal success to COM+ transaction
                ContextUtil.SetComplete();
                return $"Transfer of {amount} from {from} to {to} successful.";
            }
            catch (Exception ex)
            {
                ContextUtil.SetAbort();
                return "Transfer failed: " + ex.Message;
            }
        }
    }
}
```

### Step 2 — Register the Assembly as COM+

Run these commands in **Developer Command Prompt (as Admin)**:

```bash
# Sign the assembly (required for COM+ registration)
sn -k BankComponent.snk

# Register with COM+ Services
regsvcs BankComponent.dll
```

### Step 3 — Client Application

> Create a separate **Console App** project and add a reference to `BankComponent.dll`.

```csharp
// Client/Program.cs
using System;
using BankComponent;

namespace COMClient
{
    class Program
    {
        static void Main()
        {
            BankService bank = new BankService();
            string result = bank.Transfer("Alice", "Bob", 5000.00);
            Console.WriteLine("Result: " + result);
        }
    }
}
```

---

## Expected Output

```
Debiting 5000 from Alice
Crediting 5000 to Bob
Result: Transfer of 5000 from Alice to Bob successful.
```

---

## Steps to Register in Component Services (GUI)

1. Open **Component Services** → `dcomcnfg`
2. Navigate to **COM+ Applications**
3. Verify `BankCOMPlusApp` is listed after `regsvcs`
4. Right-click → Properties to view transaction settings

---

## Viva Questions

1. What is COM+ and how does it extend COM?
2. What base class must a .NET COM+ component inherit from?
3. What is the role of `ContextUtil.SetComplete()` and `SetAbort()`?
4. What does the `[AutoComplete]` attribute do?
5. Why must a COM+ assembly be strongly named (signed)?

---

[Back to Index](../README.md)
