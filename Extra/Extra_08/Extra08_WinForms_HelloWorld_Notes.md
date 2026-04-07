# Extra Experiment 08 — Windows Forms: Hello World Form | Notes

---

## Console App vs WinForms App

```
Console App                         Windows Forms App
──────────────────────────────      ──────────────────────────────────
Runs top-to-bottom                  Event-driven — waits for user action
No visual interface                 GUI window with buttons, labels, etc.
Input via Console.ReadLine()        Input via TextBox controls
Output via Console.WriteLine()      Output via Label.Text or MessageBox
Program ends when Main() returns    Program runs until the window is closed
```

---

## The Three Key Files in a WinForms Project

```
Form1.cs             ← YOUR code — event handlers, business logic
Form1.Designer.cs    ← AUTO-GENERATED — control layout (never edit manually)
Program.cs           ← Entry point — calls Application.Run(new Form1())
```

---

## Event-Driven Model

```
User clicks button
       ↓
Windows sends WM_CLICK message
       ↓
.NET message loop receives it
       ↓
Calls your event handler: btnGreet_Click(sender, e)
       ↓
Your code runs (updates lblMessage.Text)
       ↓
Loop goes back to waiting...
```

---

## Wiring an Event Handler — Two Ways

```csharp
// Way 1: Double-click the control in Designer → VS creates the method
// (VS also adds the += line to Designer.cs automatically)

// Way 2: Manually in code
btnGreet.Click += new EventHandler(btnGreet_Click);
// or shorter: 
btnGreet.Click += btnGreet_Click;

private void btnGreet_Click(object sender, EventArgs e)
{
    // sender = the control that fired the event (the button)
    // e      = extra data about the event (usually empty for Click)
}
```

---

## Most Used Control Properties

```csharp
// Label
lblMessage.Text      = "Hello!";
lblMessage.ForeColor = Color.Red;
lblMessage.Font      = new Font("Arial", 14, FontStyle.Bold);
lblMessage.Visible   = false;   // hide the label

// Button
btnGreet.Text        = "Click Me";
btnGreet.Enabled     = false;   // grey out (disable) the button
btnGreet.BackColor   = Color.LightBlue;

// Form
this.Text            = "My Window Title";
this.Size            = new Size(400, 300);
this.BackColor       = Color.White;
```

---

## Showing Messages

```csharp
// Simple message box
MessageBox.Show("Hello, World!");

// With title and icon
MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

// Reading the result
DialogResult result = MessageBox.Show("Delete?", "Confirm", MessageBoxButtons.YesNo);
if (result == DialogResult.Yes)
    // proceed with deletion
```

---

## Common Mistakes

```csharp
// 1. Editing Designer.cs by hand — layout breaks when Designer regenerates it
//    → Always use the Properties panel or Designer to change control properties

// 2. Doing heavy work on the UI thread — freezes the window
private void btnLoad_Click(object sender, EventArgs e)
{
    System.Threading.Thread.Sleep(5000);   // BAD — window freezes for 5 seconds
}
// → Use async/await or BackgroundWorker for long operations

// 3. Forgetting to name controls before wiring events
//    → Set the Name property in Properties panel FIRST, then double-click to add handler
```
