using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodFinder2.Models
{
    public partial class List
    {
        public int ListId { get; set; }
        public string ListName { get; set; }
        public List<ListDetail> ListDetails { get; set; }

        public System.DateTime ListDate { get; set; }

    }
}