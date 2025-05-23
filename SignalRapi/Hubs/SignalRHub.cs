﻿

// Hub bir sunucu görevi görecek. biz dağıtım işlemini Hub sınıfım hangisiyse onun üzerinden sağlayacağız.

// Server kısmını oluşturmuş olduk . Client kısmı ise WebUI kısmı oluyor


using Microsoft.AspNetCore.SignalR;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;

namespace SignalRapi.Hubs
{
    public class SignalRHub:Hub
    {


        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IMoneyCaseService _moneyCaseService;
        private readonly IMenuTableService _menuTableService;
        private readonly IBookingService _bookingService;
        private readonly INotificationService _notificationService;




        public SignalRHub(ICategoryService categoryService, IProductService productService, IOrderService? orderService, IMoneyCaseService moneyCaseService, IMenuTableService menuTableService, IBookingService bookingService, INotificationService notificationService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _orderService = orderService;
            _moneyCaseService = moneyCaseService;
            _menuTableService = menuTableService;
            _bookingService = bookingService;
            _notificationService = notificationService;
        }

        //public static int clientCount { get; set; }
        static int clientCount=0 ;

        public async Task SendStatistics()
        {
            var value = _categoryService.TCategoryCount();
            await Clients.All.SendAsync("ReceiveCategoryCount", value);


            var value2 = _productService.TProductCount();
            await Clients.All.SendAsync("ReceiveProductCount", value2);

            var value3 = _categoryService.TActiveCategoryCount();
            await Clients.All.SendAsync("ReceiveActiveCategoryCount", value3);

            var value4 = _categoryService.TPassiveCategoryCount();
            await Clients.All.SendAsync("ReceivePassiveCategoryCount", value4);

            var value5 = _productService.TGetProductCountByCategoryName("Hamburger");
            await Clients.All.SendAsync("ReceiveGetProductCountByHamburger", value5);

            var value6 = _productService.TGetProductCountByCategoryName("İçecek");
            await Clients.All.SendAsync("ReceiveGetProductCountByDrink", value6);

            var value7 = _productService.TProductPriceAvg();
            await Clients.All.SendAsync("ReceiveProductPriceAvg", value7 + "₺");

            var value8 = _productService.TProductNamePriceByMax();
            await Clients.All.SendAsync("ReceiveProductNamePriceByMax", value8);

            var value9 = _productService.TProductNamePriceByMin();
            await Clients.All.SendAsync("ReceiveProductNamePriceByMin", value9);

            var value10 = _productService.TProductAvgPriceByName("Hamburger");
            await Clients.All.SendAsync("ReceiveProductAvgPriceByName", value10.ToString("0.00")+ "₺");

            var value11 = _orderService.TTotalOrderCount();
            await Clients.All.SendAsync("ReceiveTotalOrderCount", value11);

            var value12 = _orderService.TActiveOrderCount();
            await Clients.All.SendAsync("ReceiveActiveOrderCount", value12);

            var value13 = _orderService.TLastOrderPrice();
            await Clients.All.SendAsync("ReceiveLastOrderPrice", value13.ToString("0.00") + "₺");

            var value14 = _moneyCaseService.TTotalMoneyCaseAmount();
            await Clients.All.SendAsync("ReceiveTotalMoneyCaseAmount", value14.ToString("0.00") + "₺");

            var value15 = _orderService.TTodayTotalPrice();
            await Clients.All.SendAsync("ReceiveTodayTotalPrice", value15.ToString("0.00") + "₺");

            var value16 = _menuTableService.TMenuTableCount();
            await Clients.All.SendAsync("ReceiveMenuTableCount", value16);





        }

        public async Task SendProgres()
        {
            var value = _moneyCaseService.TTotalMoneyCaseAmount();
            await Clients.All.SendAsync("ReceiveTotalMoneyCaseAmount", value.ToString("0.00") + " TL");

            
            var value2 = _orderService.TActiveOrderCount();
            await Clients.All.SendAsync("ReceiveActiveOrderCount", value2);

            var value3 = _menuTableService.TMenuTableCount();
            await Clients.All.SendAsync("ReceiveMenuTableCount", value3);

            var value5 = _productService.TProductPriceAvg();
            await Clients.All.SendAsync("ReceiveProductPriceAvg", value5);

            var value6 = _productService.TProductAvgPriceByName("Hamburger");
            await Clients.All.SendAsync("ReceiveAvgPriceByHamburger", value6);

            var value7 = _productService.TGetProductCountByCategoryName("İçecek");
            await Clients.All.SendAsync("ReceiveProductCountByCategoryNameDrink", value7);

            var value8 = _orderService.TTotalOrderCount();
            await Clients.All.SendAsync("ReceiveTotalOrderCount", value8);

            var value9 = _productService.TProductPriceBySteakBurger();
            await Clients.All.SendAsync("ReceiveProductPriceBySteakBurger", value9);

            var value10 = _productService.TTotalPriceByDrinkCategory();
            await Clients.All.SendAsync("ReceiveTotalPriceByDrinkCategory", value10);

            var value11 = _productService.TTotalPriceBySaladCategory();
            await Clients.All.SendAsync("ReceiveTotalPriceBySaladCategory", value11);



        }

        public async Task GetBookingList()
        {
            var values = _bookingService.TGetListAll();
            await Clients.All.SendAsync("ReceiveBookingList", values);
        }

        public async Task SendNotification()
        {
            var value = _notificationService.TNotificationCountByStatusFalse();
            await Clients.All.SendAsync("ReceiveNotificationCountByFalse", value);


            var notificationListByFalse=_notificationService.TGetAllNotificationByFalse();
            await Clients.All.SendAsync("ReceiveNotificationListByFalse",notificationListByFalse);

        }

        public async Task GetMenuTablesStatus()
        {
            var value = _menuTableService.TGetListAll();
            await Clients.All.SendAsync("ReceiveMenuTableStatus",value);
        }

        public async Task SendMessage(string user, string message) { 
            
            await Clients.All.SendAsync("ReceiveMessage", user, message); 

        }

        public override async Task OnConnectedAsync() // Hub'a birisi bağlandığında tetiklenir
        {
            clientCount++;
            await Clients.All.SendAsync("ReceiveClientCount", clientCount);
            await base.OnConnectedAsync();
        }


        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            clientCount--;
            await Clients.All.SendAsync("ReceiveClientCount", clientCount);
            await base.OnDisconnectedAsync(exception);

        }





    }
}
