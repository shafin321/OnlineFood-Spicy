using System;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

namespace Spice.Models
{
	public class ShoppingCart
	{
		[Range(1, int.MaxValue, ErrorMessage = "Please enter a value greater than or equal to {1}")]
		public int Count { get; set; }

		public string UserId { get; set; }

		public virtual IdentityUser User { get; set; }

		public int Id { get; set; }

		public int MenuItemId { get; set; }

		public virtual MenuItem MenuItem { get; set; }

		public ShoppingCart()
			=> Count = 1;
	}
}
