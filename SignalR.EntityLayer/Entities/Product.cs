namespace SignalR.EntityLayer.Entities

{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool ProductStatus { get; set; }
        public int CategoryId { get; set; } // Foreign Key. her ürünün bir kategorisi vardır
        public Category Category { get; set; } // 1'e çok olduğunu belirtmek için
        public List<OrderDetail> OrderDetails { get; set; } // 1'e çok olduğunu belirtmek için

        public List<Basket> Baskets { get; set; }

    }
}
