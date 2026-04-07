# Extra Experiment 12 — Windows Forms: Student Record Form

**Subject:** CSIT-406 .NET Framework Lab
**Location:** RGPV, Bhopal

---

## 1. Aim

To build a Student Record Manager using Windows Forms that demonstrates `ComboBox`, `RadioButton`, `NumericUpDown`, `DataGridView`, and in-memory CRUD (Create, Read, Update, Delete) operations — bringing together all the WinForms controls practised in previous experiments.

## 2. Theory

This is a capstone WinForms experiment combining multiple controls and a structured data class.

| Control | Purpose |
|---|---|
| `TextBox` | Student name and roll number input |
| `NumericUpDown` | Age input — prevents non-numeric entry natively |
| `ComboBox` | Drop-down for selecting the course/branch |
| `RadioButton` | Mutually exclusive selection (gender) |
| `DataGridView` | Table-style display of all student records |
| `Button` | Add, Update, Delete, Clear actions |

**`DataGridView`** is the most powerful display control in WinForms. It can show a `List<T>` or a `DataTable` and supports sorting, selection, and editing. Binding it to a `BindingList<T>` makes the grid auto-refresh whenever the data changes.

**CRUD pattern:**
- **Create** — fill the form → Click Add
- **Read** — all records shown in the DataGridView
- **Update** — click a row → fields populate → edit → Click Update
- **Delete** — select a row → Click Delete

---

## 3. Project Setup

Create a new **Windows Forms App (.NET Framework)** project named `StudentRecordApp`.
Set `Form1.Text` = `"Student Record Manager"`, `Size` = `720, 560`, `StartPosition` = `CenterScreen`.

---

## 4. Implementation

### Step 1 — The `Student` Data Class

Add a new class file: **Project → Add → Class → `Student.cs`**

```csharp
namespace StudentRecordApp
{
    public class Student
    {
        public int    RollNo  { get; set; }
        public string Name    { get; set; }
        public int    Age     { get; set; }
        public string Course  { get; set; }
        public string Gender  { get; set; }

        public override string ToString()
        {
            return $"Roll {RollNo}: {Name}";
        }
    }
}
```

### Step 2 — Design `Form1`

**Top panel (input area):**

| Control | Name | Text / Properties |
|---|---|---|
| `Label` | — | `Roll No:` |
| `NumericUpDown` | `nudRoll` | `Min 1, Max 9999` |
| `Label` | — | `Name:` |
| `TextBox` | `txtName` | `Size 180,23` |
| `Label` | — | `Age:` |
| `NumericUpDown` | `nudAge` | `Min 15, Max 60` |
| `Label` | — | `Course:` |
| `ComboBox` | `cmbCourse` | `DropDownStyle: DropDownList` |
| `Label` | — | `Gender:` |
| `RadioButton` | `rbMale` | `Male`, `Checked = true` |
| `RadioButton` | `rbFemale` | `Female` |
| `Button` | `btnAdd` | `Add Record` |
| `Button` | `btnUpdate` | `Update` |
| `Button` | `btnDelete` | `Delete` |
| `Button` | `btnClear` | `Clear` |

**Bottom panel (display area):**
| Control | Name | Properties |
|---|---|---|
| `DataGridView` | `dgvStudents` | `Dock = Fill` (or fixed size), `ReadOnly = true`, `SelectionMode = FullRowSelect`, `MultiSelect = false` |
| `Label` | `lblStatus` | *(empty)* |

### Step 3 — `Form1.cs`

```csharp
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace StudentRecordApp
{
    public partial class Form1 : Form
    {
        // BindingList auto-updates DataGridView when items change
        private BindingList<Student> _students = new BindingList<Student>();
        private int _editingIndex = -1;   // -1 means "not editing any record"

        public Form1()
        {
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            // Populate course dropdown
            cmbCourse.Items.AddRange(new string[]
            {
                "Computer Science (CS)",
                "Information Technology (IT)",
                "Electronics (EC)",
                "Mechanical (ME)",
                "Civil (CE)"
            });
            cmbCourse.SelectedIndex = 0;

            // Bind DataGridView to the list
            dgvStudents.DataSource = _students;

            // Customise column headers
            dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Pre-load sample data
            _students.Add(new Student { RollNo = 101, Name = "Akshay Sagar",  Age = 20, Course = "Computer Science (CS)",      Gender = "Male"   });
            _students.Add(new Student { RollNo = 102, Name = "Diksha Pawar",  Age = 21, Course = "Information Technology (IT)", Gender = "Female" });
            _students.Add(new Student { RollNo = 103, Name = "Pawan Tiwari",  Age = 20, Course = "Electronics (EC)",             Gender = "Male"   });
            _students.Add(new Student { RollNo = 104, Name = "Divya Khade",   Age = 22, Course = "Computer Science (CS)",        Gender = "Female" });
            _students.Add(new Student { RollNo = 105, Name = "Deepak Gwale",  Age = 21, Course = "Mechanical (ME)",              Gender = "Male"   });

            UpdateStatus();
            btnUpdate.Enabled = false;
        }

        // ── Validate and collect input from the form ──
        private bool TryGetInput(out Student s)
        {
            s = null;

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                ShowStatus("Name cannot be empty.", error: true);
                txtName.Focus();
                return false;
            }

            s = new Student
            {
                RollNo = (int)nudRoll.Value,
                Name   = txtName.Text.Trim(),
                Age    = (int)nudAge.Value,
                Course = cmbCourse.SelectedItem?.ToString(),
                Gender = rbMale.Checked ? "Male" : "Female"
            };
            return true;
        }

        // ── ADD ──
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!TryGetInput(out Student s)) return;

            // Check for duplicate roll number
            foreach (Student existing in _students)
            {
                if (existing.RollNo == s.RollNo)
                {
                    ShowStatus($"Roll No {s.RollNo} already exists.", error: true);
                    return;
                }
            }

            _students.Add(s);
            ShowStatus($"Record added: {s.Name} (Roll {s.RollNo})");
            ClearForm();
        }

        // ── Click row → populate form for editing ──
        private void dgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _students.Count) return;

            Student s = _students[e.RowIndex];
            _editingIndex = e.RowIndex;

            nudRoll.Value       = s.RollNo;
            txtName.Text        = s.Name;
            nudAge.Value        = s.Age;
            cmbCourse.Text      = s.Course;
            rbMale.Checked      = s.Gender == "Male";
            rbFemale.Checked    = s.Gender == "Female";

            btnUpdate.Enabled   = true;
            btnAdd.Enabled      = false;
            ShowStatus($"Editing: {s.Name}");
        }

        // ── UPDATE ──
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_editingIndex == -1 || !TryGetInput(out Student updated)) return;

            _students[_editingIndex] = updated;
            ShowStatus($"Record updated: {updated.Name}");
            ClearForm();
        }

        // ── DELETE ──
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0)
            {
                ShowStatus("Select a row to delete.", error: true);
                return;
            }

            int index = dgvStudents.SelectedRows[0].Index;
            Student s = _students[index];

            DialogResult confirm = MessageBox.Show(
                $"Delete record for {s.Name} (Roll {s.RollNo})?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                _students.RemoveAt(index);
                ShowStatus($"Deleted: {s.Name}");
                ClearForm();
            }
        }

        // ── CLEAR ──
        private void btnClear_Click(object sender, EventArgs e) => ClearForm();

        private void ClearForm()
        {
            nudRoll.Value       = nudRoll.Minimum;
            txtName.Clear();
            nudAge.Value        = 18;
            cmbCourse.SelectedIndex = 0;
            rbMale.Checked      = true;
            _editingIndex       = -1;
            btnUpdate.Enabled   = false;
            btnAdd.Enabled      = true;
            txtName.Focus();
            UpdateStatus();
        }

        private void ShowStatus(string message, bool error = false)
        {
            lblStatus.Text      = message;
            lblStatus.ForeColor = error
                ? System.Drawing.Color.Red
                : System.Drawing.Color.DarkGreen;
        }

        private void UpdateStatus()
        {
            lblStatus.Text      = $"Total records: {_students.Count}";
            lblStatus.ForeColor = System.Drawing.Color.DimGray;
        }
    }
}
```

---

## 5. Expected Output

**On launch — DataGridView shows 5 pre-loaded records:**
```
RollNo | Name           | Age | Course                      | Gender
────────────────────────────────────────────────────────────────────
101    | Akshay Sagar   | 20  | Computer Science (CS)        | Male
102    | Diksha Pawar   | 21  | Information Technology (IT)  | Female
103    | Pawan Tiwari   | 20  | Electronics (EC)             | Male
104    | Divya Khade    | 22  | Computer Science (CS)        | Female
105    | Deepak Gwale   | 21  | Mechanical (ME)              | Male

Total records: 5
```

**After clicking row 102 and modifying Name to "Diksha P. Pawar" → Update:**
```
Record updated: Diksha P. Pawar
(Row 102 reflects new name in the grid instantly)
```

**After deleting row 103:**
```
Confirm Delete: Delete record for Pawan Tiwari (Roll 103)? [Yes] [No]
→ Yes → row disappears
Deleted: Pawan Tiwari
Total records: 4
```

---

## 6. Viva / Discussion Questions

1. What is a `BindingList<T>`? How does it differ from `List<T>` in the context of WinForms?
2. What does `dgvStudents.DataSource = _students` do?
3. What is `DataGridViewAutoSizeColumnsMode.Fill`?
4. What is CRUD? Give one example of each operation from this experiment.
5. What is the difference between `DropDown` and `DropDownList` for a `ComboBox`?
6. How do `RadioButton` controls enforce mutual exclusion (only one selected at a time)?
7. What does `NumericUpDown` prevent that a regular `TextBox` does not?
8. How would you export the student records to a CSV file?
