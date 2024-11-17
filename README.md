# TechFix Procurement System

## Overview

The **TechFix Procurement System** is a backend-focused project built with **ASP.NET Core Web API**. This solution is designed to streamline the procurement workflow for TechFix, addressing inefficiencies in quotation requests, order placement, inventory management, and supplier interactions.

## Key Features

- **Centralized Quotation Management**: Compare and request quotes in one platform.
- **Real-Time Inventory Visibility**: Track supplier inventory levels to avoid delays.
- **Automated Order Placement**: Simplifies and secures the ordering process.
- **Role-Based Access Control**: Granular control over permissions with JWT authentication.
- **Data Integrity and Insights**: Ensures data consistency with Entity Framework Core.

---

## Technologies Used

- **Framework**: ASP.NET Core 8.0 Web API
- **Database**: Microsoft SQL Server (Entity Framework Core for ORM)
- **Authentication**: JWT-based authentication with role-based and permission-based access control
- **Authorization Handlers**: Role-based and permission-based authorization (custom handlers)
- **Development Environment**: Visual Studio 2022

---

## Setup Instructions

### Backend Setup

1. **Clone the Repository**
   ```bash
   git clone https://github.com/YourUsername/TechFix-Procurement-System.git
   cd TechFix-Procurement-System
  ``
2. **Open the Solution**
  - Open TechFix-Solution.sln in Visual Studio.
3. **Database Configuration**
  - Update the connection string in appsettings.json to match your SQL Server setup
    ```bash
    "ConnectionStrings": {
      "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=TechFixDB;Trusted_Connection=True;"
    }
    ```
4. **Run Database Migrations**
   - Use the Package Manager Console or CLI to apply migrations.
    ```bash
    Add-Migration InitialCreate
    Update-Database
    ```
5. **Run the API**
   - Start the project by pressing F5 in Visual Studio or using the command

---

## Features and Components

### Technologies Used

- **Core Entities**:  Supplier, Inventory, Quote, Order, etc.
- **Database Relationships**: Ensures data integrity with one-to-many and many-to-many relationships.
- **Entity Framework Core**: Manages database interactions and migrations.

### Authentication and Authorization

- **JWT Authentication**:  Secures endpoints with token-based authentication.
- **Role-Based Access Control**: Restricts access based on user roles (e.g., "Super Admin," "Procurement Staff").
- **Permission-Based Access Control**: Fine-grained control over user actions (e.g., "CanViewQuotes").
- **Custom Authorization Handlers**: Handlers like RolePermissionHandler and RoleHandler enforce authorization policies.

### Seeders

- **SuperAdminSeeder**: Creates a "Super Admin" role and user for initial setup.
- **PermissionsSeeder**: Assigns default permissions to the "Super Admin" role.
---






  

