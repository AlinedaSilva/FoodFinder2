using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodFinder2.Models
{
    public class ShoppingCart
    {
        FoodFinder2Context listDB = new FoodFinder2Context();
        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }
        public void AddToCart(Product Product)
        {
            var cartItem = listDB.Carts.SingleOrDefault(c => c.CartId == ShoppingCartId && c.Id == Product.ID);
            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    Id = Product.ID,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                listDB.Carts.Add(cartItem);
            }
            else {
                cartItem.Count++;
            }
            listDB.SaveChanges();

        }
        public int RemoveFromCart(int id)
        {
            var cartItem = listDB.Carts.Single(cart => cart.CartId == ShoppingCartId && cart.RecordId == id);
            int itemCount = 0;
            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    listDB.Carts.Remove(cartItem);
                }
                listDB.SaveChanges();

            }
            return itemCount;
        }
        public void EmptyCart()
        {
            var cartItems = listDB.Carts.Where(cart => cart.CartId == ShoppingCartId);
            foreach (var cartItem in cartItems)
            {
                listDB.Carts.Remove(cartItem);
            }
            listDB.SaveChanges();
        }
        public List<Cart> GetCartItems()
        {
            return listDB.Carts.Where(cart => cart.CartId == ShoppingCartId).ToList();
        }
        public int GetCount()
        {
            int? count = (from cartItems in listDB.Carts where cartItems.CartId == ShoppingCartId select (int?)cartItems.Count).Sum();
            return count ?? 0;
        }
        public decimal GetTotal()
        {
            decimal? total = (from cartItems in listDB.Carts where cartItems.CartId == ShoppingCartId select (int?)cartItems.Count * cartItems.Product.Price).Sum();
            return total ?? decimal.Zero;
        }
        public int CreateList(List list)
        {
            decimal listTotal = 0;
            var cartItems = GetCartItems();
            foreach (var item in cartItems)
            {
                var listDetail = new ListDetail
                {
                    Id = item.Id,
                    ListId = list.ListId,
                    Price = item.Product.Price, //might cause a problem 
                    Quantity = item.Count
                };
                listTotal += (item.Count * item.Product.Price);

                listDB.ListDetails.Add(listDetail);
            }

            //list.Total = listTotal; // not working there is no total in list
            listDB.SaveChanges();

            EmptyCart();

            return list.ListId;

        }
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }
        public void MigrateCart(string userName)
        {
            var shoppingCart = listDB.Carts.Where(c => c.CartId == ShoppingCartId);
            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            listDB.SaveChanges();
        }
    }
}

    



