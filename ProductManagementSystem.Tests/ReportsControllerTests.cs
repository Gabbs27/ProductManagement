using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Controllers;
using ProductManagementSystem.Data;
using ProductManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ProductManagementSystem.Tests
{
    public class ReportsControllerTests
    {
        private DbContextOptions<AppDbContext> _contextOptions;

        public ReportsControllerTests()
        {
            // Set up the in-memory database options
            _contextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;
        }

        private AppDbContext GetContext()
        {
            var context = new AppDbContext(_contextOptions);

            // Clear the existing data to ensure test isolation
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }

        [Fact]
        public async Task CustomerItemsByItemRange_ReturnsViewWithCorrectData()
        {
            using var context = GetContext();
            var customer = new Customer { Id = 1, Name = "Test Customer", Email = "test@example.com" };
            var item = new Item { Id = 1, ItemNumber = "100", Name = "Test Item", DefaultPrice = 20.5m, Status = true, ItemCategory = "Category1" };
            var customerItem = new CustomerItem { Id = 1, CustomerId = customer.Id, ItemId = item.Id };

            context.Customers.Add(customer);
            context.Items.Add(item);
            context.CustomerItems.Add(customerItem);
            await context.SaveChangesAsync();

            var controller = new ReportsController(context);
            var result = await controller.CustomerItemsByItemRange(50, 150) as ViewResult;
            var model = result?.Model as IEnumerable<dynamic>;

            Assert.Single(model); // Confirm we got one item in range
        }


        [Fact]
        public async Task TopItemsByCustomer_ReturnsViewWithTopItems()
        {
            // Arrange
            using var context = GetContext();

            var customer = new Customer { Id = 1, Name = "John Doe", Email = "john@example.com" };
            var item1 = new Item { Id = 1, ItemNumber = "001", Name = "Item 1", DefaultPrice = 50m, ItemCategory = "Category1" };
            var item2 = new Item { Id = 2, ItemNumber = "002", Name = "Item 2", DefaultPrice = 100m, ItemCategory = "Category2" };
            var item3 = new Item { Id = 3, ItemNumber = "003", Name = "Item 3", DefaultPrice = 75m, ItemCategory = "Category3" };

            var customerItem1 = new CustomerItem { Id = 1, Customer = customer, Item = item1 };
            var customerItem2 = new CustomerItem { Id = 2, Customer = customer, Item = item2 };
            var customerItem3 = new CustomerItem { Id = 3, Customer = customer, Item = item3 };

            context.Customers.Add(customer);
            context.Items.AddRange(item1, item2, item3);
            context.CustomerItems.AddRange(customerItem1, customerItem2, customerItem3);
            await context.SaveChangesAsync();

            var controller = new ReportsController(context);

            // Act
            var result = await controller.TopItemsByCustomer(2) as ViewResult;
            var model = result?.Model as List<CustomerTopItemsViewModel>;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Single(model); // Only one customer should be in the result
            Assert.Equal(2, model.First().TopItems.Count); // There should be 2 top items
            Assert.Equal("Item 2", model.First().TopItems[0].Name); // Most expensive item first
            Assert.Equal("Item 3", model.First().TopItems[1].Name); // Second most expensive item
        }
    }
}
