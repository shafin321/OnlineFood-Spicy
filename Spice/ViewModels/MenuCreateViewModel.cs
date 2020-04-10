using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.ViewModels
{
	public class MenuCreateViewModel
	{
		public MenuItem MenuItem { get; set; }
		public IEnumerable<Category> CategorisList  { get; set; }

		public IEnumerable<SelectListItem> CategoryDropdwnList 		
		{ get; set; }


        public int Id { get; set; }

        public double Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Spicyness { get; set; }
        public enum ESpicy { NA = 0, NotSpicy = 1, Spicy = 2, VerySpicy = 3 }



        public IFormFile Photo { get; set; }

     
        public int CategoryId { get; set; }


        public virtual Category Category { get; set; }
    }
}
