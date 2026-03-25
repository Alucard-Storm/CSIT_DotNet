# Experiment 09 — Data Retrieval and Upload using WebClient

**Subject:** CSIT-406 .NET Framework Lab
**RGPV, Bhopal**

---

## Aim

Retrieve and upload data using the `WebClient` class.

---

## Theory

`WebClient` (in `System.Net`) is a simple class for downloading/uploading data over HTTP/FTP. It wraps `HttpWebRequest` for common use cases.

| Method | Description |
|---|---|
| `DownloadString(url)` | Downloads URL content as a string |
| `DownloadFile(url, path)` | Saves downloaded content to a file |
| `DownloadData(url)` | Downloads as a byte array |
| `UploadString(url, data)` | HTTP POST with string body |
| `UploadFile(url, path)` | Uploads a local file via POST |
| `UploadValues(url, values)` | Uploads form data (key-value pairs) |

> **Note:** `WebClient` is deprecated in .NET 6+. Modern apps use `HttpClient`. However, it remains relevant for .NET Framework 4.x used in RGPV curriculum.

> Real-world analogy: A weather app downloading JSON forecast data from an API uses this exact pattern.

---

## Code

### Part A — Download a Web Page as String

```csharp
using System;
using System.Net;

namespace WebClientDownload
{
    class Program
    {
        static void Main()
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    // Download HTML of a public page
                    string content = client.DownloadString("https://example.com");
                    Console.WriteLine("Downloaded " + content.Length + " characters.");
                    Console.WriteLine("\nFirst 300 characters:");
                    Console.WriteLine(content.Substring(0, Math.Min(300, content.Length)));
                }
                catch (WebException ex)
                {
                    Console.WriteLine("Web Error: " + ex.Message);
                }
            }
        }
    }
}
```

### Part B — Download File to Disk

```csharp
using System;
using System.Net;
using System.IO;

namespace DownloadFile
{
    class Program
    {
        static void Main()
        {
            using (WebClient client = new WebClient())
            {
                // Track download progress
                client.DownloadProgressChanged += (s, e) =>
                    Console.Write($"\rDownloading: {e.ProgressPercentage}%   ");

                string url = "https://www.w3.org/WAI/WCAG21/wcag21.pdf";
                string savePath = @"C:\Temp\downloaded.pdf";

                Directory.CreateDirectory(@"C:\Temp");

                Console.WriteLine("Starting download...");

                // Synchronous download
                client.DownloadFile(url, savePath);

                Console.WriteLine("\nFile saved to: " + savePath);
                Console.WriteLine("File size: " + new FileInfo(savePath).Length + " bytes");
            }
        }
    }
}
```

### Part C — Upload Form Data (HTTP POST)

```csharp
using System;
using System.Net;
using System.Collections.Specialized;

namespace WebClientUpload
{
    class Program
    {
        static void Main()
        {
            using (WebClient client = new WebClient())
            {
                // Simulated POST to a public test API
                string postUrl = "https://httpbin.org/post";

                // Key-value form data
                NameValueCollection formData = new NameValueCollection
                {
                    { "name", "Akshay" },
                    { "subject", "CSIT-406" },
                    { "experiment", "09" }
                };

                try
                {
                    byte[] responseBytes = client.UploadValues(postUrl, "POST", formData);
                    string response = System.Text.Encoding.UTF8.GetString(responseBytes);

                    Console.WriteLine("Upload successful. Server response:");
                    Console.WriteLine(response.Substring(0, Math.Min(400, response.Length)));
                }
                catch (WebException ex)
                {
                    Console.WriteLine("Upload failed: " + ex.Message);
                }
            }
        }
    }
}
```

### Part D — Set Custom Headers

```csharp
using System;
using System.Net;

namespace WebClientHeaders
{
    class Program
    {
        static void Main()
        {
            using (WebClient client = new WebClient())
            {
                // Add custom request headers
                client.Headers[HttpRequestHeader.UserAgent] = "CSIT406-Lab/1.0";
                client.Headers[HttpRequestHeader.Accept] = "application/json";

                string result = client.DownloadString("https://httpbin.org/headers");
                Console.WriteLine(result);
            }
        }
    }
}
```

---

## Expected Output

**Part A:**
```
Downloaded 1256 characters.

First 300 characters:
<!doctype html>
<html>
<head>
    <title>Example Domain</title>
    ...
```

**Part C:**
```
Upload successful. Server response:
{
  "form": {
    "experiment": "09",
    "name": "Akshay",
    "subject": "CSIT-406"
  },
  ...
}
```

---

## Viva Questions

1. What is `WebClient`? Which namespace does it belong to?
2. What is the difference between `DownloadString()` and `DownloadFile()`?
3. How do you set an HTTP header in `WebClient`?
4. What is `NameValueCollection` used for in `UploadValues()`?
5. Why is `HttpClient` preferred over `WebClient` in modern .NET?

---

[Back to Index](../README.md)
