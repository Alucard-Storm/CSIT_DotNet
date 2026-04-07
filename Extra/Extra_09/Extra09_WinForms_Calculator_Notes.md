# Extra Experiment 09 — Windows Forms: Calculator | Notes

---

## Reading Numbers from a TextBox

```csharp
// WRONG — throws FormatException if user types "abc"
double a = double.Parse(txtA.Text);

// CORRECT — returns false if invalid, no crash
if (double.TryParse(txtA.Text, out double a))
{
    // a is valid, use it
}
else
{
    lblResult.Text = "Error: invalid number";
}
```

---

## Displaying Output on the Form

```csharp
lblResult.Text      = "Result: " + answer;         // update the label
lblResult.ForeColor = System.Drawing.Color.Green;  // change text colour
lblResult.ForeColor = System.Drawing.Color.Red;    // for errors
lblResult.ForeColor = System.Drawing.Color.Black;  // reset to default
```

---

## Blocking Invalid Characters in a TextBox (`KeyPress`)

```csharp
private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
{
    // char.IsControl catches Backspace, Delete, arrow keys (must allow these)
    if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
    {
        e.Handled = true;   // true = "I've handled it" = don't type the character
    }
}
```

Wire this to **both** TextBoxes by selecting each one in the Designer → Events tab → KeyPress → type `txtNumber_KeyPress`.

---

## Sharing One Handler Across Multiple Buttons

Instead of writing four separate `btnAdd_Click`, `btnSubtract_Click`, etc., you can use `sender` to identify which button was clicked:

```csharp
private void btnOperator_Click(object sender, EventArgs e)
{
    if (!TryGetOperands(out double a, out double b)) return;

    Button clicked = (Button)sender;   // cast sender back to Button

    double result = clicked.Name switch
    {
        "btnAdd"      => a + b,
        "btnSubtract" => a - b,
        "btnMultiply" => a * b,
        "btnDivide"   => b != 0 ? a / b : throw new DivideByZeroException(),
        _             => 0
    };
    lblResult.Text = "Result: " + result;
}
```

Wire all four buttons to `btnOperator_Click` in their `Click` event.

---

## Common TextBox Properties

```csharp
txtA.Text          // get or set the text content
txtA.Clear()       // same as txtA.Text = ""
txtA.Focus()       // move keyboard cursor to this control
txtA.SelectAll()   // highlight all text (convenient for editing)
txtA.ReadOnly      = true;   // user cannot type — display only
txtA.PasswordChar  = '*';    // masks text (for password fields)
txtA.MaxLength     = 10;     // limit input to 10 characters
txtA.TextAlign     = HorizontalAlignment.Right;  // right-align numbers
```

---

## Quick Layout Tips

- Use `Tab` order (View → Tab Order in Designer) to make Tab key move logically between controls.
- Use `AcceptButton` on the Form to make pressing **Enter** trigger a button (e.g., the `=` button).
- Use `CancelButton` on the Form to make pressing **Escape** trigger the Clear button.

```csharp
// In Form constructor or Load event:
this.AcceptButton = btnAdd;     // Enter key fires btnAdd_Click
this.CancelButton = btnClear;   // Escape key fires btnClear_Click
```
