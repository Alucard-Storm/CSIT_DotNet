# Extra Experiment 10 — Windows Forms: Login Form

**Subject:** CSIT-406 .NET Framework Lab
**Location:** RGPV, Bhopal

---

## 1. Aim

To build a Windows Forms login screen that validates a username and password, demonstrates masked input, shows an error `Label`, disables the login button after three failed attempts, and opens a welcome form upon successful login.

## 2. Theory

A login form is a fundamental UI pattern. This experiment introduces several important concepts:

| Concept | Control / Technique |
|---|---|
| Password masking | `TextBox.PasswordChar = '*'` |
| Input validation | Checking `TextBox.Text` before acting |
| Attempt limiting | `private int _attempts` counter field |
| Disabling a control | `button.Enabled = false` |
| Opening a second form | `new Form2().ShowDialog()` |
| Closing the current form | `this.Close()` |

**`ShowDialog()`** opens a form as a **modal** window — the user cannot interact with any other window in the application until this one is closed. `Show()` opens a **modeless** window (non-blocking).

> **Note on real applications:** Passwords must **never** be stored as plain strings. In production code, use hashed + salted passwords (e.g., `BCrypt`). This experiment uses hardcoded strings for simplicity only.

---

## 3. Project Setup

Create a new **Windows Forms App (.NET Framework)** project named `LoginForm`. The project will have two forms: `LoginForm` (the login screen) and `WelcomeForm` (shown after success).

Add a second form: **Project** → **Add** → **Windows Form** → name it `WelcomeForm`.

---

## 4. Implementation

### Step 1 — Design `LoginForm`

Set `LoginForm.Text` = `"Student Portal — Login"`, `Size` = `380, 300`, `StartPosition` = `CenterScreen`.

| Control | Name | Text / Properties |
|---|---|---|
| `Label` | `lblTitle` | `Student Portal`, Font 16pt Bold |
| `Label` | — | `Username:` |
| `TextBox` | `txtUsername` | `Size 200,23` |
| `Label` | — | `Password:` |
| `TextBox` | `txtPassword` | `Size 200,23`, `PasswordChar = *` |
| `Button` | `btnLogin` | `Login` |
| `Button` | `btnClear` | `Clear` |
| `Label` | `lblError` | *(empty)*, `ForeColor Red`, hidden initially |

### Step 2 — `LoginForm.cs`

```csharp
using System;
using System.Windows.Forms;

namespace LoginFormDemo
{
    public partial class LoginForm : Form
    {
        // Hardcoded credentials — for demonstration only
        private const string ValidUsername = "akshay";
        private const string ValidPassword = "rgpv@2026";

        private int _failedAttempts = 0;
        private const int MaxAttempts = 3;

        public LoginForm()
        {
            InitializeComponent();
            lblError.Visible = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Basic empty-field validation
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ShowError("Please enter both username and password.");
                return;
            }

            // Credential check (case-insensitive username, case-sensitive password)
            if (username.Equals(ValidUsername, StringComparison.OrdinalIgnoreCase)
                && password == ValidPassword)
            {
                // Success — hide login form and show welcome screen
                lblError.Visible = false;
                WelcomeForm welcome = new WelcomeForm(username);
                this.Hide();
                welcome.ShowDialog();
                this.Close();
            }
            else
            {
                _failedAttempts++;
                int remaining = MaxAttempts - _failedAttempts;

                if (_failedAttempts >= MaxAttempts)
                {
                    ShowError("Too many failed attempts. Account locked.");
                    btnLogin.Enabled    = false;
                    txtUsername.Enabled = false;
                    txtPassword.Enabled = false;
                }
                else
                {
                    ShowError($"Invalid credentials. {remaining} attempt(s) remaining.");
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
            lblError.Visible = false;
            txtUsername.Focus();
        }

        private void ShowError(string message)
        {
            lblError.Text    = message;
            lblError.Visible = true;
        }

        // Allow pressing Enter in txtPassword to trigger login
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                btnLogin_Click(sender, e);
        }
    }
}
```

### Step 3 — Design `WelcomeForm`

Set `WelcomeForm.Text` = `"Welcome"`, `Size` = `350, 200`, `StartPosition` = `CenterScreen`.

| Control | Name | Text |
|---|---|---|
| `Label` | `lblWelcome` | *(set from code)*, Font 14pt |
| `Button` | `btnLogout` | `Logout` |

### Step 4 — `WelcomeForm.cs`

```csharp
using System;
using System.Windows.Forms;

namespace LoginFormDemo
{
    public partial class WelcomeForm : Form
    {
        public WelcomeForm(string username)
        {
            InitializeComponent();
            lblWelcome.Text = $"Welcome, {username}!\n\nLogin successful.";
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();   // closes WelcomeForm; ShowDialog() in LoginForm returns
        }
    }
}
```

### Step 5 — `Program.cs`

```csharp
using System;
using System.Windows.Forms;

namespace LoginFormDemo
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
```

---

## 5. Expected Output

**On launch — LoginForm:**
```
        Student Portal
Username: [______________]
Password: [**************]   ← characters masked with *
         [ Login ]  [ Clear ]
```

**After one wrong attempt:**
```
Error: Invalid credentials. 2 attempt(s) remaining.
```

**After three wrong attempts:**
```
Error: Too many failed attempts. Account locked.
(Login button and fields are disabled and greyed out)
```

**After correct login (username: akshay, password: rgpv@2026):**
WelcomeForm opens:
```
Welcome, akshay!

Login successful.
         [ Logout ]
```

---

## 6. Viva / Discussion Questions

1. What is `PasswordChar` on a `TextBox`? Does it affect the actual value of `Text`?
2. What is the difference between `ShowDialog()` and `Show()`?
3. What does `this.Hide()` do versus `this.Close()`?
4. What is `StringComparison.OrdinalIgnoreCase` and why is it used for the username check?
5. What is `string.IsNullOrWhiteSpace()`? How is it different from checking `== ""`?
6. Why should passwords never be stored as plain text in real applications?
7. What does `control.Enabled = false` do visually to a control?
