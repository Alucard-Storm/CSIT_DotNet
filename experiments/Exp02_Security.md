# Experiment 02 — Role-Based and Permission-Based Security

**Subject:** CSIT-406 .NET Framework Lab
**Location:** RGPV, Bhopal

---

## 1. Aim

To demonstrate how to protect an application using role-based security and permission-based security features in the .NET Framework.

## 2. Theory

The .NET Framework provides built-in tools to secure your applications, organized within the `System.Security` namespace. This lab relies on two main ways to check user access:

| Security Type | Explanation |
|---|---|
| **Role-Based Security (RBS)** | Checks if the current user belongs to a specific group or role (such as "Admin" or "Student") before letting them perform an action. |
| **Permission-Based Security** | Deeply connected to Code Access Security (CAS), this checks if the executing code explicitly asserts the permissions necessary to run. |

### Key Classes Used
- **`GenericIdentity`**: A simple class that holds the user's name to identify who is currently logged in.
- **`GenericPrincipal`**: A class that links a specific identity (the user) to an array of roles (the groups they belong to).
- **`Thread.CurrentPrincipal`**: A property that stores the active user and their roles for the current running program thread.
- **`PrincipalPermission`**: A built-in permission class that can demand the current user matches a specific name or role before the code is allowed to continue.

*Instructional Example:* Consider a digital university system. A student and a professor both log in. Role-based security allows the "Student" role to read grades, but only allows the "Professor" role to update those grades.

---

## 3. Implementation Code

### Part A: Role-Based Security

This code demonstrates how to manually create a user, assign roles, and use an `if` statement to restrict access.

```csharp
using System;
using System.Security.Principal;
using System.Threading;

namespace RoleBasedSecurity
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Create the user identity
            GenericIdentity identity = new GenericIdentity("Akshay");
            
            // 2. Define the roles for this user
            string[] roles = { "Admin", "Teacher" };
            
            // 3. Connect the user identity to the roles
            GenericPrincipal principal = new GenericPrincipal(identity, roles);

            // 4. Set this user as the active security principal for the program
            Thread.CurrentPrincipal = principal;

            Console.WriteLine("User Logged In: " + Thread.CurrentPrincipal.Identity.Name);
            Console.WriteLine("Has Admin Role: " + Thread.CurrentPrincipal.IsInRole("Admin"));
            Console.WriteLine("Has Student Role: " + Thread.CurrentPrincipal.IsInRole("Student"));

            // 5. Check role to grant or deny access
            if (Thread.CurrentPrincipal.IsInRole("Admin"))
            {
                Console.WriteLine("Action: Welcome to the Admin Control Panel.");
            }
            else
            {
                Console.WriteLine("Action: Access Denied.");
            }
        }
    }
}
```

### Part B: Permission-Based Security using PrincipalPermission

This code enforces security by demanding a specific role. If the demand is not met, the program automatically throws an error.

```csharp
using System;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace PermissionSecurity
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup the user context as an Admin
            GenericIdentity identity = new GenericIdentity("Akshay");
            GenericPrincipal principal = new GenericPrincipal(identity, new string[] { "Admin" });
            Thread.CurrentPrincipal = principal;

            try
            {
                // Demand 1: The user must have the "Admin" role
                PrincipalPermission requiredPermission = new PrincipalPermission("Akshay", "Admin");
                requiredPermission.Demand();
                
                Console.WriteLine("Success: Admin permission granted.");
            }
            catch (SecurityException ex)
            {
                Console.WriteLine("Security Error: " + ex.Message);
            }

            try
            {
                // Demand 2: The user must have the "SuperAdmin" role
                PrincipalPermission restrictedPermission = new PrincipalPermission("Akshay", "SuperAdmin");
                restrictedPermission.Demand();
                
                Console.WriteLine("Success: SuperAdmin access granted.");
            }
            catch (SecurityException)
            {
                Console.WriteLine("Failure: You do not have the SuperAdmin role.");
            }
        }
    }
}
```

*Note:* `PrincipalPermission` is highly relevant for learning in the .NET Framework lab, but students should be aware it is considered obsolete in modern .NET technologies like .NET Core in favor of claims-based authorization.

---

## 4. Expected Output

**Execution Output - Part A:**
```text
User Logged In: Akshay
Has Admin Role: True
Has Student Role: False
Action: Welcome to the Admin Control Panel.
```

**Execution Output - Part B:**
```text
Success: Admin permission granted.
Failure: You do not have the SuperAdmin role.
```

---

## 5. Viva / Discussion Questions

1. **Definitions:** What is the technical difference between Authentication (verifying who you are) and Authorization (verifying what you can do)?
2. **Security Context:** What information does `Thread.CurrentPrincipal` hold during program execution?
3. **Exception Handling:** What specific exception is thrown when a `PrincipalPermission.Demand()` fails due to missing roles?
4. **Core Interfaces:** Name the two main interfaces used for identity in .NET.
5. **Evolution:** How does classical Role-Based Security differ from modern Claims-Based Identity used in modern web applications?

---

[Back to Main Index](../README.md)
