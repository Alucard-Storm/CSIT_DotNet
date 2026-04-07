# Extra Experiment 09 — Windows Forms: Calculator

**Subject:** CSIT-406 .NET Framework Lab
**Location:** RGPV, Bhopal

---

## 1. Aim

To build a functional four-operation calculator as a Windows Forms application, practising the use of `TextBox`, `Button`, and `Label` controls, reading user input from controls, performing arithmetic, and handling division-by-zero gracefully.

## 2. Theory

This experiment introduces the most commonly used input control — the **TextBox** — and demonstrates how to read numeric data from it, validate it, and display a result back on the form.

| Control | Purpose in This Experiment |
|---|---|
| `TextBox` | Accepts numeric input from the user (two operands) |
| `Button` | Triggers an arithmetic operation when clicked |
| `Label` | Displays the computed result or an error message |
| `GroupBox` | Groups related controls visually under a title |

**Key technique — reading a number from a TextBox:**
```csharp
if (double.TryParse(txtA.Text, out double a)) { ... }
```
`TryParse` is always preferred over `double.Parse` because it returns `false` instead of throwing an exception when the user types non-numeric text.

---

## 3. Project Setup

1. Create a new **Windows Forms App (.NET Framework)** project named `CalculatorForm`.
2. Set `Form1.Text` = `"Calculator"`, `Form1.Size` = `400, 320`, `StartPosition` = `CenterScreen`.

---

## 4. Implementation

### Step 1 — Design the Form

Place the following controls on `Form1`:

| Control | Name | Text / Properties |
|---|---|---|
| `Label` | `lblA` | `Number 1:` |
| `TextBox` | `txtA` | *(empty)*, `Size 100,23` |
| `Label` | `lblB` | `Number 2:` |
| `TextBox` | `txtB` | *(empty)*, `Size 100,23` |
| `Button` | `btnAdd` | `+  Add` |
| `Button` | `btnSubtract` | `−  Subtract` |
| `Button` | `btnMultiply` | `×  Multiply` |
| `Button` | `btnDivide` | `÷  Divide` |
| `Button` | `btnClear` | `Clear` |
| `Label` | `lblResult` | `Result: —` |
| | | `Font: 12pt Bold` |

### Step 2 — Code (`Form1.cs`)

```csharp
using System;
using System.Windows.Forms;

namespace CalculatorForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // ── Helper: parse both TextBoxes and return true if both are valid ──
        private bool TryGetOperands(out double a, out double b)
        {
            a = 0; b = 0;

            if (!double.TryParse(txtA.Text, out a))
            {
                ShowError("Number 1 is not a valid number.");
                txtA.Focus();
                return false;
            }
            if (!double.TryParse(txtB.Text, out b))
            {
                ShowError("Number 2 is not a valid number.");
                txtB.Focus();
                return false;
            }
            return true;
        }

        private void ShowResult(double value, string operation)
        {
            lblResult.Text      = $"Result: {value}";
            lblResult.ForeColor = System.Drawing.Color.DarkGreen;
        }

        private void ShowError(string message)
        {
            lblResult.Text      = "Error: " + message;
            lblResult.ForeColor = System.Drawing.Color.Red;
        }

        // ── Button event handlers ──

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (TryGetOperands(out double a, out double b))
                ShowResult(a + b, "+");
        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            if (TryGetOperands(out double a, out double b))
                ShowResult(a - b, "−");
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            if (TryGetOperands(out double a, out double b))
                ShowResult(a * b, "×");
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            if (!TryGetOperands(out double a, out double b)) return;

            if (b == 0)
            {
                ShowError("Cannot divide by zero.");
                return;
            }
            ShowResult(a / b, "÷");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtA.Clear();
            txtB.Clear();
            lblResult.Text      = "Result: —";
            lblResult.ForeColor = System.Drawing.Color.Black;
            txtA.Focus();
        }

        // Allow only digits, a decimal point, minus sign, and control keys in TextBoxes
        private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' &&
                e.KeyChar != '-' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;   // swallow the keystroke — character not typed
            }
        }
    }
}
```

> **Wiring the KeyPress handler:** In the Designer, select `txtA`, open the Properties panel, click the **Events** (lightning bolt) tab, and double-click `KeyPress`. Name it `txtNumber_KeyPress`. Repeat for `txtB`.

---

## 5. Expected Output

**Addition (10 + 5):**
```
Number 1: 10
Number 2: 5
[ +  Add ]  clicked
Result: 15   ← shown in green
```

**Division by zero (8 ÷ 0):**
```
Number 1: 8
Number 2: 0
[ ÷  Divide ]  clicked
Error: Cannot divide by zero.   ← shown in red
```

**Invalid input ("abc" as Number 1):**
```
Number 1: abc
[ +  Add ]  clicked
Error: Number 1 is not a valid number.   ← shown in red, cursor moves to txtA
```

---

## 6. Viva / Discussion Questions

1. Why is `double.TryParse` preferred over `double.Parse` for user input?
2. What is the `KeyPressEventArgs.Handled` property? What happens when you set it to `true`?
3. What does `txtA.Focus()` do, and why is it useful after showing an error?
4. What is the difference between `TextBox.Clear()` and setting `TextBox.Text = ""`?
5. How would you extend this calculator to also show a history of all previous calculations?
6. What is a `GroupBox` control used for?
7. Explain the `sender` parameter in `btnAdd_Click(object sender, EventArgs e)`. How could you use it to share one handler across all four operator buttons?
