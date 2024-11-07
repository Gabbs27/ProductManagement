# ProductManagement

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

This is a web application built with ASP.NET Core MVC for managing products, customers, and customer-product associations, including full CRUD functionality and reporting features.

## Features

- **Item Management**: CRUD for items with fields like item number, description, price, category, and status.
- **Customer Management**: Manage customers with fields like name, email, status, and phone number.
- **Customer Items**: Associate items with specific customers.
- **Reports**:
  - Top items by customer.
  - Customer items filtered by item number range.

## Technologies

- **ASP.NET Core 6.0**
- **Entity Framework Core**
- **SQLite** for easy setup
- **Bootstrap** for responsive design
- **XUnit** for unit testing

## Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/Gabbs27/ProductManagement.git
   cd ProductManagement
   
2. **Set up the database:
  ```bash
    dotnet ef migrations add InitialCreate
    dotnet ef database update
```
3. **Run the application:
```bash
    dotnet run
```
Open http://localhost:5000 in your browser.

Run Unit Tests:
```bash
dotnet test
```
Usage

Items: Manage items with item number, description, default price, category, and status.
Customers: Manage customer info including name, email, and status.
Customer Items: Associate items with customers.
Reports: Generate reports for top items by customer and items by item number range.
Project Structure

Controllers: Handle HTTP requests (ItemsController, CustomersController, etc.).
Models: Define data models (Item, Customer, CustomerItem).
Data: AppDbContext for database access.
Views: Razor views for data representation.
Tests: XUnit tests for functionality.
Contributing

Fork the repo.
Create a branch (git checkout -b feature/YourFeature).
Commit (git commit -m 'Add YourFeature').
Push (git push origin feature/YourFeature).
Open a Pull Request.
License

This project is licensed under the MIT License. See LICENSE for more details.
