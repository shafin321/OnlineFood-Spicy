﻿using Spice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.ViewModels
{
	public class IndexViewModel
	{
		public IEnumerable<MenuItem> MenuItem { get; set; }

		public IEnumerable<Category> Category{ get; set; }

		public MenuCreateViewModel ImageClass { get; set; }

	}
}
