# MachineManagement

## Project Overview

**MachineManagement** is a project that was initially developed during vocational training and later revisited and improved during academic studies at the **DHBW (Baden-Wuerttemberg Cooperative State University)**. The goal of the project is to optimize the management and scheduling of machines in a training workshop environment.

It provides a centralized platform to:

- Assign production jobs to available machines  
- Visualize the current and upcoming machine workload  
- Relieve instructors from manual planning tasks  
- Gain a clear overview of all machines and their status

This system helps improve machine utilization and planning transparency.

---

## Key Features

- Assign tasks and jobs to machines  
- Visual utilization dashboard per machine  
- Instructor dashboard for full overview  
- Improved planning and workload distribution

---

## Technologies Used

- **C#**
- **.NET 7.0**
- **WPF (Windows Presentation Foundation)**
- **Entity Framework Core**
- **Microsoft SQL Server**
- **FontAwesome.Sharp** (icon library)

---

## Getting Started

### Requirements

- Visual Studio 2022 or newer  
- .NET 7.0 SDK  
- SQL Server (local or remote)

### Required NuGet Packages

⚠️ Make sure to install **exactly version 7.0.1** of the following packages to ensure .NET 7 compatibility:

```bash
Install-Package Microsoft.EntityFramework.SqlServer -Version 7.0.1
Install-Package Microsoft.EntityFrameworkCore -Version 7.0.1
Install-Package Microsoft.EntityFramework -Version 7.0.1
Install-Package FontAwesome.Sharp -Version 7.0.1
```

---

### How to Run the Project

1. Clone the repository:

   ```bash
   git clone https://github.com/your-username/machinemanagement-main.git
   cd machinemanagement-main
   ```

2. Open the solution (`.sln`) file in Visual Studio.  
3. Configure your database connection in `appsettings.json`.  
4. Apply the initial database migration:

   ```bash
   Update-Database
   ```

5. Press **F5** or click **Start** to launch the application.

---

## License

This project is licensed under the **MIT License**.