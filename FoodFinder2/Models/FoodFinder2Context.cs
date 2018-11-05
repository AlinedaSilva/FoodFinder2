using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace FoodFinder2.Models
{
    public class FoodFinder2Context: DbContext
    {
        public FoodFinder2Context() : base("DefaultConnection")
        {
        }
        public DbSet<Product> products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<List> Lists { get; set; }
        public DbSet<ListDetail> ListDetails { get; set; }
    }
}