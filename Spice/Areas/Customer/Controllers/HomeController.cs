using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Spice.Data;
using Spice.Models;
using Spice.ViewModels;
using System.Diagnostics;

namespace Spice.Controllers
{
	[Area("Customer")]
	public class HomeController : Controller
	{

		private readonly ApplicationDbContext _context;

		public HomeController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{

			var menu = _context.MenuItems.Include(m => m.Category);

			var viewmodel = new IndexViewModel
			{
				MenuList=menu,
				CategoryList=_context.Categories

			};
			return View(viewmodel);
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
