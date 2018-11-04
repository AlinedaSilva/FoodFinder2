namespace FoodFinder2.Models
{
    public class ListDetail
    {
        public int ListDetailId { get; set; }
        public int ListId { get; set; }
        public long Id { get; set; }// product id
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }
        public decimal Price { get;  set; } // product price
        public virtual List List { get; set; }

    }
}
