using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementSystem.Controllers
{
    public class ReportsController : Controller
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context)
        {
            _context = context;
        }

        // Reporte 1: Clientes con Ciertos Números de Ítems Asignados
        public async Task<IActionResult> CustomersWithItemCount(int itemCount)
        {
            var customers = await _context.Customers
                .Where(c => c.CustomerItems.Count == itemCount)
                .Include(c => c.CustomerItems)
                .ThenInclude(ci => ci.Item)
                .ToListAsync();

            ViewData["ItemCount"] = itemCount;
            return View(customers);
        }

        // Reporte 2: Los X Artículos Más Caros por Cliente
        public async Task<IActionResult> TopExpensiveItemsByCustomer(int topCount)
        {
            var customerItems = await _context.Customers
                .Include(c => c.CustomerItems)
                .ThenInclude(ci => ci.Item)
                .Select(c => new
                {
                    Customer = c,
                    TopItems = c.CustomerItems
                        .OrderByDescending(ci => ci.Item.DefaultPrice) // Ordenar por precio
                        .Take(topCount) // Tomar los X más caros
                })
                .ToListAsync();

            ViewData["TopCount"] = topCount;
            return View(customerItems);
        }
    }
}
