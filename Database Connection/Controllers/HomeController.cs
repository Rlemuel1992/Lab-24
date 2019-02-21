using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Database_Connection.Models;
using Dapper;

namespace Database_Connection.Controllers
{
	public class HomeController : Controller
	{
		public List<Products> ShoppingCart = new List<Products>();

		public ActionResult Index()
		{

			List<Products> AllProducts = new List<Products>();
			using (SqlConnection connection = new SqlConnection("Data Source=LAPTOP-6M65FED7\\SQLEXPRESS;Initial Catalog=Database_Connection.Models.ItemDBContext;Integrated Security=True"))
			{
				var selectQueryString = "SELECT * FROM Products";
				AllProducts = connection.Query<Products>(selectQueryString).ToList();

			}
			Session["AllProducts"] = AllProducts;
			return View();
		}

		public ActionResult Login()
		{
			return View();
		}

		public ActionResult Register()
		{
			return View("Create", "Users");
		}


		public ActionResult ValidateLogin()
		{


			return View();
		}

		public ActionResult Shop()
		{
			//var ShopDB = new ItemDBContext();


			return View();
		}

		public ActionResult AddItem(int Quantity, string itemName)
		{
			ItemDBContext db = new ItemDBContext();
			
			

			if (Session["ShoppingCart"] != null)
			{
				ShoppingCart = (List<Products>)Session["ShoppingCart"];
			}
			foreach (Products item in db.Products)
			{
				if (item.ProductName == itemName)
				{
					for (int i = 0; i < Quantity; i++)
					{
						ShoppingCart.Add(item);
					}

				}
			}
			Session["ShoppingCart"] = ShoppingCart;
			return RedirectToAction("Shop");
		}

		public ActionResult CheckOut()
		{
			if (Session["ShoppingCart"] != null)
			{
				ShoppingCart = (List<Products>)Session["ShoppingCart"];
				ViewBag.ShoppingList = ShoppingCart;
				ViewBag.CurrentUser = Session["CurrentUser"];
				double SubTotal = 0;
				double TaxRate = 1.06;
				double GrandTotal = 0;

				foreach (var item in ShoppingCart)
				{
					SubTotal += item.Price;
				}
				GrandTotal = SubTotal * TaxRate;
				ViewBag.GrandTotal = Math.Round(GrandTotal, 2);
				return View();
			}
			else
				return RedirectToAction("Shop");
		}
		public ActionResult RemoveItem(string id)
		{
			ShoppingCart = (List<Products>)Session["ShoppingCart"];
			int index = IsExist(id);
			ShoppingCart.RemoveAt(index);
			Session["ShoppingCart"] = ShoppingCart;
			return RedirectToAction("CheckOut");
		}
		public ActionResult RemoveAll()
		{
			Session["ShoppingCart"] = null;
			return View("Shop");
		}
		private int IsExist(string id)
		{
			ShoppingCart = (List<Products>)Session["ShoppingCart"];
			for (int i = 0; i < ShoppingCart.Count; i++)
				if (ShoppingCart[i].ProductName.Equals(id))
					return i;
			return -1;
		}

	}
}