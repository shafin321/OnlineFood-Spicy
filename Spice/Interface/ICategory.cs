using Microsoft.AspNetCore.Mvc.Rendering;
using Spice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Interface
{
	public interface ICategory
	{
		IEnumerable<Category> GetAll();
		IEnumerable<SelectListItem> GetMemberDropDownList();
		Category Delete(int id);
	}
}
