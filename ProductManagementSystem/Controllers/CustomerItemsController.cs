using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Data;
using ProductManagementSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementSystem.Controllers
{
    public class CustomerItemsController : Controller
    {
        private readonly AppDbContext _context;

        public CustomerItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CustomerItems
        public async Task<IActionResult> Index()
        {
            var customerItems = _context.CustomerItems
                .Include(ci => ci.Customer)
                .Include(ci => ci.Item);
            return View(await customerItems.ToListAsync());
        }

        // GET: CustomerItems/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name");
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name");
            return View();
        }

        // POST: CustomerItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerItem customerItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", customerItem.CustomerId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", customerItem.ItemId);
            return View(customerItem);
        }

        // GET: CustaomerItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var customerItem = await _context.CustomerItems
                .Include(ci => ci.Customer)
                .Include(ci => ci.Item)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerItem == null) return NotFound();

            return View(customerItem);
        }

        // POST: CustomerItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerItem = await _context.CustomerItems.FindAsync(id);
            _context.CustomerItems.Remove(customerItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
