using Microsoft.AspNetCore.Mvc.Rendering;
using Spice.Data;
using Spice.Interface;
using Spice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Services
{
	

	public class CategoryService: ICategory
	{
		private readonly ApplicationDbContext _context;
		public CategoryService(ApplicationDbContext context)
		{
			_context = context;
		}

		public IEnumerable<Category> GetAll()
		{
		return	_context.Categories;
		}

		public IEnumerable<SelectListItem> GetMemberDropDownList()
		{
			return _context.Categories.Select(i => new SelectListItem()
			{

				Text = i.Name,
				Value = i.Id.ToString()
			}

			);
		}
	}
}
