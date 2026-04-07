# Extra Experiment 12 — Windows Forms: Student Record Form | Notes

---

## `DataGridView` — Binding to a List

```csharp
// BindingList<T> — notifies the grid automatically when items change
private BindingList<Student> _students = new BindingList<Student>();

// Bind once in constructor or Form_Load
dgvStudents.DataSource = _students;

// Now any change to _students auto-refreshes the grid:
_students.Add(new Student { ... });     // row appears instantly
_students.RemoveAt(2);                 // row disappears instantly
_students[0] = updatedStudent;         // row updates instantly
```

**Column headers** are auto-generated from the property names of `Student`. To rename them set `dgvStudents.Columns["RollNo"].HeaderText = "Roll No"` after binding.

---

## `DataGridView` — Reading the Selected Row

```csharp
// Ensure a row is selected
if (dgvStudents.SelectedRows.Count == 0) return;

int index     = dgvStudents.SelectedRows[0].Index;
Student s     = _students[index];   // get the object from the backing list

// Or from the cell click event:
private void dgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
{
    if (e.RowIndex < 0) return;    // guard: header row has RowIndex = -1
    Student s = _students[e.RowIndex];
}
```

---

## `ComboBox` — Key Properties

```csharp
cmbCourse.Items.Add("Computer Science");
cmbCourse.Items.AddRange(new string[] { "IT", "EC", "ME" });

cmbCourse.SelectedIndex = 0;              // select first item
string chosen = cmbCourse.SelectedItem?.ToString();

// DropDownStyle:
// DropDown     — user can type a custom value (free text + list)
// DropDownList — user can ONLY choose from the list (no free text)
```

---

## `NumericUpDown` — Replacing TextBox for Numbers

```csharp
nudAge.Minimum   = 15;
nudAge.Maximum   = 60;
nudAge.Value     = 18;           // default
nudAge.Increment = 1;            // step size per click

int age = (int)nudAge.Value;     // read the current value — cast to int
```

**Advantage over TextBox:** user cannot type letters — no `TryParse` needed.

---

## `RadioButton` — Mutual Exclusion

RadioButtons inside the **same container** (Panel, GroupBox, Form) are automatically mutually exclusive — checking one unchecks all others.

```csharp
// Reading which one is selected
string gender = rbMale.Checked ? "Male" : "Female";

// Setting programmatically
rbFemale.Checked = true;   // also unchecks rbMale automatically
```

---

## CRUD Summary

```
Create  → TryGetInput() + _students.Add(s)
Read    → DataGridView shows all rows via DataSource binding
Update  → _editingIndex stores which row → _students[_editingIndex] = updated
Delete  → _students.RemoveAt(index)
```

---

## Bonus: Export to CSV

```csharp
private void ExportToCsv()
{
    using (System.IO.StreamWriter sw = new System.IO.StreamWriter("students.csv"))
    {
        sw.WriteLine("RollNo,Name,Age,Course,Gender");   // header

        foreach (Student s in _students)
            sw.WriteLine($"{s.RollNo},{s.Name},{s.Age},{s.Course},{s.Gender}");
    }
    MessageBox.Show("Exported to students.csv", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
}
```

---

## Common Mistakes

```csharp
// 1. Modifying _students while iterating over it
foreach (Student s in _students)
    _students.Remove(s);    // InvalidOperationException — modify after the loop

// 2. Using List<T> instead of BindingList<T> — grid does not auto-update
List<Student> list = new List<Student>();
dgvStudents.DataSource = list;
list.Add(new Student());    // grid does NOT refresh
// Fix: use BindingList<Student> or call dgvStudents.Refresh() manually

// 3. Reading SelectedRows[0] without checking Count first
int index = dgvStudents.SelectedRows[0].Index;   // crash if nothing selected
// Fix: if (dgvStudents.SelectedRows.Count == 0) return;
```
