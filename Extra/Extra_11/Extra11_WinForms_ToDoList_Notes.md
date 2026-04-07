# Extra Experiment 11 — Windows Forms: To-Do List App | Notes

---

## `ListBox` — Core Operations

```csharp
// Add items
lstTasks.Items.Add("Buy groceries");
lstTasks.Items.Insert(0, "Urgent task");     // insert at position 0

// Remove items
lstTasks.Items.Remove("Buy groceries");      // by value
lstTasks.Items.RemoveAt(2);                  // by index
lstTasks.Items.Clear();                      // remove all

// Read items
string item   = (string)lstTasks.Items[0];  // cast required
int count     = lstTasks.Items.Count;

// Selection
int index     = lstTasks.SelectedIndex;     // -1 if nothing selected
string chosen = (string)lstTasks.SelectedItem;
lstTasks.SelectedIndex = 0;                  // select first item programmatically
```

---

## Backing Collection Pattern

Never use `lstTasks.Items` as your data store. Keep a separate list and rebuild the ListBox from it:

```csharp
private List<string> _tasks = new List<string>();

private void RefreshList()
{
    lstTasks.Items.Clear();
    foreach (string t in _tasks)
        lstTasks.Items.Add(t);
    lblCount.Text = "Tasks: " + _tasks.Count;
}
```

**Why?** `ListBox.Items` is a UI collection — it has no search, sort, or save functionality. Your `List<string>` can be sorted, searched, serialized to a file, etc.

---

## Swapping Two Items (Move Up / Down)

```csharp
// Standard swap using a temporary variable
int i = lstTasks.SelectedIndex;

string temp   = _tasks[i - 1];
_tasks[i - 1] = _tasks[i];
_tasks[i]     = temp;

RefreshList();
lstTasks.SelectedIndex = i - 1;  // keep the moved item selected
```

---

## Confirming Destructive Actions with `MessageBox`

```csharp
DialogResult answer = MessageBox.Show(
    "Delete all tasks?",           // message
    "Confirm",                     // title bar
    MessageBoxButtons.YesNo,       // button set
    MessageBoxIcon.Warning         // icon
);

if (answer == DialogResult.Yes)
{
    _tasks.Clear();
    RefreshList();
}
```

**`MessageBoxButtons` options:** `OK`, `OKCancel`, `YesNo`, `YesNoCancel`, `RetryCancel`
**`MessageBoxIcon` options:** `Information`, `Warning`, `Error`, `Question`

---

## Enabling / Disabling Buttons Based on State

```csharp
private void RefreshList()
{
    // ...rebuild ListBox...

    // Contextual enable/disable
    btnDelete.Enabled   = _tasks.Count > 0;
    btnClearAll.Enabled = _tasks.Count > 0;
    btnMoveUp.Enabled   = lstTasks.SelectedIndex > 0;
    btnMoveDown.Enabled = lstTasks.SelectedIndex < _tasks.Count - 1
                          && lstTasks.SelectedIndex != -1;
}
```

---

## Bonus: Saving the List to a File

```csharp
// Save
File.WriteAllLines("tasks.txt", _tasks);

// Load on startup
if (File.Exists("tasks.txt"))
    _tasks = new List<string>(File.ReadAllLines("tasks.txt"));
```

Call save in `btnAdd_Click`, `btnDelete_Click`, and `btnClearAll_Click`. Call load in the `Form1` constructor after `RefreshList()`.

---

## Common Mistakes

```csharp
// 1. Modifying ListBox.Items directly (bypasses backing list)
lstTasks.Items.Add("task");     // backing _tasks list is now out of sync!
// Fix: always add to _tasks first, then call RefreshList()

// 2. Deleting without checking selection
_tasks.RemoveAt(lstTasks.SelectedIndex);   // crashes if SelectedIndex == -1
// Fix: if (lstTasks.SelectedIndex == -1) return;

// 3. Index out of range after deletion
lstTasks.SelectedIndex = index;   // may be >= Count after last item deleted
// Fix: Math.Min(index, _tasks.Count - 1)
```
