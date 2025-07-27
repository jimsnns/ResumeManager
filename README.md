# Resume Manager – ASP.NET Core MVC Project

This is a sample resume management application built with **ASP.NET Core 8.0 MVC** and **Entity Framework Core In-Memory database**, created as part of a technical evaluation for Byte Computer.

---

## 📚 Project Description

The application provides basic CRUD functionality for managing **candidates** and their associated **degrees**. Users can also **upload, view, and delete CV files** (PDF or Word) per candidate.

---

## 🛠️ Technologies Used

- ASP.NET Core 8.0 MVC
- Entity Framework Core (In-Memory provider)
- Bootstrap 5
- Razor Views
- C#

---

## 📁 Features

- ✅ Add, edit, delete candidates
- ✅ Add, edit, delete degrees
- ✅ Link degrees to candidates (dropdown selection)
- ✅ Upload CVs (PDF/Word)
- ✅ View uploaded CVs directly in browser
- ✅ Delete CV files from server
- ✅ Delete degrees that are not used by any candidate
- ✅ Form validation on all required fields

---

## 🧪 Validation Rules

- **First Name / Last Name** – required
- **Email** – required, valid format
- **Mobile** – optional, must be exactly 10 digits if filled
- **Degree** – optional (dropdown from degrees list)
- **CV** – optional (uploadable .pdf or .doc/.docx)

---

## 🚀 Getting Started

### 1. Clone the Repository
```bash
https://github.com/jimsnns/ResumeManager.git
cd ResumeManager
```

### 2. Open in Visual Studio 2022+

### 3. Run the Application
Run with F5 or via the terminal:
```bash
dotnet run
```
- The app will launch on https://localhost:<port>

## 📂 CV Upload Folder

Uploaded CVs are stored under:

``` wwwroot/uploads/ ```

Make sure the folder exists or is created at runtime.

## 👤 Author
Developed by Dimitris Sinanis ||
Full Stack Software Engineer
