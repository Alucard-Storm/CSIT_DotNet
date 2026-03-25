# Experiment 09 — Downloading Data from the Internet (WebClient) | Notes

---

## What is WebClient?

`WebClient` is a C# class that allows your program to visit a website URL and download its content, just like a web browser does — but in the background, without showing any window.

```
Your C# code
     |
     | (visits the URL, downloads the content)
     v
Internet / Server
     |
     v
Data comes back to your C# variable or file
```

---

## Downloading a Web Page as Text

```csharp
using (WebClient client = new WebClient())
{
    string htmlContent = client.DownloadString("https://example.com");
    
    Console.WriteLine("Downloaded " + htmlContent.Length + " characters.");
    Console.WriteLine(htmlContent.Substring(0, 200));   // Print only first 200 chars
}
```

**Output:**
```
Downloaded 1256 characters.
<!doctype html>
<html>
<head>
    <title>Example Domain</title>
```

---

## Downloading a File and Saving It

```csharp
using (WebClient client = new WebClient())
{
    client.DownloadFile(
        "https://www.w3.org/WAI/WCAG21/wcag21.pdf",    // URL to download
        @"C:\Temp\myfile.pdf"                           // Where to save it
    );
    Console.WriteLine("File saved to C:\Temp\myfile.pdf");
}
```

**Output:**
```
File saved to C:\Temp\myfile.pdf
```

---

## Submitting Form Data (HTTP POST)

```csharp
NameValueCollection formData = new NameValueCollection
{
    { "name", "Akshay" },
    { "course", "CSIT-406" }
};

byte[] response = client.UploadValues("https://httpbin.org/post", "POST", formData);
string text = Encoding.UTF8.GetString(response);
Console.WriteLine(text);

// Output: Server echoes back your submitted data as JSON
```

---

## Key Points to Remember

| Method | What it does |
|---|---|
| `DownloadString(url)` | Downloads the page text into a string variable |
| `DownloadFile(url, path)` | Saves a file from the internet to your computer |
| `UploadValues(url, data)` | Submits form data to a server (like clicking Submit) |
| `using` block | Ensures the `WebClient` is properly closed and freed |

**Note:** `WebClient` is from `.NET Framework`. In modern `.NET` projects, developers use `HttpClient` instead.
