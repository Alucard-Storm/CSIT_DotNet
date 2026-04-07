# Extra Experiment 07 — File I/O | Notes

---

## The Three Common Ways to Write a File

```csharp
// 1. Quick one-liner (small content, whole file at once)
File.WriteAllText("file.txt", "Hello World");     // creates or OVERWRITES
File.AppendAllText("file.txt", "\nNew line");     // adds to end

// 2. StreamWriter (line-by-line, buffered — better for large files)
using (StreamWriter sw = new StreamWriter("file.txt"))
{
    sw.WriteLine("Line 1");
    sw.WriteLine("Line 2");
}
// file is closed automatically when the using block exits

// 3. Append mode with StreamWriter
using (StreamWriter sw = new StreamWriter("file.txt", append: true))
{
    sw.WriteLine("This is added to the end.");
}
```

---

## The Three Common Ways to Read a File

```csharp
// 1. Read entire file as one string
string content = File.ReadAllText("file.txt");

// 2. Read all lines into an array
string[] lines = File.ReadAllLines("file.txt");
foreach (string line in lines)
    Console.WriteLine(line);

// 3. StreamReader — line-by-line (memory-efficient for huge files)
using (StreamReader sr = new StreamReader("file.txt"))
{
    string line;
    while ((line = sr.ReadLine()) != null)   // ReadLine returns null at end of file
        Console.WriteLine(line);
}
```

---

## The `using` Statement — Why It Matters

```csharp
// WITHOUT using — file handle stays open if an exception occurs
StreamWriter sw = new StreamWriter("file.txt");
sw.WriteLine("data");
// if an exception happens here, sw.Close() is never called → file is locked!

// WITH using — Close() is called automatically, even on exception
using (StreamWriter sw = new StreamWriter("file.txt"))
{
    sw.WriteLine("data");
}   // ← Close() is called here automatically
```

---

## `File` (static) vs `FileInfo` (instance)

```csharp
// File — one-shot static methods (simpler for single operations)
File.Exists("data.txt")
File.Copy("src.txt", "dst.txt")
File.Delete("file.txt")

// FileInfo — reusable object with metadata (better when you need file properties)
FileInfo fi = new FileInfo("data.txt");
fi.Length           // size in bytes
fi.CreationTime     // when it was created
fi.LastWriteTime    // when it was last modified
fi.Extension        // ".txt"
fi.FullName         // full absolute path
```

---

## `Path` — Build Paths Safely

```csharp
// Never build paths manually with string concatenation
string bad  = "Reports" + "\\" + "file.txt";   // fragile — works only on Windows

// Use Path.Combine — handles slashes for the current OS
string good = Path.Combine("Reports", "file.txt");

// Other Path utilities
Path.GetFileName("C:\\data\\file.txt")              // "file.txt"
Path.GetFileNameWithoutExtension("file.txt")         // "file"
Path.GetExtension("file.txt")                        // ".txt"
Path.GetDirectoryName("C:\\data\\file.txt")          // "C:\\data"
Path.GetFullPath("file.txt")                         // absolute path from cwd
```

---

## File vs Directory Operations

```csharp
// Files
File.Exists("file.txt")
File.WriteAllText / ReadAllText / AppendAllText
File.Copy("src", "dst", overwrite: true)
File.Move("old.txt", "new.txt")
File.Delete("file.txt")

// Directories
Directory.Exists("Reports")
Directory.CreateDirectory("Reports")
Directory.GetFiles("Reports", "*.txt")    // returns string[] of matching paths
Directory.Delete("Reports", recursive: true)
```

---

## Common Mistakes

```csharp
// 1. Not checking File.Exists before reading
string data = File.ReadAllText("missing.txt"); // FileNotFoundException!
// Fix:
if (File.Exists("missing.txt"))
    string data = File.ReadAllText("missing.txt");

// 2. Forgetting the using block — file stays locked
StreamWriter sw = new StreamWriter("log.txt");
sw.WriteLine("data");
// forgot sw.Close() → file locked by the process

// 3. Hardcoded path separators
string path = "logs\\file.txt";   // breaks on Linux/Mac
string path = Path.Combine("logs", "file.txt");  // cross-platform

// 4. Overwriting a file when you meant to append
File.WriteAllText("log.txt", "new entry");  // DELETES old content!
File.AppendAllText("log.txt", "new entry"); // adds to end
```
