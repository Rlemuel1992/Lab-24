using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Database_Connection.Models
{
	public class Products
	{
		public int ID { get; set; }
		public string ProductName { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }
		public int Quantity { get; set; }

	}

	public class ItemDBContext : DbContext
	{
		public DbSet<Users> ItemsList { get; set; }

		public DbSet<Products> Products { get; set; }
	}
}