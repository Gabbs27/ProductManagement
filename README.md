# ProductManagement

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

This is a web application built with ASP.NET Core MVC for managing products, customers, and customer-product associations. The system includes full CRUD functionality for items, customers, and customer items, as well as two reporting capabilities. Unit tests are included to ensure reliability.

## Features

- **Item Management**: Create, read, update, and delete items with fields like item number, description, price, category, and status.
- **Customer Management**: Manage customer information, including name, email, status (active/inactive), and phone number.
- **Customer Items**: Assign items to customers, associating products with individual customers.
- **Reports**:
  - Top items by customer.
  - Customer items filtered by a specified item number range.

## Technologies

- **ASP.NET Core 6.0**: Main framework for building the web application.
- **Entity Framework Core**: ORM framework for data access.
- **SQLite**: Database used for development for ease of setup.
- **Bootstrap**: Used for responsive design and styling.
- **XUnit**: Unit testing framework to verify functionality.

## Installation and Setup

1. **Clone the repository**:
   ```bash
   git clone https://github.com/Gabbs27/ProductManagement.git
   cd ProductManagement
Set up the database: Run the following commands to create and apply migrations:
dotnet ef migrations add InitialCreate
dotnet ef database update
Run the application: Start the application using the command:
dotnet run
The application will be available at http://localhost:5000 by default.
Run Unit Tests: To execute the unit tests, navigate to the test project directory and run:
dotnet test
Project Structure

Controllers: Contains the main controllers (ItemsController, CustomersController, CustomerItemsController, and ReportsController) which handle HTTP requests and manage data flow.
Models: Defines the data models (Item, Customer, CustomerItem) with necessary validations and relationships.
Data: Contains AppDbContext for managing database access and entity configurations.
Views: Razor views for each entity and report, providing user-friendly data representation.
Tests: Unit tests using XUnit to ensure functionality and reliability.
Usage

Items: Manage items, each with properties like item number, description, default price, category, and status.
Customers: Manage customer data, including name, email, phone number, and status.
Customer Items: Associate items with customers.
Reports:
Generate a report showing top items by customer.
Generate a report showing customer items within a specified item number range.
Example Screenshots

Fork the repository.
Create a feature branch (git checkout -b feature/YourFeature).
Commit your changes (git commit -m 'Add YourFeature').
Push to the branch (git push origin feature/YourFeature).
Open a Pull Request.
License

This project is licensed under the MIT License. See the LICENSE file for more details.





