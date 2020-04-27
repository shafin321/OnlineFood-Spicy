using System.Collections.Generic;

using Spice.Models;

namespace Spice.ViewModels
{
	public class IndexViewModel
	{
		public IEnumerable<MenuItem> MenuItems { get; set; }

		public IEnumerable<Category> Categories { get; set; }

		public MenuCreateViewModel ImageClass { get; set; }

	}
}
