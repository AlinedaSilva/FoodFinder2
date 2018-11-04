using FoodFinder2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace FoodFinder2.Controllers
{
    public class ShoppingCartController : Controller
    {
        FoodFinder2Context listDB = new FoodFinder2Context();

        public Func<decimal> CartTotal { get; private set; }



        // GET: ShoppingCart from shopping list but as as I am using a make request method might be incorrect
        public ActionResult Index() // will need to be changed!!! as it needs to direct to the MakeRequest Method
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            var viewModel = new ShoppingCartRemoveViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            return View(viewModel);
        }
        public ActionResult AddToCart(int id)
        {
            var addedProduct = listDB.products.Single(product => product.ID == id);
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(addedProduct);
            return RedirectToAction("Index");
        }
        // Post /ShoppingCart/RemoveFromCart/5
        [HttpPost]

        public ActionResult RemoveFromCart(int id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext); // might be incorrect as I need to get the info from the make request method
            string Name = listDB.Carts.Single(item => item.RecordId == id).Product.Name;
            int itemCount = cart.RemoveFromCart(id);
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(Name) + " Has been removed from the shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }
        //GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }
}

