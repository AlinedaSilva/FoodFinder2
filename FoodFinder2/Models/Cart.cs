using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodFinder2.Models
{
    public class Cart
    {
        public int RecordId { get; set; }
        public string CartId { get; set; }
        public long Id { get; set; } //product
        public int Count { get; set; }
        public System.DateTime DateCreated { get; set; }
        public virtual Product Product { get; set; }
    }
}