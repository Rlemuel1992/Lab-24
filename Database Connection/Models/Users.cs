using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Database_Connection.Models
{
	public class Users
	{
		[Key]
		public int ID { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }

		public Users() { }


	}

	public class UserDBContext : DbContext
	{
		public DbSet<Users> FakeUserLogin { get; set; }
	}
}