using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Data;
using ProductManagementSystem.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var customerItems = await _context.CustomerItems
                .Include(ci => ci.Customer)
                .Include(ci => ci.Item)
                .ToListAsync();
            return View(customerItems);
        }

        // GET: CustomerItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerItem = await _context.CustomerItems
                .Include(ci => ci.Customer)
                .Include(ci => ci.Item)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (customerItem == null)
            {
                return NotFound();
            }

            return View(customerItem);
        }

        // GET: CustomerItems/Create
        public IActionResult Create()
        {
            ViewData["Customers"] = new SelectList(_context.Customers, "Id", "Name");
            ViewData["Items"] = new SelectList(_context.Items, "Id", "Name");
            return View();
        }

        // POST: CustomerItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,ItemId")] CustomerItem customerItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Customers"] = new SelectList(_context.Customers, "Id", "Name", customerItem.CustomerId);
            ViewData["Items"] = new SelectList(_context.Items, "Id", "Name", customerItem.ItemId);
            return View(customerItem);
        }


        // GET: CustomerItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerItem = await _context.CustomerItems.FindAsync(id);
            if (customerItem == null)
            {
                return NotFound();
            }

            ViewData["Customers"] = _context.Customers.ToList();
            ViewData["Items"] = _context.Items.ToList();
            return View(customerItem);
        }

        // POST: CustomerItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CustomerItem customerItem)
        {
            if (id != customerItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerItemExists(customerItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["Customers"] = _context.Customers.ToList();
            ViewData["Items"] = _context.Items.ToList();
            return View(customerItem);
        }

        // GET: CustomerItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerItem = await _context.CustomerItems
                .Include(ci => ci.Customer)
                .Include(ci => ci.Item)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (customerItem == null)
            {
                return NotFound();
            }

            return View(customerItem);
        }

        // POST: CustomerItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerItem = await _context.CustomerItems.FindAsync(id);
            if (customerItem != null)
            {
                _context.CustomerItems.Remove(customerItem);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerItemExists(int id)
        {
            return _context.CustomerItems.Any(e => e.Id == id);
        }
    }
}
