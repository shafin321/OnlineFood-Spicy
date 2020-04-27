﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Spice.Models;

namespace Spice.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		public DbSet<Category> Categories { get; set; }
		public DbSet<MenuItem> MenuItems { get; set; }
		public DbSet<ShoppingCart> ShoppingCarts { get; set; }
	}
}
