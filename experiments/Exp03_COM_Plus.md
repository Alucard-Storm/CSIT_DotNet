# Experiment 03 — COM+ Component Registration

**Subject:** CSIT-406 .NET Framework Lab
**Location:** RGPV, Bhopal

---

## 1. Aim

To successfully create and register a Component Object Model Plus (COM+) component and use it inside a client application.

## 2. Theory

**COM+ (Component Object Model Plus)** is an older Microsoft technology used to build large-scale, distributed applications. Instead of writing complex system-level code yourself, COM+ provides built-in services for your C# classes to use automatically.

| COM+ Service | Explanation |
|---|---|
| **Distributed Transactions** | Ensures that a group of database actions either all succeed completely or all fail together (commit or rollback). |
| **Object Pooling** | Keeps a pool of ready-to-use objects in memory to speed up the application, rather than creating new objects from scratch every time. |
| **Role-Based Security** | Restricts who can use the component based on predefined user roles. |
| **Just-in-Time Action** | Saves server memory by only turning objects "on" right right when a method is called, and turning them "off" immediately after. |

### Key Classes for .NET COM+
- **`ServicedComponent`**: A special base class. Your C# class must inherit from this to be recognized by the COM+ engine.
- **`[Transaction]`**: An attribute placed above your class to tell COM+ that this class requires transaction management.
- **`ContextUtil.SetComplete()`**: A method used to tell COM+ that your code ran perfectly and the transaction should be saved (committed).
- **`ContextUtil.SetAbort()`**: A method used to tell COM+ that an error occurred and the transaction should be cancelled (rolled back).

*Instructional Example:* Consider a digital banking transfer. If you move currency from Account A to Account B, taking money out of A and putting money into B are two separate steps. COM+ ensures that if the deposit into B fails, the withdrawal from A is automatically reversed.

---

## 3. Implementation Code

### Step 1: Create the COM+ Component

*Instruction:* Create a **Class Library (.NET Framework)** project named `BankComponent`.

```csharp
// File: BankService.cs
using System;
using System.EnterpriseServices;
using System.Runtime.InteropServices;

// Required attributes to name the application in the COM+ catalog
[assembly: ApplicationName("BankCOMPlusApp")]
[assembly: ApplicationActivation(ActivationOption.Server)]

namespace BankComponent
{
    // The class must require a transaction
    [Transaction(TransactionOption.Required)]
    public class BankService : ServicedComponent
    {
        // AutoComplete tells COM+ to commit if no errors happen, or abort if an error is thrown
        [AutoComplete]  
        public string Transfer(string fromAccount, string toAccount, double amount)
        {
            try
            {
                // Simulate taking money out and putting it in
                Console.WriteLine($"Debiting {amount} from {fromAccount}");
                Console.WriteLine($"Crediting {amount} to {toAccount}");

                // Confirm the transaction was successful
                ContextUtil.SetComplete();
                return $"Transfer of {amount} from {fromAccount} to {toAccount} was successful.";
            }
            catch (Exception ex)
            {
                // If anything goes wrong, cancel the transaction completely
                ContextUtil.SetAbort();
                return "Transfer failed due to error: " + ex.Message;
            }
        }
    }
}
```

### Step 2: Register the Component

*Instruction:* Open the **Developer Command Prompt for Visual Studio** as an Administrator and run the following commands to install your library into Windows.

```bash
# 1. Create a strong name key to securely sign your component
sn -k BankComponent.snk

# 2. Register your DLL with the Windows COM+ Services
regsvcs BankComponent.dll
```

### Step 3: Create the Client Application

*Instruction:* Create a **Console Application (.NET Framework)** project and add a reference to your `BankComponent.dll` file.

```csharp
// File: Program.cs
using System;
using BankComponent;

namespace COMClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Connecting to the COM+ BankService...");
            
            // Create the COM+ object
            BankService bank = new BankService();
            
            // Call the transfer method remotely
            string finalResult = bank.Transfer("Alice", "Bob", 5000.00);
            
            Console.WriteLine("Result: " + finalResult);
        }
    }
}
```

---

## 4. Expected Output

```text
Connecting to the COM+ BankService...
Debiting 5000 from Alice
Crediting 5000 to Bob
Result: Transfer of 5000 from Alice to Bob was successful.
```

---

## 5. Verifying the Registration in Windows

1. Open the Windows Run dialog and type `dcomcnfg` to open **Component Services**.
2. Navigate the tree: **Component Services** → **Computers** → **My Computer** → **COM+ Applications**.
3. You will see your new application `BankCOMPlusApp` listed.
4. Right-click the application and select **Properties** to inspect its transaction settings.

---

## 6. Viva / Discussion Questions

1. **Concept Definition:** What is COM+ and what major benefits does it provide to enterprise software developers?
2. **Class Inheritance:** Why does a .NET class have to inherit from `ServicedComponent` to work inside COM+?
3. **Transaction Control:** Explain exactly what `ContextUtil.SetComplete()` and `ContextUtil.SetAbort()` do during code execution.
4. **Attributes:** How does the `[AutoComplete]` attribute simplify transaction management for developers?
5. **Security Registration:** Why does Windows require a COM+ assembly to have a strong name (using the `sn.exe` tool)?

---

[Back to Main Index](../README.md)
