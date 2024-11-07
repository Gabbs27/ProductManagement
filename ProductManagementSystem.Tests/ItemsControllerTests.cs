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
    public class ItemsControllerTests
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
        public async Task Index_ReturnsViewWithItems()
        {
            // Arrange
            using var context = GetContext();
            var item = new Item { Id = 1, ItemNumber = "001", Name = "Test Item", DefaultPrice = 10.5m, ItemCategory = "Category1", Status = true };
            context.Items.Add(item);
            await context.SaveChangesAsync();

            var controller = new ItemsController(context);

            // Act
            var result = await controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            Assert.IsAssignableFrom<IEnumerable<Item>>(result.Model);
        }

        [Fact]
        public async Task Create_AddsNewItemAndRedirectsToIndex()
        {
            // Arrange
            using var context = GetContext();
            var controller = new ItemsController(context);
            var newItem = new Item { Id = 2, ItemNumber = "002", Name = "New Item", DefaultPrice = 15.0m, ItemCategory = "Category2", Status = true };

            // Act
            var result = await controller.Create(newItem) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Single(context.Items);
            Assert.Equal("New Item", context.Items.Find(2).Name);
        }

        [Fact]
        public async Task Delete_RemovesItemAndRedirectsToIndex()
        {
            // Arrange
            using var context = GetContext();
            var item = new Item { Id = 3, ItemNumber = "003", Name = "Item to Delete", DefaultPrice = 12.5m, ItemCategory = "Category3", Status = true };
            context.Items.Add(item);
            await context.SaveChangesAsync();

            var controller = new ItemsController(context);

            // Act
            var result = await controller.DeleteConfirmed(3) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Empty(context.Items); // Ensure the item was deleted
        }
    }
}
