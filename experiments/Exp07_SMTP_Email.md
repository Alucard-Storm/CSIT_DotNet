# Experiment 07 — Send Email via SMTP Client

**Subject:** CSIT-406 .NET Framework Lab
**RGPV, Bhopal**

---

## Aim

Send emails using SMTP client with authentication and attachments.

---

## Theory

.NET provides the `System.Net.Mail` namespace for sending emails programmatically.

| Class | Purpose |
|---|---|
| `SmtpClient` | Handles connection to SMTP server and sending |
| `MailMessage` | Represents the email (from, to, subject, body) |
| `MailAddress` | Represents an email address |
| `Attachment` | Attaches a file to the email |
| `NetworkCredential` | Provides username/password for SMTP auth |

**Common SMTP Servers:**

| Provider | Server | Port | SSL |
|---|---|---|---|
| Gmail | smtp.gmail.com | 587 | Yes (TLS) |
| Outlook | smtp.office365.com | 587 | Yes |
| Yahoo | smtp.mail.yahoo.com | 587 | Yes |

> **Note for Gmail:** Enable **"App Password"** under Google Account Security (if 2FA is on). Do not use your main password directly.

> Real-world analogy: Automated order confirmation emails from Amazon or OTP emails from banks use SMTP exactly like this.

---

## Code

### Part A — Simple Email

```csharp
using System;
using System.Net;
using System.Net.Mail;

namespace SMTPDemo
{
    class Program
    {
        static void Main()
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("your_email@gmail.com", "Lab Demo");
                mail.To.Add("recipient@example.com");
                mail.Subject = "CSIT-406 Lab Test Email";
                mail.Body = "Hello!\n\nThis email was sent from a .NET SMTP client.\n\nRegards,\nLab 07";
                mail.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("your_email@gmail.com", "your_app_password");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtp.Send(mail);
                Console.WriteLine("Email sent successfully!");
            }
            catch (SmtpException ex)
            {
                Console.WriteLine("SMTP Error: " + ex.Message);
            }
        }
    }
}
```

### Part B — Email with HTML Body and Attachment

```csharp
using System;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace SMTPWithAttachment
{
    class Program
    {
        static void Main()
        {
            // Create a temp file to attach
            string filePath = @"C:\Temp\report.txt";
            Directory.CreateDirectory(@"C:\Temp");
            File.WriteAllText(filePath, "Lab Report - CSIT 406\nExperiment 7: SMTP Email Demo");

            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("your_email@gmail.com", "Lab Demo");
                mail.To.Add("recipient@example.com");
                mail.CC.Add("cc_person@example.com");          // CC
                mail.Subject = "Lab Report with Attachment";
                mail.IsBodyHtml = true;

                mail.Body = @"
                    <h2>CSIT-406 Lab Report</h2>
                    <p>Please find the <b>experiment report</b> attached.</p>
                    <p>Sent from .NET SMTP Client</p>
                ";

                // Add attachment
                Attachment attachment = new Attachment(filePath);
                mail.Attachments.Add(attachment);

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential("your_email@gmail.com", "your_app_password")
                };

                smtp.Send(mail);
                Console.WriteLine("Email with attachment sent successfully!");

                attachment.Dispose();
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

## Expected Output

```
Email sent successfully!
```

*(Check recipient inbox — should receive email with subject and optional attachment.)*

---

## Configuration Reminder

```
Gmail Setup:
1. Go to myaccount.google.com
2. Security → 2-Step Verification → App Passwords
3. Generate password for "Mail" + "Windows Computer"
4. Use that 16-character code in NetworkCredential
```

---

## Viva Questions

1. What namespace is used for sending emails in .NET?
2. What is the difference between `mail.To`, `mail.CC`, and `mail.BCC`?
3. Why is port 587 used instead of port 25?
4. What does `EnableSsl = true` do in `SmtpClient`?
5. How do you send an HTML email instead of plain text?

---

[Back to Index](../README.md)
