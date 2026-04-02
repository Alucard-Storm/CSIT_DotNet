# Experiment 07 — Sending Emails using SMTP | Notes

---

## What Happens When You Send an Email from Code?

Your C# program acts as an **email client**, just like Gmail or Outlook. It connects to an email server (for example, Gmail's SMTP server) and delivers your message.

```
Your C# Code
     |
     |  (gives email to Gmail's server on port 587)
     v
Gmail SMTP Server (smtp.gmail.com)
     |
     v
Recipient's Inbox
```

---

## The Three Core Steps

### Step 1 — Write the Email (MailMessage)

```csharp
MailMessage email = new MailMessage();
email.From = new MailAddress("me@gmail.com");
email.To.Add("friend@example.com");
email.Subject = "Hello from C#!";
email.Body = "This is the message content.";
```

### Step 2 — Connect to the Email Server (SmtpClient)

```csharp
SmtpClient server = new SmtpClient("smtp.gmail.com", 587);
server.EnableSsl = true;                         // Turn on security
server.Credentials = new NetworkCredential("me@gmail.com", "app_password");
```

### Step 3 — Send It

```csharp
server.Send(email);
Console.WriteLine("Email sent!");
```

**Output:**
```
Email sent!
```
*(Check the recipient's inbox to verify delivery.)*

---

## Attaching a File to the Email

```csharp
Attachment file = new Attachment(@"C:\Temp\report.txt");
email.Attachments.Add(file);

server.Send(email);          // Sends the email WITH the attachment
file.Dispose();              // Always free the file from memory after sending
```

---

## Gmail Tip (Important!)

Gmail will **reject** your normal password if you try to use it in code. You must create an **App Password**:

1. Go to Google Account security settings
2. Search for "App Passwords"
3. Generate one and use that 16-character code in your `NetworkCredential`

---

## Key Points to Remember

| Class | Purpose |
|---|---|
| `MailMessage` | Represents the actual email (To, From, Subject, Body) |
| `SmtpClient` | The "mailman" — connects to the server and delivers the email |
| `Attachment` | Adds a file to the email |
| `NetworkCredential` | Stores your login username and password safely |
| `EnableSsl = true` | Turns on TLS security so the password is not sent in plain text |
| Port 587 | Standard secure SMTP port used by all major email providers |
