# Experiment 02 — Role-Based and Permission-Based Security | Notes

---

## The Big Idea

Imagine a university system. A **student** can view their own marks. A **professor** can update marks. A **principal** can delete records. This is **role-based security** — what you can do depends on your role.

```
User = Akshay
Roles = ["Admin", "Teacher"]

IsInRole("Admin")   → True   (Access Granted)
IsInRole("Student") → False  (Access Denied)
```

---

## Step-by-Step: How to Do It in C#

### Step 1 — Create an Identity (Who are you?)

```csharp
GenericIdentity identity = new GenericIdentity("Akshay");
// "Akshay" is now a recognized user in the system
```

### Step 2 — Assign Roles (What groups do you belong to?)

```csharp
string[] roles = { "Admin", "Teacher" };
GenericPrincipal principal = new GenericPrincipal(identity, roles);
```

### Step 3 — Set as Active User for the Program

```csharp
Thread.CurrentPrincipal = principal;
// Now the entire program knows the current user is Akshay
```

### Step 4 — Check Before Allowing Access

```csharp
if (Thread.CurrentPrincipal.IsInRole("Admin"))
    Console.WriteLine("Welcome to Admin Panel!");
else
    Console.WriteLine("Access Denied.");
```

**Output:**
```
User Logged In: Akshay
Has Admin Role: True
Has Student Role: False
Welcome to Admin Panel!
```

---

## PrincipalPermission — Forcing a Role Check

Instead of using `if`, you can force the check. If the role is wrong, C# automatically throws an error.

```csharp
PrincipalPermission permission = new PrincipalPermission("Akshay", "Admin");
permission.Demand();   // Runs fine — Akshay IS an Admin

PrincipalPermission restricted = new PrincipalPermission("Akshay", "SuperAdmin");
restricted.Demand();   // Throws SecurityException — no SuperAdmin role!
```

**Output:**
```
Success: Admin permission granted.
Failure: You do not have the SuperAdmin role.
```

---

## Key Points to Remember

| Term | Meaning |
|---|---|
| Authentication | Proving WHO you are (login) |
| Authorization | Proving WHAT you can do (role check) |
| `GenericIdentity` | Stores the user's name |
| `GenericPrincipal` | Links name + roles together |
| `Thread.CurrentPrincipal` | The currently active user |
| `PrincipalPermission.Demand()` | Forces a role check; throws error if it fails |
