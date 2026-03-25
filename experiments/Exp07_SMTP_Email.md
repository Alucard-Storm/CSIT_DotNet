# Experiment 07 — Sending Emails with C# (SMTP Client)

**Subject:** CSIT-406 .NET Framework Lab
**Location:** RGPV, Bhopal

---

## 1. Aim

To write a program that sends an email (including a file attachment) using the Simple Mail Transfer Protocol (SMTP).

## 2. Theory

The `.NET Framework` allows you to send emails directly from your C# code using the `System.Net.Mail` namespace. To do this, your code talks to an Email Server (like Gmail or Outlook) and asks it to deliver your message.

### Main Classes Used

| Class Name | Explanation |
|---|---|
| **`SmtpClient`** | The "mailman." This class connects to the email server and actually sends the mail. |
| **`MailMessage`** | Represents the email itself. You use this to set the "To", "From", "Subject", and the message content. |
| **`MailAddress`** | Used to make sure the email addresses are formatted correctly (like `name@example.com`). |
| **`Attachment`** | Used to attach a file from your computer (like a PDF or TXT file) to the email. |
| **`NetworkCredential`** | A security pass. It holds your username and password to prove to the email server that you are allowed to send mail. |

*Instructional Example:* Whenever you buy something online and immediately receive a receipt in your email, or reset your password and receive a link, the company is using automated SMTP code just like this to send you that message.

---

## 3. Implementation Code

### Part A: Sending a Simple Text Email

This code shows the most basic way to send a plain-text email message.

```csharp
// File: BasicEmail.cs
using System;
using System.Net;
using System.Net.Mail;

namespace SMTPDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // 1. Create the email message
                MailMessage email = new MailMessage();
                email.From = new MailAddress("your_email@gmail.com", "Lab Student");
                email.To.Add("friend@example.com");
                email.Subject = "Hello from C#!";
                email.Body = "Hi there,\n\nI sent this email using my own C# program in the lab.\n\nThanks!";

                // 2. Setup the connection to Gmail's server
                SmtpClient mailServer = new SmtpClient("smtp.gmail.com", 587);
                mailServer.EnableSsl = true; // Turn on security (SSL/TLS)
                
                // 3. Provide your login details
                mailServer.Credentials = new NetworkCredential("your_email@gmail.com", "your_app_password");

                // 4. Send the message
                mailServer.Send(email);
                Console.WriteLine("Success: The email was sent!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Could not send the email. " + ex.Message);
            }
        }
    }
}
```

### Part B: Sending an Email with an Attachment

This code shows how to send an email that includes a file attachment.

```csharp
// File: EmailWithAttachment.cs
using System;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace SMTPWithAttachment
{
    class Program
    {
        static void Main(string[] args)
        {
            // First, let's create a temporary text file to attach
            string filePath = @"C:\Temp\MyLabReport.txt";
            Directory.CreateDirectory(@"C:\Temp");
            File.WriteAllText(filePath, "This is my lab report for Experiment 7.");

            try
            {
                MailMessage email = new MailMessage();
                email.From = new MailAddress("your_email@gmail.com");
                email.To.Add("friend@example.com");
                email.Subject = "Lab Report Attached";
                email.Body = "Please find the attached text file.";

                // Attach the file to the email
                Attachment myFile = new Attachment(filePath);
                email.Attachments.Add(myFile);

                // Setup the server and send
                SmtpClient mailServer = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential("your_email@gmail.com", "your_app_password")
                };

                mailServer.Send(email);
                Console.WriteLine("Success: The email with the attachment was sent!");

                // Clean up the attachment from memory
                myFile.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
```

---

## 4. Expected Output

```text
Success: The email was sent!
Success: The email with the attachment was sent!
```

---

## 5. Gmail Security Instructions

If you use a Gmail account, Google will not let you log in with your normal password from a C# program because it blocks "less secure apps." You must generate an **App Password**:

1. Go to your Google Account website.
2. Search for **App Passwords** in the security menu.
3. Generate a new password specific for this app and use that 16-letter password in your `NetworkCredential` code instead of your real password.

---

## 6. Viva / Discussion Questions

1. **Protocol:** What does SMTP stand for, and what is it used for?
2. **Security:** Why do we need to set `EnableSsl = true` when connecting to Gmail?
3. **MIME Structure:** What happens if you forget to write `myFile.Dispose()` in a program that sends looping emails with attachments?
4. **Ports:** Why do we connect to port `587` instead of the older port `25`?
5. **Class Differences:** What is the specific difference in purpose between the `SmtpClient` class and the `MailMessage` class?

---

[Back to Main Index](../README.md)
