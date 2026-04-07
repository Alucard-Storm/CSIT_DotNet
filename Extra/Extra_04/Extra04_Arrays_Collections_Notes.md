# Extra Experiment 04 — Arrays and Collections | Notes

---

## Array Basics

```csharp
// Declaration and initialisation
int[] marks = new int[5];              // 5 zeros: [0, 0, 0, 0, 0]
int[] marks = { 85, 72, 91, 60, 78 }; // initialiser syntax (size inferred)

// Indexing — STARTS AT 0
marks[0]              // first element  → 85
marks[marks.Length-1] // last element   → 78

// Length
marks.Length          // 5 (total elements)
```

---

## Array vs List Side-by-Side

```
                  Array (int[])       List<int>
──────────────    ──────────────────  ──────────────────
Size              Fixed at creation   Grows/shrinks freely
Add new item      NOT possible        tasks.Add("item")
Remove item       NOT possible        tasks.Remove("item")
Access by index   arr[i]              list[i]
Count of items    arr.Length          list.Count
```

---

## `List<T>` — Most Important Methods

```csharp
List<string> names = new List<string>();

names.Add("Akshay");          // add to end
names.Insert(0, "Diksha");   // add at position 0 (shifts others)
names.Remove("Akshay");      // remove first occurrence of "Akshay"
names.RemoveAt(1);           // remove item at index 1
names.Clear();               // remove ALL items

names.Contains("Diksha");    // true / false — does item exist?
names.IndexOf("Diksha");     // -1 if not found, otherwise index
names.Count;                 // number of items currently in the list
names.Sort();                // sort alphabetically (modifies in-place)

string[] arr = names.ToArray();    // convert List → Array
List<string> list = new List<string>(arr);  // convert Array → List
```

---

## `Dictionary<K, V>` — Key-Value Lookup

Think of it like a phonebook: name (key) → phone number (value).

```csharp
Dictionary<int, string> dir = new Dictionary<int, string>();

dir.Add(101, "Akshay");     // add key 101 with value "Akshay"
dir[102] = "Diksha";        // add OR overwrite key 102

dir[101]                    // direct access → "Akshay" (throws if key missing!)
dir.ContainsKey(103)        // false — check before accessing
dir.TryGetValue(103, out string name)  // safe access — returns bool

dir.Remove(101);            // delete entry with key 101
dir.Count;                  // number of entries

// Iterating
foreach (KeyValuePair<int, string> entry in dir)
    Console.WriteLine($"{entry.Key}: {entry.Value}");
```

---

## Two-Dimensional Arrays

```csharp
int[,] grid = new int[3, 4];   // 3 rows, 4 columns
grid[0, 0] = 1;                 // row 0, column 0

// Or initialise directly
int[,] matrix = { {1, 2}, {3, 4}, {5, 6} };

grid.GetLength(0)   // number of rows    → 3
grid.GetLength(1)   // number of columns → 4
```

---

## `foreach` — The Cleanest Way to Iterate

```csharp
int[] scores = { 85, 72, 91 };

foreach (int score in scores)
    Console.WriteLine(score);

// Same as:
for (int i = 0; i < scores.Length; i++)
    Console.WriteLine(scores[i]);
```

**`foreach` limitation:** you cannot modify the collection inside a `foreach` loop — use a regular `for` loop or `RemoveAll()` for that.

---

## Common Mistakes

```csharp
// 1. Index out of bounds
int[] arr = new int[5];
arr[5] = 10;     // ERROR! Valid indices are 0 to 4

// 2. Accessing a Dictionary key that doesn't exist
dict[999]        // throws KeyNotFoundException — use TryGetValue instead

// 3. Modifying a List while iterating it
foreach (string item in list)
    list.Remove(item);    // ERROR! Cannot modify list while iterating
// Use: list.RemoveAll(item => item == "value");
```
