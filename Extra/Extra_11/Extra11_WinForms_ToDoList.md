# Extra Experiment 11 — Windows Forms: To-Do List App

**Subject:** CSIT-406 .NET Framework Lab
**Location:** RGPV, Bhopal

---

## 1. Aim

To build an interactive To-Do List application using Windows Forms, practising the `ListBox` control for displaying and selecting items, managing a backing `List<string>` collection, and connecting multiple button actions to modify the list at runtime.

## 2. Theory

This experiment introduces the **`ListBox`** control — one of the most useful controls for displaying collections of items — and demonstrates how the visual UI stays in sync with an in-memory data collection.

| Control | Purpose |
|---|---|
| `TextBox` | User types the new task here |
| `ListBox` | Displays all current tasks; user selects an item to delete |
| `Button` (Add) | Adds the TextBox content as a new item to the ListBox |
| `Button` (Delete) | Removes the currently selected item from the ListBox |
| `Button` (Clear All) | Empties the entire list after confirmation |
| `Label` | Shows the current task count |

**Pattern — keeping UI in sync with data:**
Always maintain a backing `List<string>` as the source of truth, and call `RefreshList()` to rebuild the `ListBox` from it after every change. This is the foundation of MVC/MVVM patterns used in modern frameworks.

---

## 3. Project Setup

Create a new **Windows Forms App (.NET Framework)** project named `ToDoListApp`.
Set `Form1.Text` = `"To-Do List"`, `Size` = `420, 450`, `StartPosition` = `CenterScreen`.

---

## 4. Implementation

### Step 1 — Design the Form

| Control | Name | Text / Properties |
|---|---|---|
| `Label` | — | `Add a task:` |
| `TextBox` | `txtTask` | `Size 260,23` |
| `Button` | `btnAdd` | `Add Task` |
| `ListBox` | `lstTasks` | `Size 360,200`, `IntegralHeight False` |
| `Button` | `btnDelete` | `Delete Selected` |
| `Button` | `btnMoveUp` | `↑ Move Up` |
| `Button` | `btnMoveDown` | `↓ Move Down` |
| `Button` | `btnClearAll` | `Clear All` |
| `Label` | `lblCount` | `Tasks: 0` |

### Step 2 — `Form1.cs`

```csharp
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ToDoListApp
{
    public partial class Form1 : Form
    {
        // Backing collection — the real source of truth
        private List<string> _tasks = new List<string>();

        public Form1()
        {
            InitializeComponent();
            RefreshList();
        }

        // ── Rebuild the ListBox from the _tasks list ──
        private void RefreshList()
        {
            lstTasks.Items.Clear();
            foreach (string task in _tasks)
                lstTasks.Items.Add(task);

            lblCount.Text = $"Tasks: {_tasks.Count}";

            // Enable/disable buttons based on state
            btnDelete.Enabled    = _tasks.Count > 0;
            btnMoveUp.Enabled    = _tasks.Count > 1;
            btnMoveDown.Enabled  = _tasks.Count > 1;
            btnClearAll.Enabled  = _tasks.Count > 0;
        }

        // ── Add task ──
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string task = txtTask.Text.Trim();

            if (string.IsNullOrWhiteSpace(task))
            {
                MessageBox.Show("Please type a task first.", "Empty Task",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTask.Focus();
                return;
            }

            _tasks.Add(task);
            txtTask.Clear();
            txtTask.Focus();
            RefreshList();

            // Select the newly added item
            lstTasks.SelectedIndex = lstTasks.Items.Count - 1;
        }

        // ── Delete selected task ──
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = lstTasks.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Please select a task to delete.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string task = _tasks[index];
            DialogResult confirm = MessageBox.Show(
                $"Delete task:\n\"{task}\"?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                _tasks.RemoveAt(index);
                RefreshList();

                // Keep selection on the nearest remaining item
                if (_tasks.Count > 0)
                    lstTasks.SelectedIndex = Math.Min(index, _tasks.Count - 1);
            }
        }

        // ── Move selected task up ──
        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            int index = lstTasks.SelectedIndex;
            if (index <= 0) return;

            string temp      = _tasks[index - 1];
            _tasks[index - 1] = _tasks[index];
            _tasks[index]     = temp;

            RefreshList();
            lstTasks.SelectedIndex = index - 1;
        }

        // ── Move selected task down ──
        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            int index = lstTasks.SelectedIndex;
            if (index == -1 || index >= _tasks.Count - 1) return;

            string temp      = _tasks[index + 1];
            _tasks[index + 1] = _tasks[index];
            _tasks[index]     = temp;

            RefreshList();
            lstTasks.SelectedIndex = index + 1;
        }

        // ── Clear all tasks ──
        private void btnClearAll_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show(
                "Delete ALL tasks?",
                "Confirm Clear",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                _tasks.Clear();
                RefreshList();
            }
        }

        // ── Press Enter in TextBox to add task quickly ──
        private void txtTask_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                btnAdd_Click(sender, e);
        }
    }
}
```

---

## 5. Expected Output

**On launch:**
```
Add a task: [_______________________] [ Add Task ]

┌──────────────────────────────────────┐
│  (empty list)                        │
└──────────────────────────────────────┘
[ Delete Selected ] [ ↑ Move Up ] [ ↓ Move Down ] [ Clear All ]
Tasks: 0
```

**After adding three tasks:**
```
┌──────────────────────────────────────┐
│  Complete lab record                 │
│  Study OOP concepts                  │
│▶ Submit assignment                   │  ← selected item
└──────────────────────────────────────┘
Tasks: 3
```

**After clicking "Delete Selected" on "Study OOP concepts":**
```
Confirm Delete dialog: Delete task: "Study OOP concepts"? [Yes] [No]
→ Yes clicked →
┌──────────────────────────────────────┐
│  Complete lab record                 │
│▶ Submit assignment                   │
└──────────────────────────────────────┘
Tasks: 2
```

---

## 6. Viva / Discussion Questions

1. What is `ListBox.SelectedIndex`? What value does it hold when nothing is selected?
2. Why is it good practice to maintain a separate `List<string>` rather than reading directly from `lstTasks.Items`?
3. What does `MessageBox.Show(...)` return, and how is `DialogResult` used?
4. What does `Math.Min(index, _tasks.Count - 1)` ensure after deleting an item?
5. What is the difference between `lstTasks.Items.Clear()` and `_tasks.Clear()`?
6. How would you save the task list to a file so it persists after closing the app?
7. How would you mark a task as "done" without deleting it (e.g., prefix with `[✓]`)?
