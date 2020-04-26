using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Spice.Data;
using Spice.Models;
using Spice.ViewModels;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
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
				MenuItem=menu,
				Category=_db.Categories

			};
			return View(viewmodel);
		}

		[HttpGet]
		public  ActionResult Details(int id)
		{
			var menuItemFromDb = _db.MenuItems.Include(m => m.Category).FirstOrDefault(m => m.Id == id);

			ShoppingCart cartobj = new ShoppingCart
			{
				MenuItem = menuItemFromDb,
				MenuItemId = menuItemFromDb.Id,

			};

			return View(cartobj);
		}

		[HttpPost]
		public async Task<IActionResult> Details(ShoppingCart CartObject)
		{
			CartObject.Id = 0;
			if (ModelState.IsValid)
			{
				var claimsIdentity = (ClaimsIdentity)this.User.Identity;
				var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
				CartObject.ApplicationUserId = claim.Value;

				ShoppingCart cartFromDb = await _db.ShoppingCarts.Where(c => c.ApplicationUserId == CartObject.ApplicationUserId
												&& c.MenuItemId == CartObject.MenuItemId).SingleOrDefaultAsync();

				if (cartFromDb == null)
				{
					await _db.ShoppingCarts.AddAsync(CartObject);
				}
				else
				{
					cartFromDb.Count = cartFromDb.Count + CartObject.Count;
				}
				await _db.SaveChangesAsync();

				var count = _db.ShoppingCarts.Where(c => c.ApplicationUserId == CartObject.ApplicationUserId).ToList().Count();
				HttpContext.Session.SetInt32("Cart", count);

				return RedirectToAction("Index");
			}
			else
			{

				var menuItemFromDb = await _db.MenuItems.Include(m => m.Category).Where(m => m.Id == CartObject.MenuItemId).FirstOrDefaultAsync();

				ShoppingCart cartObj = new ShoppingCart()
				{
					MenuItem = menuItemFromDb,
					MenuItemId = menuItemFromDb.Id
				};

				return View(cartObj);
			}
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
