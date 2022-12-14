
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using GroceryManagement.web.Data;
using GroceryManagement.web.Models;
using Microsoft.AspNetCore.Authorization;

namespace GroceryManagement.web.Areas.User1.Controllers
{
    [Area("User1")]
    [Authorize(Roles = "AppAdmin")]


    public class CategoriesController : Controller
    {  //exp  injected 
        private readonly ApplicationDbContext _context;
        // context - all table data
        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: User1/Categories
       
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }
        public async Task<IActionResult> Index2()
        {
            return View(await _context.Categories.ToListAsync());
        }
        public async Task<IActionResult> Index3()
        {
            return View(await _context.Categories.ToListAsync());
        }
        public async Task<IActionResult> Index4()
        {
            return View(await _context.Categories.ToListAsync());
        }
        public async Task<IActionResult> Index5()
        {
            return View(await _context.Categories.ToListAsync());
        }
        public async Task<IActionResult> Index6()
        {
            return View(await _context.Categories.ToListAsync());
        }
        public async Task<IActionResult> Index7()
        {
            return View(await _context.Categories.ToListAsync());
        }
        public async Task<IActionResult> Index8()
        {
            return View(await _context.Categories.ToListAsync());
        }
        public async Task<IActionResult> Index9()
        {
            return View(await _context.Categories.ToListAsync());
        }
        public async Task<IActionResult> Index10()
        {
            return View(await _context.Categories.ToListAsync());
        }
        public async Task<IActionResult> Index11()
        {
            return View(await _context.Categories.ToListAsync());
        }
        public async Task<IActionResult> Index12()
        {
            return View(await _context.Categories.ToListAsync());
        }
        public async Task<IActionResult> Index13()
        {
            return View(await _context.Categories.ToListAsync());
        }
        public async Task<IActionResult> Index14() //iaction - interface
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: User1/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.IcId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: User1/Categories/Create
        public IActionResult Create()   //shows views
        {
            return View();
        }

        // POST: User1/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]   //update database
        public async Task<IActionResult> Create([Bind("IcId,Categories")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: User1/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: User1/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IcId,Categories")] Category category)
        {
            if (id != category.IcId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.IcId))
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
            return View(category);
        }

        // GET: User1/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.IcId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: User1/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.IcId == id);
        }
    }
}
