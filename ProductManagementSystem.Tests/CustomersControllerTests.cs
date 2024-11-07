using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Controllers;
using ProductManagementSystem.Data;
using ProductManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ProductManagementSystem.Tests
{
    public class CustomersControllerTests
    {
        private DbContextOptions<AppDbContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{System.Guid.NewGuid()}")
                .Options;
        }

        private AppDbContext GetContext()
        {
            return new AppDbContext(CreateNewContextOptions());
        }

        [Fact]
        public async Task Index_ReturnsViewWithCustomers()
        {
            // Arrange
            using var context = GetContext();
            var customer = new Customer { Id = 1, Name = "John Doe", Email = "john@example.com", IsActive = true, PhoneNumber = "1234567890" };
            context.Customers.Add(customer);
            await context.SaveChangesAsync();

            var controller = new CustomersController(context);

            // Act
            var result = await controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            Assert.IsAssignableFrom<IEnumerable<Customer>>(result.Model);
        }

        [Fact]
        public async Task Create_AddsNewCustomerAndRedirectsToIndex()
        {
            // Arrange
            using var context = GetContext();
            var controller = new CustomersController(context);
            var newCustomer = new Customer { Id = 2, Name = "Jane Doe", Email = "jane@example.com", IsActive = true, PhoneNumber = "0987654321" };

            // Act
            var result = await controller.Create(newCustomer) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Single(context.Customers);
            Assert.Equal("Jane Doe", context.Customers.Find(2).Name);
        }

        [Fact]
        public async Task Edit_UpdatesCustomerAndRedirectsToIndex()
        {
            // Arrange
            using var context = GetContext();
            var customer = new Customer { Id = 3, Name = "John Smith", Email = "johnsmith@example.com", IsActive = true, PhoneNumber = "1122334455" };
            context.Customers.Add(customer);
            await context.SaveChangesAsync();

            var controller = new CustomersController(context);

            // Act - Retrieve the entity and modify its properties
            var customerInDb = await context.Customers.FindAsync(3);
            customerInDb.Name = "John Updated";
            customerInDb.Email = "johnupdated@example.com";
            customerInDb.IsActive = false;
            customerInDb.PhoneNumber = "5566778899";

            var result = await controller.Edit(3, customerInDb) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);

            // Refresh the context and verify the updates
            var updatedCustomer = await context.Customers.FindAsync(3);
            Assert.Equal("John Updated", updatedCustomer.Name);
            Assert.Equal("johnupdated@example.com", updatedCustomer.Email);
            Assert.False(updatedCustomer.IsActive);
        }

        [Fact]
        public async Task Delete_RemovesCustomerAndRedirectsToIndex()
        {
            // Arrange
            using var context = GetContext();
            var customer = new Customer { Id = 4, Name = "Alice", Email = "alice@example.com", IsActive = true, PhoneNumber = "6677889900" };
            context.Customers.Add(customer);
            await context.SaveChangesAsync();

            var controller = new CustomersController(context);

            // Act
            var result = await controller.DeleteConfirmed(4) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Empty(context.Customers); // Ensure the customer was deleted
        }
    }
}
