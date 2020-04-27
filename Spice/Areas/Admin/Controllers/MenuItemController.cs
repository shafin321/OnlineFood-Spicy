using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spice.Data;
using Spice.Interface;
using Spice.Models;
using Spice.Utility;
using Spice.ViewModels;

namespace Spice.Areas.Admin.Controllers
{

	[Area("Admin")]
	public class MenuItemController : Controller
	{
		private readonly ICategory _category;
		private readonly ApplicationDbContext _db;
		private readonly IWebHostEnvironment _hostingEnvironment;
		public MenuItemController(ApplicationDbContext db, IWebHostEnvironment hostingEnvironment, ICategory category)
		{
			_db = db;
			_hostingEnvironment = hostingEnvironment;
			_category = category;
		}
		public IActionResult Index()
		{
			var manuitem = _db.MenuItems.Include(m => m.Category);
			return View(manuitem);
		}

		public IActionResult Create()
		{
			var model = new MenuItem();

			var viewmodel = new MenuCreateViewModel
			{
				CategorisList = _db.Categories,
				MenuItem = model,
				CategoryDropdwnList = _category.GetMemberDropDownList(),


			};

			return View(viewmodel);
		}

		[HttpPost]
		public IActionResult Create(MenuCreateViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				string uniqueFileName = null;

				// If the Photo property on the incoming model object is not null, then the user
				// has selected an image to upload.
				if (viewModel.Photo != null)
				{
					// The image must be uploaded to the images folder in wwwroot
					// To get the path of the wwwroot folder we are using the inject
					// HostingEnvironment service provided by ASP.NET Core
					var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
					// To make sure the file name is unique we are appending a new
					// GUID value and and an underscore to the file name
					uniqueFileName = Guid.NewGuid().ToString() + "_" + viewModel.Photo.FileName;
					var filePath = Path.Combine(uploadsFolder, uniqueFileName);
					// Use CopyTo() method provided by IFormFile interface to
					// copy the file to wwwroot/images folder
					viewModel.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
				}


				var model = new MenuItem
				{
					Id = viewModel.MenuItem.Id,
					Name = viewModel.MenuItem.Name,
					Image = uniqueFileName,
					Price = viewModel.MenuItem.Price,
					Category = viewModel.MenuItem.Category,
					CategoryId = viewModel.MenuItem.CategoryId,
					Description = viewModel.MenuItem.Description,
					Spicyness = viewModel.MenuItem.Spicyness


				};

				_db.MenuItems.Add(model);
				_db.SaveChanges();

				return RedirectToAction("Index");
			}

			return View(viewModel);
		}
	}
}