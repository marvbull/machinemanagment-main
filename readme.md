# 🛠️ Machine Management System

## 📖 Project Overview

This project was originally developed during a vocational training program and later extended and refined during academic studies at the **DHBW (Baden-Wuerttemberg Cooperative State University)**.  
It is a machine management tool designed for **training workshops** to improve **machine assignment**, **visualize machine utilization**, and **relieve instructors** through automation and clear planning tools.

The focus was on developing a practical, responsive, and intuitive interface using modern .NET technologies.

## 📂 Repository Structure

- `src/` – Core WPF application (C#, .NET 7.0, Entity Framework)  
- `doc/` – Documentation, concept drafts, and planning

## ⚙️ Technologies Used

- **C# / .NET 7.0** – Main application logic (WPF)  
- **WPF (Windows Presentation Foundation)** – GUI for machine/job management  
- **Entity Framework Core** – Database access (SQL Server)  
- **FontAwesome.Sharp** – UI icons and styling  
- **Microsoft SQL Server** – Backend database system

> 🔧 Make sure to install the following NuGet packages **exactly in version 7.0.1** for compatibility:
>
> ```bash
> Install-Package Microsoft.EntityFramework.SqlServer -Version 7.0.1  
> Install-Package Microsoft.EntityFrameworkCore -Version 7.0.1  
> Install-Package Microsoft.EntityFramework -Version 7.0.1  
> Install-Package FontAwesome.Sharp -Version 7.0.1
> ```

## 🚀 How It Works

Instructors assign jobs to available machines and monitor machine utilization live.  
The system helps to distribute workloads evenly and visualize bottlenecks or idle machines.

- Central job overview with real-time status  
- Visual machine occupancy  
- Simplified workflow for instructors  
- Reduces planning effort in training environments

## ✅ Project Status

✅ **Completed and functional**  
The system is stable and performs job assignment and utilization tracking as intended.  
Ready for further extension (e.g., web interface, mobile view, reporting).

## 📄 License

This project is licensed under the **MIT License** – see the [LICENSE](./LICENSE) file for details.
