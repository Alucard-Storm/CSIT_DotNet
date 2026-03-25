# Experiment 03 — COM+ Component Registration | Notes

---

## What is COM+?

COM+ is a Windows technology that lets you build reusable server-side components. The most useful thing it does is manage **transactions** automatically.

A transaction is a group of actions that must ALL succeed together, or ALL be cancelled together.

**Example — Bank Transfer:**
```
Step 1: Remove 5000 from Alice's account
Step 2: Add 5000 to Bob's account

If Step 2 fails → Step 1 must be CANCELLED automatically.
COM+ handles this cancellation for you.
```

---

## The Three Important Parts

### 1. Your Component Class

Your class must inherit from `ServicedComponent`. This tells COM+ it is allowed to manage it.

```csharp
[Transaction(TransactionOption.Required)]     // "I need a transaction"
public class BankService : ServicedComponent  // Must inherit this
{
    [AutoComplete]  // Success → SetComplete, Error → SetAbort automatically
    public string Transfer(string from, string to, double amount)
    {
        Console.WriteLine($"Removing {amount} from {from}");
        Console.WriteLine($"Adding {amount} to {to}");
        ContextUtil.SetComplete();  // "Everything worked fine!"
        return "Transfer successful.";
    }
}
```

### 2. Register it with Windows

Open **Developer Command Prompt** as Administrator:

```bash
sn -k BankComponent.snk      # Give your DLL a secure signature
regsvcs BankComponent.dll    # Register it with COM+ Services
```

### 3. Your Client Code

The client just creates the object and calls the method.

```csharp
BankService bank = new BankService();
string result = bank.Transfer("Alice", "Bob", 5000);
Console.WriteLine(result);
```

**Output:**
```
Removing 5000 from Alice
Adding 5000 to Bob
Transfer successful.
```

---

## Key Points to Remember

| Term | Meaning |
|---|---|
| `ServicedComponent` | Parent class required for all COM+ components |
| `[Transaction(Required)]` | This component needs a transaction |
| `ContextUtil.SetComplete()` | "All steps worked, save the transaction" |
| `ContextUtil.SetAbort()` | "Something failed, cancel all steps" |
| `[AutoComplete]` | Automatically commits or rolls back based on exceptions |
| `regsvcs` | The command to register your DLL with COM+ |
