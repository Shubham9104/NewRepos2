using GroceryManagement.web.Areas.User2.ViewModels;
using GroceryManagement.web.Data;
using GroceryManagement.web.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.Rendering;
//using Restaurant.Web.Areas.OrderNow.ViewModels;
//using Restaurant.Web.Data;
//using Restaurant.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryManagement.web.Areas.User2.Controllers
{
    [Area("User2")]
    public class HomeController : Controller

    {
        private readonly ApplicationDbContext _context;
        ShowViewModel viewmodel = new ShowViewModel();

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Categories.ToList());
        }

        public IActionResult Inside(int? Id)
        {
            var items = _context.Items.Where(m => m.CategoryID == Id);
            return View(items.ToList());
        }
       
        public IActionResult BookOrder(int? Id)
        {
            return View();
        }



    }
}
