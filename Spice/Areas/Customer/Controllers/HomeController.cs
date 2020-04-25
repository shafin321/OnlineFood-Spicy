using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Spice.Data;
using Spice.Models;
using Spice.ViewModels;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Controllers
{
	[Area("Customer")]
	public class HomeController : Controller
	{

		private readonly ApplicationDbContext _db;

		public HomeController(ApplicationDbContext db)
		{
			_db = db;
		}

		public IActionResult Index()
		{

			var menu = _db.MenuItems.Include(m => m.Category);

			var viewmodel = new IndexViewModel
			{
				MenuList=menu,
				CategoryList=_db.Categories

			};
			return View(viewmodel);
		}


		public async Task<IActionResult> Details(int id)
		{
			var menuItem = await _db.MenuItems.Include(c => c.Category).Where(m=>m.Id==id).FirstOrDefaultAsync();

			ShoppingCart cartobj = new ShoppingCart
			{
				MenuItem=menuItem,
				MenuItemId=menuItem.Id,

			};

			return View(cartobj);
		}

		[HttpPost]
		public IActionResult Details()
		{
			if (ModelState.IsValid)
			{

			}

			return View();
		}
		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
