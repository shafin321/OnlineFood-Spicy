using Spice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.ViewModels
{
	public class OrderDetailsCartViewModel
	{

		public List<ShoppingCart> ListCart { get; set; }
		public OrderHeader OrderHeader { get; set; }
		public MenuCreateViewModel ImageClass { get; set; }

	}
}
