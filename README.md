# ProductManagementSystem

**Repository URL:** [https://github.com/Gabbs27/ProductManagement](https://github.com/Gabbs27/ProductManagement)

## Overview

The ProductManagementSystem is an ASP.NET Core MVC application designed to manage products, customers, and their associated items. It includes basic CRUD operations and reporting capabilities.

## Features

- **Item Management**: Create, read, update, and delete items with fields like item number, name, description, price, category, and status.
- **Customer Management**: Manage customers with fields such as name, email, phone number, and status (active/inactive).
- **Customer Item Assignment**: Associate items with customers and track the relationship.
- **Reporting**: Generate reports, such as:
  - **Top Items by Customer**: Display the top X most expensive items per customer.
  - **Customer Items by Item Range**: Filter customer items within a specified item number range.

## Setup

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/Gabbs27/ProductManagement.git
2. Navigate to the Project Directory:
```bash
    cd ProductManagementSystem
```
3. Install Dependencies:
* Make sure you have .NET 6 SDK installed.
* Restore packages:

```bash
dotnet restore
```

4. Database Setup:
This application uses SQLite as the database provider. Ensure SQLite is installed.
Apply migrations:

```bash
dotnet ef database update
```

5. Run the Application:
   ```bash
   dotnet run

Testing

The project includes unit tests for the controllers. To run tests:
```
cd ProductManagementSystem.Tests
dotnet test
```

```Folder Structure

Controllers/: Contains the MVC controllers for managing items, customers, and customer-item relationships.
Models/: Defines the data models used in the application.
Views/: Contains the Razor views for each controller action.
Data/: Database context and configuration.
Reports/: Report generation module.
```

Requirements

.NET 6 SDK
SQLite

Developed by Gabriel





