namespace SignalRWebUI.Dtos.ProductDtos
{
    public class ResultProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool ProductStatus { get; set; }

        // Category ismine ihtiyacımız olduğu için burada CategoryName'i de ekledik
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }

    }
}



