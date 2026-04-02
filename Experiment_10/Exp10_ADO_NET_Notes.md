# Experiment 10 — Database Access using ADO.NET | Notes

---

## What is ADO.NET?

ADO.NET is the set of C# classes that allow your program to talk to a database. Think of it as the bridge between your C# code and your SQL Server.

```
Your C# Code
     |
     | SqlConnection (opens the bridge)
     v
SQL Server Database
     |
     | Results come back through SqlDataReader
     v
Your C# variables
```

---

## Step-by-Step: Reading from the Database

### Step 1 — Write the Connection String

This is the "address" of your database. Change `localhost` and `CollegeDB` to match your setup.

```csharp
string conn = "Server=localhost;Database=CollegeDB;Integrated Security=True;";
```

### Step 2 — Open the Connection

```csharp
SqlConnection con = new SqlConnection(conn);
con.Open();
```

### Step 3 — Write a Command (SQL Query)

```csharp
SqlCommand cmd = new SqlCommand("SELECT RollNo, FullName FROM Students", con);
```

### Step 4 — Read the Results Row by Row

```csharp
SqlDataReader reader = cmd.ExecuteReader();

while (reader.Read())   // .Read() moves to the next row; returns false when done
{
    Console.WriteLine(reader["RollNo"] + "  " + reader["FullName"]);
}
```

**Output:**
```
101  Akshay
102  Priya
103  Rahul
```

---

## Inserting Data Safely (Parameterized Query)

Never put user input directly into a SQL string. This leads to **SQL Injection** — a security attack where a hacker types SQL code into your text box to manipulate the database.

**Unsafe (NEVER do this):**
```csharp
// A hacker could type: ' OR 1=1; DROP TABLE Students --
cmd = new SqlCommand("SELECT * FROM Students WHERE Name = '" + userInput + "'", con);
```

**Safe (Always do this):**
```csharp
cmd = new SqlCommand("INSERT INTO Students VALUES (@roll, @name)", con);
cmd.Parameters.AddWithValue("@roll", 104);
cmd.Parameters.AddWithValue("@name", "Sara");

int rows = cmd.ExecuteNonQuery();
Console.WriteLine("Rows inserted: " + rows);
// Output: Rows inserted: 1
```

---

## ExecuteReader vs ExecuteNonQuery

| Method | Use it for | Returns |
|---|---|---|
| `ExecuteReader()` | SELECT queries (reading rows) | A `SqlDataReader` object |
| `ExecuteNonQuery()` | INSERT, UPDATE, DELETE queries | The number of rows affected |

---

## Key Points to Remember

| Term | Meaning |
|---|---|
| `SqlConnection` | Opens the bridge to the database |
| `SqlCommand` | The SQL instruction to run |
| `SqlDataReader` | Reads results one row at a time |
| `AddWithValue()` | Safely injects variables into the query and prevents SQL Injection |
| `Integrated Security=True` | Uses your current Windows login to authenticate with SQL Server (no username/password needed) |
