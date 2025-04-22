using SignalR.EntityLayer.Entities;

namespace SignalRapi.Models
{
    public class ResultBasketListWithProducts
    {
        public int BasketId { get; set; }
        public decimal Price { get; set; }
        public decimal Count { get; set; }
        public decimal TotalPrice { get; set; }
        public int ProductId { get; set; }
        public int MenuTableId { get; set; }

        // amacımız bu property'ye erişim sağlamak
        public string ProductName { get; set; }
    }
}
