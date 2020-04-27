using System;
using System.ComponentModel.DataAnnotations;

namespace Spice.Models
{
	public class MenuItem
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }
		public string Description { get; set; }

		public string Spicyness { get; set; }
		public enum ESpicy { NA = 0, NotSpicy = 1, Spicy = 2, VerySpicy = 3 }

		public string Image { get; set; }

		public int CategoryId { get; set; }

		public virtual Category Category { get; set; }

		[Range(1, int.MaxValue, ErrorMessage = " Price should be greater than ${1}")]
		public double Price { get; set; }

	}
}
