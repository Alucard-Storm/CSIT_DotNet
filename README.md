# .NET FRAMEWORK LAB MANUAL (CSIT-406)
## Computer Science & Information Technology — IV Semester
### Rajiv Gandhi Proudyogiki Vishwavidyalaya (RGPV), Bhopal

---

# 1. INTRODUCTION

This laboratory manual is designed in accordance with the **CSIT-406 .NET Framework Lab syllabus** under the AICTE Flexible Curricula for IV Semester.

The experiments follow a progressive structure — from delegates and security to COM+ components, Windows Services, Reflection, email, database access, and network communication.

Each experiment uses the **.NET Framework 4.x** on Windows, with C# as the primary language.

---

# 2. COURSE OBJECTIVES

- To understand and implement **Delegates and Callbacks** in C#
- To apply **.NET security** using role-based and permission-based mechanisms
- To create and register **COM+ components** for distributed transactions
- To build and install **Windows Services** for background processing
- To control services programmatically using the **ServiceController** class
- To utilize **Reflection** to inspect types and invoke methods dynamically
- To send emails using the **SMTP protocol**
- To evaluate **String vs StringBuilder** performance differences
- To retrieve and upload data using **WebClient**
- To perform **database operations** using ADO.NET with SQL Server

---

# 3. SOFTWARE REQUIREMENTS

- **IDE**: Visual Studio 2019 / 2026 (Community or higher)
- **Framework**: .NET Framework 4.8
- **Language**: C#
- **OS**: Windows 10 / 11 (required for Windows Service experiments)
- **Database** (Exp 10): Microsoft SQL Server / SQL Server Express / LocalDB

---

# 4. LIST OF EXPERIMENTS

| No. | Topic | Experiment | Notes |
|-----|-------|------------|-------|
| 01 | Delegates and Callbacks | [**Exp01_Delegates.md**](Experiment_01/Exp01_Delegates.md) | [Notes](Experiment_01/Exp01_Delegates_Notes.md) |
| 02 | Role-Based and Permission-Based Security | [**Exp02_Security.md**](Experiment_02/Exp02_Security.md) | [Notes](Experiment_02/Exp02_Security_Notes.md) |
| 03 | COM+ Component Registration | [**Exp03_COM_Plus.md**](Experiment_03/Exp03_COM_Plus.md) | [Notes](Experiment_03/Exp03_COM_Plus_Notes.md) |
| 04 | Background Windows Service | [**Exp04_WindowsService.md**](Experiment_04/Exp04_WindowsService.md) | [Notes](Experiment_04/Exp04_WindowsService_Notes.md) |
| 05 | Client App to Control a Windows Service | [**Exp05_ServiceClient.md**](Experiment_05/Exp05_ServiceClient.md) | [Notes](Experiment_05/Exp05_ServiceClient_Notes.md) |
| 06 | Reflection — Inspect & Invoke Dynamically | [**Exp06_Reflection.md**](Experiment_06/Exp06_Reflection.md) | [Notes](Experiment_06/Exp06_Reflection_Notes.md) |
| 07 | Send Email via SMTP Client | [**Exp07_SMTP_Email.md**](Experiment_07/Exp07_SMTP_Email.md) | [Notes](Experiment_07/Exp07_SMTP_Email_Notes.md) |
| 08 | String vs StringBuilder Comparison | [**Exp08_String_StringBuilder.md**](Experiment_08/Exp08_String_StringBuilder.md) | [Notes](Experiment_08/Exp08_String_StringBuilder_Notes.md) |
| 09 | Data Retrieval and Upload using WebClient | [**Exp09_WebClient.md**](Experiment_09/Exp09_WebClient.md) | [Notes](Experiment_09/Exp09_WebClient_Notes.md) |
| 10 | Database Operations with ADO.NET | [**Exp10_ADO_NET.md**](Experiment_10/Exp10_ADO_NET.md) | [Notes](Experiment_10/Exp10_ADO_NET_Notes.md) |

---

# 5. EXTRA / FOUNDATIONAL EXPERIMENTS

These experiments are **not part of the official CSIT-406 syllabus** but are provided as a starting point for students who are new to C# and the .NET Framework. They build the foundational skills required for all the experiments above.

| No. | Topic | Experiment | Notes |
|-----|-------|------------|-------|
| E01 | Hello World — First Console App | [**Extra01_HelloWorld.md**](Extra/Extra_01/Extra01_HelloWorld.md) | [Notes](Extra/Extra_01/Extra01_HelloWorld_Notes.md) |
| E02 | Variables and Data Types | [**Extra02_Variables_DataTypes.md**](Extra/Extra_02/Extra02_Variables_DataTypes.md) | [Notes](Extra/Extra_02/Extra02_Variables_DataTypes_Notes.md) |
| E03 | Control Flow — if, switch, loops | [**Extra03_ControlFlow.md**](Extra/Extra_03/Extra03_ControlFlow.md) | [Notes](Extra/Extra_03/Extra03_ControlFlow_Notes.md) |
| E04 | Arrays and Collections | [**Extra04_Arrays_Collections.md**](Extra/Extra_04/Extra04_Arrays_Collections.md) | [Notes](Extra/Extra_04/Extra04_Arrays_Collections_Notes.md) |
| E05 | Classes and Objects (OOP) | [**Extra05_Classes_Objects.md**](Extra/Extra_05/Extra05_Classes_Objects.md) | [Notes](Extra/Extra_05/Extra05_Classes_Objects_Notes.md) |
| E06 | Exception Handling | [**Extra06_ExceptionHandling.md**](Extra/Extra_06/Extra06_ExceptionHandling.md) | [Notes](Extra/Extra_06/Extra06_ExceptionHandling_Notes.md) |
| E07 | File I/O — Read and Write Files | [**Extra07_FileIO.md**](Extra/Extra_07/Extra07_FileIO.md) | [Notes](Extra/Extra_07/Extra07_FileIO_Notes.md) |

> **Recommended order for beginners:** E01 → E02 → E03 → E04 → E05 → E06 → E07 → then proceed with Experiment 01 onwards.

---

# 6. GENERAL INSTRUCTIONS

- Each experiment follows the standard format:
  - **Aim** — What the experiment demonstrates
  - **Theory** — Concepts and key classes explained simply
  - **Code** — Ready-to-run C# source files
  - **Expected Output** — What the console or file should display
  - **Viva / Discussion Questions** — Common questions to prepare for oral examination
- All experiments must be executed on **Windows** (Visual Studio + .NET Framework 4.x).
- Students must maintain a **lab record** with handwritten entries or printed code and output.
- For Experiment 03 and 04, run Visual Studio / Developer Command Prompt as **Administrator**.

---

# 7. EVALUATION SCHEME

- Continuous Lab Assessment
- Experiment Implementation & Running Output
- Viva-Voce Examination
- Lab Record Submission

---

# 8. CONCLUSION

This lab provides hands-on exposure to the core features of the Microsoft .NET Framework. From threading and delegates to COM+ services, Reflection, and ADO.NET database access, students acquire foundational skills essential for enterprise-level Windows application development.

---

**Department of Computer Science & Information Technology**
Rajiv Gandhi Proudyogiki Vishwavidyalaya (RGPV), Bhopal
