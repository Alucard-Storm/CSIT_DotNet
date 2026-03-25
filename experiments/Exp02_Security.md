# Experiment 02 — Role-Based and Permission-Based Security

**Subject:** CSIT-406 .NET Framework Lab
**RGPV, Bhopal**

---

## Aim

Demonstrate role-based and permission-based security using .NET security features.

---

## Theory

.NET provides built-in security through the `System.Security` namespace.

| Security Type | Description |
|---|---|
| **Role-Based Security (RBS)** | Checks if the current user belongs to a role (e.g., Admin, User) |
| **Permission-Based Security** | Checks if code has permission to perform an action (file I/O, network, etc.) |

Key classes:
- `GenericIdentity` — Represents the user identity (name)
- `GenericPrincipal` — Associates identity with roles
- `Thread.CurrentPrincipal` — Gets/sets the active principal for the thread
- `PrincipalPermission` — Demands that the caller has a specific role

> Real-world analogy: A hospital system lets Doctors read patient records, but only Admins can delete them — that's role-based security.

---

## Code

### Part A — Role-Based Security

```csharp
using System;
using System.Security.Principal;
using System.Threading;

namespace RoleBasedSecurity
{
    class Program
    {
        static void Main()
        {
            // Create identity and assign roles
            GenericIdentity identity = new GenericIdentity("Akshay");
            string[] roles = { "Admin", "Teacher" };
            GenericPrincipal principal = new GenericPrincipal(identity, roles);

            // Set as current thread's principal
            Thread.CurrentPrincipal = principal;

            Console.WriteLine("User: " + Thread.CurrentPrincipal.Identity.Name);
            Console.WriteLine("Is Admin: " + Thread.CurrentPrincipal.IsInRole("Admin"));
            Console.WriteLine("Is Student: " + Thread.CurrentPrincipal.IsInRole("Student"));

            // Conditional access
            if (Thread.CurrentPrincipal.IsInRole("Admin"))
                Console.WriteLine("Access Granted: Admin Panel");
            else
                Console.WriteLine("Access Denied");
        }
    }
}
```

### Part B — Permission-Based Security (PrincipalPermission)

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
        static void Main()
        {
            GenericIdentity identity = new GenericIdentity("Akshay");
            GenericPrincipal principal = new GenericPrincipal(identity, new[] { "Admin" });
            Thread.CurrentPrincipal = principal;

            try
            {
                // Demand that caller must be in "Admin" role
                PrincipalPermission permission = new PrincipalPermission("Akshay", "Admin");
                permission.Demand();
                Console.WriteLine("Permission granted for Admin action.");
            }
            catch (SecurityException ex)
            {
                Console.WriteLine("Security Exception: " + ex.Message);
            }

            // Test with a different role demand (will throw)
            try
            {
                PrincipalPermission restricted = new PrincipalPermission("Akshay", "SuperAdmin");
                restricted.Demand();
                Console.WriteLine("SuperAdmin access granted.");
            }
            catch (SecurityException)
            {
                Console.WriteLine("Permission Denied: SuperAdmin role not assigned.");
            }
        }
    }
}
```

---

## Expected Output

**Part A:**
```
User: Akshay
Is Admin: True
Is Student: False
Access Granted: Admin Panel
```

**Part B:**
```
Permission granted for Admin action.
Permission Denied: SuperAdmin role not assigned.
```

---

## Viva Questions

1. What is the difference between Authentication and Authorization?
2. What does `Thread.CurrentPrincipal` represent?
3. What exception is thrown when a `PrincipalPermission.Demand()` fails?
4. Name the two interfaces used for identity in .NET.
5. How does role-based security differ from claims-based security?

---

[Back to Index](../README.md)
