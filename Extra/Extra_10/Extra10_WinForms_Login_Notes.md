# Extra Experiment 10 — Windows Forms: Login Form | Notes

---

## Password Masking

```csharp
// Set in Designer (Properties panel) or in code:
txtPassword.PasswordChar = '*';   // every character shows as *

// The actual text is still fully readable in code:
string realPassword = txtPassword.Text;   // "rgpv@2026"
```

---

## Input Validation — Empty Field Check

```csharp
// Three ways to check if a TextBox is blank:
txtUsername.Text == ""                           // misses "   " (spaces only)
txtUsername.Text.Trim() == ""                    // better — trims whitespace first
string.IsNullOrWhiteSpace(txtUsername.Text)      // best — also handles null
```

---

## Opening a Second Form

```csharp
// Modal — blocks user from clicking other windows until this one closes
WelcomeForm welcome = new WelcomeForm(username);
welcome.ShowDialog();     // execution pauses here until welcome is closed

// Modeless — both windows are usable simultaneously
WelcomeForm welcome = new WelcomeForm(username);
welcome.Show();           // execution continues immediately
```

---

## Passing Data Between Forms

```csharp
// Option 1: Constructor parameter (simplest)
public WelcomeForm(string username)
{
    InitializeComponent();
    lblWelcome.Text = "Welcome, " + username;
}
// Caller: new WelcomeForm("akshay").ShowDialog();

// Option 2: Public property
public partial class WelcomeForm : Form
{
    public string Username { get; set; }
}
// Caller:
WelcomeForm w = new WelcomeForm();
w.Username = "akshay";
w.ShowDialog();
```

---

## Enabling / Disabling Controls

```csharp
btnLogin.Enabled    = false;   // greyed out — user cannot click
txtUsername.Enabled = false;   // greyed out — user cannot type
btnLogin.Enabled    = true;    // restore
```

---

## Form Lifecycle — Close vs Hide

```csharp
this.Close();    // destroys the form — resources freed, cannot be shown again
this.Hide();     // makes the form invisible — still exists in memory, can Show() again
```

**Pattern for login flow:**
```csharp
// In LoginForm:
this.Hide();              // hide login screen (don't destroy yet)
new WelcomeForm().ShowDialog();   // wait for welcome to close
this.Close();             // now close login form (app exits)
```

---

## `StringComparison` for String Equality

```csharp
// Case-sensitive (default) — "Akshay" ≠ "akshay"
username == ValidUsername

// Case-insensitive — "Akshay" == "akshay"
username.Equals(ValidUsername, StringComparison.OrdinalIgnoreCase)

// For passwords — ALWAYS case-sensitive
password == ValidPassword
```

---

## Common Mistakes

```csharp
// 1. Comparing Text directly without trimming spaces
if (txtUsername.Text == "akshay")      // fails if user typed "akshay "
if (txtUsername.Text.Trim() == "akshay")   // correct

// 2. Using == for string comparison on objects
string a = txtA.Text;
if (a == null) { }    // fine for string — == is overloaded in C# for strings

// 3. Showing the welcome form without hiding the login form first
new WelcomeForm().ShowDialog();   // login form still visible behind welcome
// Fix: this.Hide() before ShowDialog()
```
