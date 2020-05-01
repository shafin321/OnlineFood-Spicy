using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Spice.Data;
using Spice.Models;
using Spice.ViewModels;

namespace Spice.Controllers
{
	[Area("Customer")]
	public class HomeController : Controller
	{
		private readonly ApplicationDbContext _db;
		private readonly UserManager<IdentityUser> _userManager;

		public HomeController(
			ApplicationDbContext db,
			UserManager<IdentityUser> userManager
		)
		{
			_db = db;
			_userManager = userManager;
		}

		public IActionResult Index()
		{
			var menu = _db.MenuItems.Include(m => m.Category);

			var viewmodel = new IndexViewModel
			{
				MenuItems = menu,
				Categories = menu.Select(m => m.Category)
			};
			return View(viewmodel);
		}

		[Authorize]
		[HttpGet]
		public async Task<ActionResult> Details(int id)
		{
			var menuItemFromDb = await _db
								.MenuItems
								.Include(m => m.Category)
								.SingleOrDefaultAsync(m => m.Id == id);

			return View(menuItemFromDb);
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Details(MenuItem menuItem)
		{
			if (ModelState.IsValid)
			{
				var userId = _userManager.GetUserId(User);// currently loged in user 

				var cartItems = await _db
									.ShoppingCarts
									.Where(c => c.UserId == userId)
									.ToListAsync();

				var matchingCartItem = cartItems
									.SingleOrDefault(c
										=> c.MenuItemId == menuItem.Id
									);

				if (matchingCartItem == null)
				{
					_db.ShoppingCarts.Add(new ShoppingCart()
					{
						Count = 1,
						MenuItemId = menuItem.Id,
						UserId = userId
					});
				}
				else
				{
					matchingCartItem.Count += 1;
				}
				await _db.SaveChangesAsync();

				cartItems = await _db
							.ShoppingCarts
							.Where(c => c.UserId == userId)
							.ToListAsync();
				var count = cartItems.Count;
				HttpContext.Session.SetInt32("Cart", count);

				return RedirectToAction(
					"Index",
					controllerName: "Home",
					routeValues: new { area = "Customer" });
			}
			else
			{
				//var menuItemFromDb = await _db.MenuItems.Include(m => m.Category).Where(m => m.Id == CartObject.MenuItemId).FirstOrDefaultAsync();

				//var cartObj = new ShoppingCart()
				//{
				//	MenuItem = menuItemFromDb,
				//	MenuItemId = menuItemFromDb.Id
				//};

				return View(menuItem);
			}
		}

		public IActionResult Privacy()
			=> View();

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
			=> View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
