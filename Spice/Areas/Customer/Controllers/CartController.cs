using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spice.Data;
using Spice.Models;
using Spice.ViewModels;

namespace Spice.Areas.Customer.Controllers
{

	[Area("Customer")]
	public class CartController : Controller
	{
		private readonly ApplicationDbContext _db;
		private readonly UserManager<IdentityUser> _userManager;

		//   [BindProperty]
		//    public OrderDetailsCartViewModel detailCart { get; set; }
		public CartController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
		{
			_db = db;
			_userManager = userManager;
		}
		public IActionResult Index()
		{
			// var orderheader = new OrderHeader();
			var detailCart = new OrderDetailsCartViewModel
			{
				OrderHeader = new OrderHeader()
			};
			detailCart.OrderHeader.OrderTotalOriginal = 0;

			var userId = _userManager.GetUserId(User);

			var cart = _db.ShoppingCarts.Where(c => c.UserId == userId);

			if (cart != null)
			{
				detailCart.ListCart = cart.ToList();
			}

			foreach (var list in detailCart.ListCart)
			{
				list.MenuItem = _db
								.MenuItems
								.FirstOrDefault(m => m.Id == list.MenuItemId);

				if (list.MenuItem != null)
					list.MenuItem.Image = $"/images/{list.MenuItem.Image}";

				detailCart.OrderHeader.OrderTotalOriginal += list.MenuItem.Price * list.Count;

			}


			return View(detailCart);
		}
	}
}