# Experiment 10 — Connecting to a Database using ADO.NET

**Subject:** CSIT-406 .NET Framework Lab
**Location:** RGPV, Bhopal

---

## 1. Aim

To write C# code that connects to a Microsoft SQL Server database, adds a record to a table, and reads data back out using ADO.NET.

## 2. Theory

**ADO.NET** is the data access technology in the .NET Framework. It provides all the necessary C# classes you need to securely talk to a database like SQL Server. 

### Important Classes in ADO.NET

| ADO.NET Class | Explanation |
|---|---|
| **`SqlConnection`** | Serves as the phone line between your C# code and the SQL Server database. |
| **`SqlCommand`** | Represents the actual SQL instruction (like `SELECT` or `INSERT`) you want the database to run. |
| **`SqlDataReader`** | Reads the data sent back from the database one row at a time. It is very fast, but you must keep the connection open while reading. |
| **`SqlParameter`** | Used to safely pass user text into a SQL query. It prevents hackers from breaking your database (SQL Injection). |

*Instructional Example:* Think of a student portal login page. When you type your roll number and password, the C# code uses a `SqlConnection` to reach the database, and a `SqlCommand` to execute a `SELECT` query to check if your password matches.

---

## 3. Database Setup (SQL Server)

Before writing the C# code, open **SQL Server Management Studio (SSMS)** and run this SQL query to create a test table.

```sql
CREATE DATABASE CollegeDB;
GO

USE CollegeDB;
GO

CREATE TABLE Students (
    RollNo INT PRIMARY KEY,
    FullName NVARCHAR(100)
);

-- Add some sample generic students
INSERT INTO Students (RollNo, FullName) VALUES (101, 'Akshay');
INSERT INTO Students (RollNo, FullName) VALUES (102, 'Pawan');
GO
```

---

## 4. Implementation Code

### Part A: Secure Data Insertion

This code demonstrates how to safely insert a new student record into the database.

```csharp
// File: InsertStudent.cs
using System;
using System.Data.SqlClient;

namespace DatabaseLab
{
    class Program
    {
        static void Main(string[] args)
        {
            // Enter the correct server settings here
            string connectionDetails = "Server=localhost;Database=CollegeDB;Integrated Security=True;";

            using (SqlConnection databaseConnection = new SqlConnection(connectionDetails))
            {
                // Open the connection securely
                databaseConnection.Open();
                
                // Write the SQL query using @ signs for variables (Parameters)
                string sqlQuery = "INSERT INTO Students (RollNo, FullName) VALUES (@roll, @name)";
                
                using (SqlCommand myCommand = new SqlCommand(sqlQuery, databaseConnection))
                {
                    // Securely assign the actual values
                    myCommand.Parameters.AddWithValue("@roll", 103);
                    myCommand.Parameters.AddWithValue("@name", "Diksha");

                    // Execute the query
                    int rowsAdded = myCommand.ExecuteNonQuery();
                    Console.WriteLine("Success! Students added: " + rowsAdded);
                }
            }
        }
    }
}
```

### Part B: Reading Data (SELECT)

This code demonstrates how to retrieve all records from the database and print them to the screen.

```csharp
// File: ReadStudents.cs
using System;
using System.Data.SqlClient;

namespace DatabaseLab
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionDetails = "Server=localhost;Database=CollegeDB;Integrated Security=True;";

            using (SqlConnection databaseConnection = new SqlConnection(connectionDetails))
            {
                databaseConnection.Open();
                
                string sqlQuery = "SELECT RollNo, FullName FROM Students";
                SqlCommand readCommand = new SqlCommand(sqlQuery, databaseConnection);

                // Use the DataReader to go through the results row by row
                using (SqlDataReader dataReader = readCommand.ExecuteReader())
                {
                    Console.WriteLine("Roll Number   Name");
                    Console.WriteLine("-------------------------");

                    // The Read() method moves to the next row until it runs out of data
                    while (dataReader.Read())
                    {
                        Console.WriteLine(dataReader["RollNo"] + "           " + dataReader["FullName"]);
                    }
                }
            }
        }
    }
}
```

---

## 5. Expected Output

**Output - Part A:**
```text
Success! Students added: 1
```

**Output - Part B:**
```text
Roll Number   Name
-------------------------
101           Akshay
102           Pawan
103           Diksha
```

---

## 6. Viva / Discussion Questions

1. **Definitions:** What does ADO.NET stand for?
2. **Reading Data:** Why does compiling data into a `SqlDataReader` require the `SqlConnection` to stay completely open?
3. **Execution Methods:** What is the difference between `ExecuteReader()` and `ExecuteNonQuery()`?
4. **Database Security:** What is "SQL Injection" and how does using `SqlCommand.Parameters` prevent it?
5. **Connection Strings:** What does `Integrated Security=True` mean in the connection string?

---

[Back to Main Index](../README.md)
