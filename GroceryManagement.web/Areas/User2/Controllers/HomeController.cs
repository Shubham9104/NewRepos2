using GroceryManagement.web.Areas.User2.ViewModels;
using GroceryManagement.web.Data;
using GroceryManagement.web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using Restaurant.Web.Areas.OrderNow.ViewModels;
//using Restaurant.Web.Data;
//using Restaurant.Web.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryManagement.web.Areas.User2.Controllers
{
    [Area("User2")]
    [Authorize(Roles = "AppUser")]

    public class HomeController : Controller

    {
        private readonly ApplicationDbContext _context;
        private static int _customerId;
        private static int _itemId;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: User1/Customers/Create
        public IActionResult Index()
        {
            return View();
        }

        // POST: User2/Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("CustomerId,CustomerName,CustomerAddress,MobileNumber,Email")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                _customerId = customer.CustomerId;
                return RedirectToAction(nameof(ItemList));
            }
            return View(customer);
        }


        public IActionResult ItemList()
        {
            return View(_context.Items.ToList());
        }

        public IActionResult BookOrder(int id)

        {
           _itemId = id;
            //ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName");
            //ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemName");
            ViewData["PaymentId"] = new SelectList(_context.PaymentMethods, "PaymentMethodId", "PaymentMethodName");
            return View();
        }

        // POST: User1/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookOrder([Bind("Date,Quantity,PaymentId,Price,OrderPlaced")] Order order)
        {
            order.CustomerId = _customerId;
            order.ItemId = _itemId;
            order.Price = 29;
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();

                //return RedirectToAction(nameof(Index1));
                return RedirectToAction("Details", new { id = order.Id });
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName", order.CustomerId);
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemName", order.ItemId);
            ViewData["PaymentId"] = new SelectList(_context.PaymentMethods, "PaymentMethodId", "PaymentMethodName", order.PaymentId);
            return View(order);
        }

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



    }
}
