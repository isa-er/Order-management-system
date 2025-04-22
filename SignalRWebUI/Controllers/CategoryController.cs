


using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.CategoryDtos;
using System.Collections.Specialized;
using System.Text;

namespace SignalRWebUI.Controllers
{
    public class CategoryController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }




        // API'yi CONSUME ettiğimiz yer.  LİSTELEME KISMI
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient(); // istemci oluşturduk
            var responseMessage = await client.GetAsync("https://localhost:7113/api/Category"); //GetAsync>verileri listelemek için 

            if(responseMessage.IsSuccessStatusCode) // başarılı ise
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); // Jsondan gelen içeriği string olarak okuduk
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData); // Jsondan (jsonData) gelen içeriği listeye çevirdik
                return View(values); // listeyi view'e gönderdik

            }
            return View(); // başarısız ise boş view döndürdük
        }




        // CREATE KISMI

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            
            createCategoryDto.Status = true; // En başta Status'un durumunu true yaptık


            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCategoryDto); // parametreden gelen değeri Json'a çevirdik
            StringContent stringContent= new StringContent(jsonData,Encoding.UTF8,"application/json"); // TR karakterler dahil olsun
            var responseMessage = await client.PostAsync("https://localhost:7113/api/Category", stringContent); // PostAsync>veri eklemek için


            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();

        }


        // DELETE İŞLEMİ
        public async Task<IActionResult> DeleteCategory(int id) // api kısmında da delethttp kısmında ("{id}") verdik
        {
            var client = _httpClientFactory.CreateClient();
            //var responseMessage = await client.DeleteAsync("https://localhost:7113/api/Category/"+id); // DeleteAsync>veri silmek için
            //var responseMessage = await client.DeleteAsync($"https://localhost:7113/api/Category?id={id}");
            var responseMessage = await client.DeleteAsync($"https://localhost:7113/api/Category/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }




        // UPDATE İŞLEMİ. ÖNCE ID İLE VERİYİ GETİRİP SONRA GÜNCELLEME İŞLEMİ YAPACAĞIZ
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7113/api/Category/{id}"); // ilk önce güncellenecek veriyi getiriyoruz

            if (responseMessage.IsSuccessStatusCode)
            {

                // güncellenecek veriyi DTO aracılığıyla alıyoruz

                var jsonData = await responseMessage.Content.ReadAsStringAsync();// Jsondan gelen içeriği string olarak okuduk
                var values = JsonConvert.DeserializeObject<UpdateCategoryDto>(jsonData); // Jsondan gelen içeriği listeye çevirdik
                // jsonData'dan gelen değerle , UpdateCategoryDto'yu Deserialize ettik.
                return View(values);
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateCategoryDto); // parametreden gelen değeri (updateCategoryDto) Json'a çevirdik

            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); // TR karakterler dahil olsun
            var responseMessage = await client.PutAsync("https://localhost:7113/api/Category", stringContent); // PutAsync>veri güncellemek için
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }









    }
}
