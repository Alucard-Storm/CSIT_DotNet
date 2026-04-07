# Extra Experiment 07 — File I/O

**Subject:** CSIT-406 .NET Framework Lab
**Location:** RGPV, Bhopal

---

## 1. Aim

To perform file input/output operations in C# — writing data to text files, reading data back, appending lines, checking file existence, and managing files and directories using the `System.IO` namespace.

## 2. Theory

The `System.IO` namespace provides classes for interacting with the file system:

| Class | Purpose |
|---|---|
| `File` | Static helper for quick one-line read/write/copy/delete operations |
| `StreamWriter` | Writes text data to a file stream (supports large files efficiently) |
| `StreamReader` | Reads text data from a file stream line-by-line |
| `FileInfo` | Instance-based file operations; also gives metadata (size, dates) |
| `DirectoryInfo` | Creates, deletes, and enumerates directories |
| `Path` | Utility methods for building and parsing file paths safely |

**`using` statement** ensures that file handles are automatically closed and memory is released even if an exception occurs — this is the correct way to work with streams.

| Mode | Meaning |
|---|---|
| `File.WriteAllText` | Creates the file (or overwrites it) |
| `File.AppendAllText` | Adds to the end of an existing file without overwriting |
| `File.ReadAllText` | Reads the entire file as a single string |
| `File.ReadAllLines` | Reads every line into a `string[]` array |

---

## 3. Implementation Code

### Part A: Write and Read a Text File

```csharp
using System;
using System.IO;

namespace FileWriteReadDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "students.txt";

            // --- WRITE ---
            // StreamWriter creates the file and writes to it
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Roll No | Name          | Marks");
                writer.WriteLine("--------|---------------|------");
                writer.WriteLine("101     | Akshay Sagar  | 85");
                writer.WriteLine("102     | Diksha Pawar  | 91");
                writer.WriteLine("103     | Pawan Tiwari  | 78");
            }
            // File is automatically closed when the using block exits

            Console.WriteLine("File written: " + Path.GetFullPath(filePath));

            // --- READ ALL ---
            Console.WriteLine("\n--- File Contents ---");
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
    }
}
```

### Part B: Append to a File and Use `File` Static Methods

```csharp
using System;
using System.IO;

namespace FileAppendDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "log.txt";

            // WriteAllText: create or overwrite
            File.WriteAllText(filePath, $"=== Log started at {DateTime.Now} ==={Environment.NewLine}");

            // AppendAllText: add lines without erasing existing content
            for (int i = 1; i <= 3; i++)
            {
                string entry = $"[{DateTime.Now:HH:mm:ss}] Event {i}: Processed record #{i * 10}";
                File.AppendAllText(filePath, entry + Environment.NewLine);
                System.Threading.Thread.Sleep(1000);   // simulate time passing
            }

            File.AppendAllText(filePath, $"=== Log closed at {DateTime.Now} ==={Environment.NewLine}");

            // ReadAllLines: read every line into an array
            string[] lines = File.ReadAllLines(filePath);
            Console.WriteLine($"Log has {lines.Length} lines:\n");

            foreach (string line in lines)
                Console.WriteLine(line);
        }
    }
}
```

### Part C: File Existence, Copy, Delete, and `FileInfo`

```csharp
using System;
using System.IO;

namespace FileManagementDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string source = "data.txt";
            string backup = "data_backup.txt";

            // Create source file
            File.WriteAllText(source, "Important data: 42\nSecondary data: RGPV");

            // Check existence before operating
            if (File.Exists(source))
            {
                Console.WriteLine($"'{source}' exists. Size: {new FileInfo(source).Length} bytes.");

                // Copy
                File.Copy(source, backup, overwrite: true);
                Console.WriteLine($"Backed up to '{backup}'.");

                // Display FileInfo metadata
                FileInfo fi = new FileInfo(backup);
                Console.WriteLine($"\n--- FileInfo for '{backup}' ---");
                Console.WriteLine($"Full path  : {fi.FullName}");
                Console.WriteLine($"Size       : {fi.Length} bytes");
                Console.WriteLine($"Created    : {fi.CreationTime}");
                Console.WriteLine($"Extension  : {fi.Extension}");
            }

            // Delete source
            File.Delete(source);
            Console.WriteLine($"\n'{source}' deleted. Exists: {File.Exists(source)}");

            // Path utilities
            Console.WriteLine("\n--- Path Utilities ---");
            Console.WriteLine("File name  : " + Path.GetFileName(backup));
            Console.WriteLine("No ext     : " + Path.GetFileNameWithoutExtension(backup));
            Console.WriteLine("Directory  : " + Path.GetDirectoryName(Path.GetFullPath(backup)));
        }
    }
}
```

### Part D: Working with Directories

```csharp
using System;
using System.IO;

namespace DirectoryDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string reportDir = "Reports";

            // Create directory if it does not exist
            if (!Directory.Exists(reportDir))
            {
                Directory.CreateDirectory(reportDir);
                Console.WriteLine($"Directory '{reportDir}' created.");
            }

            // Write three report files into the directory
            for (int month = 1; month <= 3; month++)
            {
                string fileName = Path.Combine(reportDir, $"Report_{month:D2}_2026.txt");
                File.WriteAllText(fileName, $"Monthly report for month {month}, year 2026.");
            }

            // List all .txt files in the directory
            Console.WriteLine($"\nFiles in '{reportDir}':");
            foreach (string file in Directory.GetFiles(reportDir, "*.txt"))
                Console.WriteLine("  " + Path.GetFileName(file));

            // Cleanup
            Directory.Delete(reportDir, recursive: true);
            Console.WriteLine($"\nDirectory '{reportDir}' and its contents deleted.");
        }
    }
}
```

---

## 4. Expected Output

**Output - Part A:**
```text
File written: C:\...\students.txt

--- File Contents ---
Roll No | Name          | Marks
--------|---------------|------
101     | Akshay Sagar  | 85
102     | Diksha Pawar  | 91
103     | Pawan Tiwari  | 78
```

**Output - Part B:**
```text
Log has 5 lines:

=== Log started at 07/04/2026 10:00:00 ===
[10:00:00] Event 1: Processed record #10
[10:00:01] Event 2: Processed record #20
[10:00:02] Event 3: Processed record #30
=== Log closed at 07/04/2026 10:00:03 ===
```

**Output - Part C:**
```text
'data.txt' exists. Size: 38 bytes.
Backed up to 'data_backup.txt'.

--- FileInfo for 'data_backup.txt' ---
Full path  : C:\...\data_backup.txt
Size       : 38 bytes
Created    : 07/04/2026 10:00:00
Extension  : .txt

'data.txt' deleted. Exists: False

--- Path Utilities ---
File name  : data_backup.txt
No ext     : data_backup
Directory  : C:\...
```

**Output - Part D:**
```text
Directory 'Reports' created.

Files in 'Reports':
  Report_01_2026.txt
  Report_02_2026.txt
  Report_03_2026.txt

Directory 'Reports' and its contents deleted.
```

---

## 5. Viva / Discussion Questions

1. What is the difference between `File.WriteAllText` and `File.AppendAllText`?
2. Why should `StreamReader` and `StreamWriter` be used inside a `using` block?
3. What happens if you call `File.ReadAllText` on a file that doesn't exist?
4. What is the difference between `StreamReader.ReadToEnd()` and `ReadLine()`?
5. What does `Path.Combine()` do? Why is it better than concatenating strings with `\\`?
6. What does `recursive: true` mean in `Directory.Delete`?
7. What is the difference between `File` and `FileInfo` classes?
8. How would you handle a `FileNotFoundException` when reading a file?
