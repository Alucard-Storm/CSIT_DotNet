# Extra Experiment 08 — Windows Forms: Hello World Form

**Subject:** CSIT-406 .NET Framework Lab
**Location:** RGPV, Bhopal

---

## 1. Aim

To create the first Windows Forms application in C# — a GUI window with a label and a button that displays a greeting message when clicked, and to understand the event-driven programming model.

## 2. Theory

**Windows Forms (WinForms)** is the .NET Framework's built-in library for building desktop GUI applications. Unlike console apps which run top-to-bottom, WinForms applications are **event-driven** — they wait and respond to user actions (clicks, key presses, mouse movements).

| Concept | Explanation |
|---|---|
| **Form** | The window itself — inherits from `System.Windows.Forms.Form` |
| **Control** | A UI element placed on a Form (Button, Label, TextBox, etc.) |
| **Event** | A notification that something happened (e.g., `button1.Click`) |
| **Event Handler** | A method that runs in response to an event (e.g., `button1_Click`) |
| **Designer** | Visual Studio's drag-and-drop UI editor that generates code in `Form1.Designer.cs` |
| **`InitializeComponent()`** | Auto-generated method called in the constructor that creates and positions all controls |

**How WinForms works:**
1. `Program.cs` calls `Application.Run(new Form1())` — this starts the message loop.
2. The message loop waits for Windows messages (mouse click, key press, etc.).
3. When you click a button, Windows posts a `WM_CLICK` message; .NET converts it and calls your event handler method.

---

## 3. Project Setup

1. Open **Visual Studio** → **Create a new project**.
2. Select **Windows Forms App (.NET Framework)** → click **Next**.
3. Name the project `HelloWorldForm` → select **.NET Framework 4.8** → click **Create**.
4. Visual Studio opens the Form Designer showing a blank grey `Form1`.

---

## 4. Implementation

### Step 1 — Design the Form (Designer)

In the **Toolbox** panel, drag and drop these controls onto `Form1`:

| Control | Property | Value |
|---|---|---|
| `Label` | `Name` | `lblMessage` |
| | `Text` | `Click the button below!` |
| | `Font` | `Microsoft Sans Serif, 14pt` |
| | `Location` | `80, 60` |
| `Button` | `Name` | `btnGreet` |
| | `Text` | `Say Hello` |
| | `Location` | `130, 140` |
| | `Size` | `120, 40` |

Also set the **Form** itself:

| Property | Value |
|---|---|
| `Text` (title bar) | `My First WinForms App` |
| `Size` | `400, 280` |
| `StartPosition` | `CenterScreen` |

### Step 2 — Write the Event Handler (`Form1.cs`)

Double-click the `btnGreet` button in the Designer — Visual Studio creates the `btnGreet_Click` method automatically and switches to the code view.

```csharp
using System;
using System.Windows.Forms;

namespace HelloWorldForm
{
    public partial class Form1 : Form
    {
        // Counter to track how many times the button has been clicked
        private int _clickCount = 0;

        public Form1()
        {
            InitializeComponent();   // sets up all the controls from the Designer
        }

        // This method runs every time the user clicks btnGreet
        private void btnGreet_Click(object sender, EventArgs e)
        {
            _clickCount++;
            lblMessage.Text = $"Hello, World!\nYou clicked {_clickCount} time(s).";
            lblMessage.ForeColor = System.Drawing.Color.DarkGreen;
        }
    }
}
```

### Step 3 — The Auto-Generated Designer File (`Form1.Designer.cs`)

Visual Studio generates this file. You do **not** edit it manually — it is shown here for understanding only.

```csharp
// Form1.Designer.cs (auto-generated — do not edit by hand)
partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.lblMessage = new System.Windows.Forms.Label();
        this.btnGreet   = new System.Windows.Forms.Button();
        this.SuspendLayout();

        // lblMessage
        this.lblMessage.Font     = new System.Drawing.Font("Microsoft Sans Serif", 14F);
        this.lblMessage.Location = new System.Drawing.Point(80, 60);
        this.lblMessage.Size     = new System.Drawing.Size(300, 60);
        this.lblMessage.Text     = "Click the button below!";

        // btnGreet
        this.btnGreet.Location = new System.Drawing.Point(130, 140);
        this.btnGreet.Size     = new System.Drawing.Size(120, 40);
        this.btnGreet.Text     = "Say Hello";
        this.btnGreet.Click   += new System.EventHandler(this.btnGreet_Click);

        // Form1
        this.ClientSize = new System.Drawing.Size(400, 250);
        this.Controls.Add(this.lblMessage);
        this.Controls.Add(this.btnGreet);
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "My First WinForms App";
        this.ResumeLayout(false);
    }

    private System.Windows.Forms.Label  lblMessage;
    private System.Windows.Forms.Button btnGreet;
}
```

### Step 4 — Entry Point (`Program.cs`)

```csharp
using System;
using System.Windows.Forms;

namespace HelloWorldForm
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());   // opens the window and starts the message loop
        }
    }
}
```

---

## 5. Expected Output

**On launch:**
A window titled "My First WinForms App" appears at the centre of the screen displaying:
```
Click the button below!
[ Say Hello ]
```

**After clicking "Say Hello" three times:**
The label updates to:
```
Hello, World!
You clicked 3 time(s).
```
*(Text colour changes to dark green.)*

---

## 6. Viva / Discussion Questions

1. What is the difference between a **console application** and a **Windows Forms application**?
2. What is an **event handler**? What are its two standard parameters (`sender` and `e`)?
3. What does `Application.Run()` do? What happens if you remove it?
4. What is `InitializeComponent()` and where is it defined?
5. What is the role of `Form1.Designer.cs`? Should you edit it manually?
6. What does `[STAThread]` mean on the `Main` method?
7. How does `+=` attach an event handler to a control's event?
8. What is the **message loop** in a GUI application?
