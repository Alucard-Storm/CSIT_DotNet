# Experiment 09 — Downloading Web Data using WebClient

**Subject:** CSIT-406 .NET Framework Lab
**Location:** RGPV, Bhopal

---

## 1. Aim

To write a program that can download information and files automatically from the internet using the `WebClient` class.

## 2. Theory

The `System.Net.WebClient` class is a tool provided by C# to make it very easy for your program to talk to websites. Instead of opening a web browser like Chrome, your code can visit a URL and download the text or files directly.

### Main Methods of `WebClient`

| Method Name | Explanation |
|---|---|
| **`DownloadString(url)`** | Visits a website and downloads all the HTML text from that page into a C# variable. |
| **`DownloadFile(url, fileName)`** | Visits a link (like a picture or PDF link) and saves it directly to a folder on your computer's hard drive. |
| **`UploadValues(url, data)`** | Used to fill out an online form automatically with code and click "Submit" (send data to the server). |

*Note:* While `WebClient` is great for simple lab studies in the .NET Framework, modern professional applications usually replace it with a newer class called `HttpClient` to better handle massive amounts of user traffic.

*Instructional Example:* Think of a weather app on your phone. When you open the app, it doesn't open a web page. Instead, it uses code to silently download current temperature data from a weather server, and then it displays that data on your screen.

---

## 3. Implementation Code

### Part A: Download Website Text

This standard program will connect to a public webpage and download its source code text.

```csharp
// File: DownloadText.cs
using System;
using System.Net;

namespace WebClientDownload
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Connecting to example.com...");
            
            // Create the WebClient tool
            using (WebClient browserNode = new WebClient())
            {
                try
                {
                    // Tell it to download the text at this URL
                    string websiteText = browserNode.DownloadString("https://example.com");
                    
                    Console.WriteLine("Download finished!");
                    Console.WriteLine($"The page contains {websiteText.Length} characters of text.");
                    
                    Console.WriteLine("\nHere is a small preview of the HTML code:");
                    Console.WriteLine(websiteText.Substring(0, 200)); 
                }
                catch (WebException ex)
                {
                    Console.WriteLine("Error connecting to the internet: " + ex.Message);
                }
            }
        }
    }
}
```

### Part B: Download a File to Your Computer

This program connects to an online file and saves it directly to your `C:\Temp` folder.

```csharp
// File: DownloadFile.cs
using System;
using System.Net;
using System.IO;

namespace DownloadFileApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // You can change this link to any direct file on the internet
            string downloadUrl = "https://www.w3.org/WAI/WCAG21/wcag21.pdf";
            string locationToSave = @"C:\Temp\MyDownloadedFile.pdf";

            // Make sure the C:\Temp folder exists
            Directory.CreateDirectory(@"C:\Temp");

            Console.WriteLine("Downloading file, please wait...");

            using (WebClient browserNode = new WebClient())
            {
                // This line actually downloads and saves the file quietly
                browserNode.DownloadFile(downloadUrl, locationToSave);

                Console.WriteLine("Success! File saved to: " + locationToSave);
            }
        }
    }
}
```

---

## 4. Expected Output

**Output - Part A:**
```text
Connecting to example.com...
Download finished!
The page contains 1256 characters of text.

Here is a small preview of the HTML code:
<!doctype html>
<html>
<head>
    <title>Example Domain</title>
```

**Output - Part B:**
```text
Downloading file, please wait...
Success! File saved to: C:\Temp\MyDownloadedFile.pdf
```

---

## 5. Viva / Discussion Questions

1. **Definitions:** What is the primary purpose of the `WebClient` class?
2. **File Safety:** What is the difference between `DownloadString` and `DownloadFile` regarding where the data is stored?
3. **Modernization:** Why does Microsoft recommend using `HttpClient` instead of `WebClient` for modern web applications?
4. **Exception Handling:** What exception (`WebException`) might be thrown if the computer has no internet access while trying to download?
5. **Memory:** Why is a `using` block placed around the `new WebClient()` object code? (Hint: `IDisposable`).

---

[Back to Main Index](../README.md)
