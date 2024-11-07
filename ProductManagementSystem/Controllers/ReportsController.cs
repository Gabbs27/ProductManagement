using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Data;
using ProductManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProductManagementSystem.Controllers
{
    public class ReportsController : Controller
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Reports/Index
        public IActionResult Index()
        {
            return View();
        }

        // Report 1: Customer Items by Item Range
        // GET: Reports/CustomerItemsByItemRange
        public IActionResult CustomerItemsByItemRange()
        {
            // Solo mostramos la página inicial del formulario
            return View();
        }

        // POST: Generate Customer Items by Item Range Report
        [HttpPost]
        public async Task<IActionResult> CustomerItemsByItemRange(int? itemNumberFrom, int? itemNumberTo)
        {
            if (itemNumberFrom == null || itemNumberTo == null)
            {
                ModelState.AddModelError("", "Please provide both Item Number From and To.");
                return View();
            }

            // Obtén todos los CustomerItems de la base de datos
            var customerItems = await _context.CustomerItems
                .Include(ci => ci.Customer)
                .Include(ci => ci.Item)
                .ToListAsync();

            // Filtrar en memoria
            var reportData = customerItems
                .Where(ci => int.TryParse(ci.Item.ItemNumber, out int itemNumber)
                             && itemNumber >= itemNumberFrom
                             && itemNumber <= itemNumberTo)
                .Select(ci => new
                {
                    CustomerId = ci.Customer.Id,
                    CustomerName = ci.Customer.Name,
                    ItemNumber = ci.Item.ItemNumber,
                    Description = ci.Item.Name,
                    Price = ci.Item.DefaultPrice,
                    Quantity = ci.Item.Status ? 100 : 50 // Ajusta la cantidad según la lógica de tu aplicación
                })
                .ToList();

            return View(reportData);
        }

        public async Task<IActionResult> TopItemsByCustomer(int itemCount = 3)
        {
            var customers = await _context.Customers
                .Include(c => c.CustomerItems)
                .ThenInclude(ci => ci.Item)
                .ToListAsync();

            var reportData = customers.Select(c => new CustomerTopItemsViewModel
            {
                Customer = c,
                TopItems = c.CustomerItems
                    .OrderByDescending(ci => ci.Item.DefaultPrice)
                    .Take(itemCount)
                    .Select(ci => ci.Item)
                    .ToList()
            }).ToList();

            return View(reportData);
        }
    }
}
