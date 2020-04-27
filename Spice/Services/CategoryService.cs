using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc.Rendering;

using Spice.Data;
using Spice.Interface;
using Spice.Models;

namespace Spice.Services
{
	public class CategoryService : ICategory
	{
		private readonly ApplicationDbContext _context;
		public CategoryService(ApplicationDbContext context) 
			=> _context = context;

		public IEnumerable<Category> GetAll()
			=> _context.Categories;

		public IEnumerable<SelectListItem> GetMemberDropDownList()
			=> _context.Categories.Select(i => new SelectListItem()
			{
				Text = i.Name,
				Value = i.Id.ToString()
			});

		public Category Delete(int id)
		{
			var model = _context.Categories.Find(id);

			_context.Remove(model);
			_context.SaveChanges();
			return model;
		}
	}
}
