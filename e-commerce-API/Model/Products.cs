namespace e_commerce_API.Model.Products
{
    public class Products
    {

        public int Id { get; set; }
        public required string Title { get; set; }
        public decimal Price { get; set; }
        public required string Description { get; set; }
        public required string Category { get; set; }
        public required string ImageUrl { get; set; }
        public decimal RatingRate { get; set; }
        public int RatingCount { get; set; }
    }
}
