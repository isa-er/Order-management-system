using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.BasketDtos;
using SignalRWebUI.Dtos.CategoryDtos;
using SignalRWebUI.Dtos.ProductDtos;
using SignalRWebUI.Views.Shared.Components;
using System.Text;

namespace SignalRWebUI.Controllers
{
    public class MenuController : Controller
    {



        private readonly IHttpClientFactory _httpClientFactory;
        public MenuController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<ResultProductDto> products { get; private set; }

        public async Task<IActionResult> Index(int id)
        {
            ViewBag.v = id;

            var client = _httpClientFactory.CreateClient(); // istemci oluşturduk


            // Ürünler için liste oluştur
            var products = new List<ResultProductDto>();
            var productResponse = await client.GetAsync("https://localhost:7113/api/Product");

            if (productResponse.IsSuccessStatusCode)
            {
                var productJsonData = await productResponse.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<ResultProductDto>>(productJsonData);
            }





            // Kategoriler için liste oluştur
            var categories = new List<ResultCategoryDto>();
            var categoryResponse = await client.GetAsync("https://localhost:7113/api/Category");

            if (categoryResponse.IsSuccessStatusCode)
            {
                var categoryJsonData = await categoryResponse.Content.ReadAsStringAsync();
                categories = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(categoryJsonData);
            }

            // ViewModel Oluştur ve Doldur
            var viewModel = new ProductCategoryViewModel
            {
                Products = products,
                Categories = categories
            };

            return View(viewModel);
        }




        [HttpPost]

        public async Task<IActionResult> AddBasket(int id, int menuTableId)
        {

            if (menuTableId == 0)
            {
                return BadRequest("MenuTableId 0 geliyor");
            }


            CreateBasketDto createBasketDto = new CreateBasketDto
            {

                ProductId = id,
                MenuTableId = menuTableId // gelen menutableId burada kullanılıyor
            };



            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createBasketDto); // parametreden gelen değeri Json'a çevirdik
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); // TR karakterler dahil olsun
            var responseMessage = await client.PostAsync("https://localhost:7113/api/Basket", stringContent);


            var client2 = _httpClientFactory.CreateClient();
            await client2.GetAsync("https://localhost:7113/api/MenuTables/ChangeMenuTableStatusToTrue?id="+menuTableId);



            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return Json(createBasketDto);
        }





    }
}
