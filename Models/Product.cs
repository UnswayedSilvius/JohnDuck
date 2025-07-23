namespace JohnDuck.Models
{
    public class Product
    {
        public int Id { get; set; } // Primary key
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int StockQty { get; set; }
        public string Unit { get; set; } // e.g. kg, pcs, ltr
    }
}
