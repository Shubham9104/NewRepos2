using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GroceryManagement.web.Data;
using GroceryManagement.web.Models;

namespace GroceryManagement.web.Areas.User1.Controllers
{
    [Area("User1")]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: User1/Orders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Orders.Include(o => o.Customers).Include(o => o.Item).Include(o => o.PaymentMethods);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: User1/Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customers)
                .Include(o => o.Item)
                .Include(o => o.PaymentMethods)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: User1/Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerAddress");
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemName");
            ViewData["PaymentId"] = new SelectList(_context.PaymentMethods, "PaymentMethodId", "PaymentMethodName");
            return View();
        }

        // POST: User1/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,Date,ItemId,Quantity,PaymentId,Price,OrderPlaced")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerAddress", order.CustomerId);
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemName", order.ItemId);
            ViewData["PaymentId"] = new SelectList(_context.PaymentMethods, "PaymentMethodId", "PaymentMethodName", order.PaymentId);
            return View(order);
        }

        // GET: User1/Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerAddress", order.CustomerId);
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemName", order.ItemId);
            ViewData["PaymentId"] = new SelectList(_context.PaymentMethods, "PaymentMethodId", "PaymentMethodName", order.PaymentId);
            return View(order);
        }

        // POST: User1/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,Date,ItemId,Quantity,PaymentId,Price,OrderPlaced")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerAddress", order.CustomerId);
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemName", order.ItemId);
            ViewData["PaymentId"] = new SelectList(_context.PaymentMethods, "PaymentMethodId", "PaymentMethodName", order.PaymentId);
            return View(order);
        }

        // GET: User1/Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customers)
                .Include(o => o.Item)
                .Include(o => o.PaymentMethods)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: User1/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
