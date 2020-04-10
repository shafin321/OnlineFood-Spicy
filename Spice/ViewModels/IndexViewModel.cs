using Spice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.ViewModels
{
	public class IndexViewModel
	{
		public IEnumerable<MenuItem> MenuList { get; set; }

		public IEnumerable<Category> CategoryList{ get; set; }

		public MenuCreateViewModel ImageClass { get; set; }

	}
}
