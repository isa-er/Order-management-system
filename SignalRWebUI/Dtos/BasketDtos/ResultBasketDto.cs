﻿using SignalR.EntityLayer.Entities;

namespace SignalRWebUI.Dtos.BasketDtos
{
    public class ResultBasketDto
    {
        public int BasketId { get; set; }
        public decimal Price { get; set; }
        public decimal Count { get; set; }
        public decimal TotalPrice { get; set; }


        public int ProductId { get; set; }
        // Product Product { get; set; }


        public int MenuTableId { get; set; }
        //public MenuTable MenuTable { get; set; }


        public string ProductName { get; set; }

    }
}
