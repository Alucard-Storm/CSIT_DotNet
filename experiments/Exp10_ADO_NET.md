# Experiment 10 — Database Operations with ADO.NET

**Subject:** CSIT-406 .NET Framework Lab
**RGPV, Bhopal**

---

## Aim

Connect to a database, execute queries, and manipulate data using ADO.NET.

---

## Theory

**ADO.NET** (ActiveX Data Objects .NET) is the data access layer of the .NET Framework, providing classes to interact with relational databases.

### Core Components

| Component | Description |
|---|---|
| `SqlConnection` | Opens a connection to SQL Server |
| `SqlCommand` | Executes SQL queries/stored procedures |
| `SqlDataReader` | Reads data row-by-row (forward-only, fast) |
| `SqlDataAdapter` | Fills a `DataSet` from a query |
| `DataSet` | In-memory representation of tables |
| `DataTable` | Single in-memory table |

### Connection Modes

| Mode | Class | Use Case |
|---|---|---|
| Connected | `SqlDataReader` | Real-time, streaming reads |
| Disconnected | `DataSet` + `SqlDataAdapter` | Offline processing, binding |

> Real-world analogy: A student result portal — ADO.NET connects to the SQL Server, fetches marks for a roll number, and displays them. The result page is generated using `SqlDataReader` in connected mode.

---

## Setup

```sql
-- Run in SQL Server Management Studio (SSMS)
CREATE DATABASE CollegeDB;
USE CollegeDB;

CREATE TABLE Students (
    RollNo INT PRIMARY KEY,
    Name NVARCHAR(100),
    Branch NVARCHAR(50),
    CGPA DECIMAL(4,2)
);

INSERT INTO Students VALUES (101, 'Akshay', 'CSIT', 8.9);
INSERT INTO Students VALUES (102, 'Priya',  'CSE',  9.1);
INSERT INTO Students VALUES (103, 'Rahul',  'IT',   7.5);
```

---

## Code

### Part A — Connection and SELECT (Connected Mode)

```csharp
using System;
using System.Data.SqlClient;

namespace ADOConnected
{
    class Program
    {
        static string connStr = "Server=localhost;Database=CollegeDB;Integrated Security=True;";

        static void Main()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                Console.WriteLine("Connection State: " + conn.State);

                string query = "SELECT * FROM Students";
                SqlCommand cmd = new SqlCommand(query, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\n{0,-10} {1,-20} {2,-10} {3}", "RollNo", "Name", "Branch", "CGPA");
                    Console.WriteLine(new string('-', 50));

                    while (reader.Read())
                    {
                        Console.WriteLine("{0,-10} {1,-20} {2,-10} {3}",
                            reader["RollNo"],
                            reader["Name"],
                            reader["Branch"],
                            reader["CGPA"]);
                    }
                }
            }
        }
    }
}
```

### Part B — INSERT, UPDATE, DELETE

```csharp
using System;
using System.Data.SqlClient;

namespace ADOManipulation
{
    class Program
    {
        static string connStr = "Server=localhost;Database=CollegeDB;Integrated Security=True;";

        static void ExecuteNonQuery(string sql)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine($"Rows affected: {rows}");
            }
        }

        static void Main()
        {
            // INSERT
            Console.WriteLine("--- INSERT ---");
            ExecuteNonQuery("INSERT INTO Students VALUES (104, 'Sara', 'EC', 8.2)");

            // UPDATE
            Console.WriteLine("--- UPDATE ---");
            ExecuteNonQuery("UPDATE Students SET CGPA = 9.5 WHERE RollNo = 101");

            // DELETE
            Console.WriteLine("--- DELETE ---");
            ExecuteNonQuery("DELETE FROM Students WHERE RollNo = 103");

            Console.WriteLine("\nOperations complete.");
        }
    }
}
```

### Part C — Parameterized Queries (Prevents SQL Injection)

```csharp
using System;
using System.Data.SqlClient;

namespace ADOParameterized
{
    class Program
    {
        static string connStr = "Server=localhost;Database=CollegeDB;Integrated Security=True;";

        static void Main()
        {
            Console.Write("Enter Roll No to search: ");
            int rollNo = int.Parse(Console.ReadLine());

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Parameterized query — safe against SQL injection
                string query = "SELECT * FROM Students WHERE RollNo = @RollNo";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@RollNo", rollNo);  // Safe binding

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine("\nStudent Found:");
                        Console.WriteLine("Name   : " + reader["Name"]);
                        Console.WriteLine("Branch : " + reader["Branch"]);
                        Console.WriteLine("CGPA   : " + reader["CGPA"]);
                    }
                    else
                        Console.WriteLine("No student found with Roll No: " + rollNo);
                }
            }
        }
    }
}
```

### Part D — Disconnected Mode (DataSet + DataAdapter)

```csharp
using System;
using System.Data;
using System.Data.SqlClient;

namespace ADODisconnected
{
    class Program
    {
        static void Main()
        {
            string connStr = "Server=localhost;Database=CollegeDB;Integrated Security=True;";
            string query = "SELECT * FROM Students";

            SqlDataAdapter adapter = new SqlDataAdapter(query, connStr);
            DataSet ds = new DataSet();

            // Fill DataSet — connection opens and closes automatically
            adapter.Fill(ds, "Students");

            DataTable table = ds.Tables["Students"];

            Console.WriteLine("Total Records: " + table.Rows.Count);
            Console.WriteLine("\n{0,-10} {1,-20} {2}", "RollNo", "Name", "CGPA");
            Console.WriteLine(new string('-', 40));

            foreach (DataRow row in table.Rows)
                Console.WriteLine("{0,-10} {1,-20} {2}", row["RollNo"], row["Name"], row["CGPA"]);
        }
    }
}
```

---

## Expected Output

**Part A:**
```
Connection State: Open

RollNo     Name                 Branch     CGPA
--------------------------------------------------
101        Akshay               CSIT       8.90
102        Priya                CSE        9.10
103        Rahul                IT         7.50
```

**Part B:**
```
--- INSERT ---
Rows affected: 1
--- UPDATE ---
Rows affected: 1
--- DELETE ---
Rows affected: 1
```

**Part C:**
```
Enter Roll No to search: 101
Student Found:
Name   : Akshay
Branch : CSIT
CGPA   : 9.50
```

---

## Viva Questions

1. What is ADO.NET? Name its two modes of data access.
2. What is the difference between `SqlDataReader` and `DataSet`?
3. What does `ExecuteNonQuery()` return?
4. What is a parameterized query and why is it important?
5. What is the role of `SqlDataAdapter` in disconnected mode?

---

[Back to Index](../README.md)
