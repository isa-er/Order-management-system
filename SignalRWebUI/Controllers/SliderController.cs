using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.FeatureDtos;
using SignalRWebUI.Dtos.SliderDtos;
using System.Text;

namespace SignalRWebUI.Controllers
{
    public class SliderController : Controller
    {





        private readonly IHttpClientFactory _httpClientFactory;

        public SliderController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }




        // API'yi CONSUME ettiğimiz yer.  LİSTELEME KISMI
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient(); // istemci oluşturduk
            var responseMessage = await client.GetAsync("https://localhost:7113/api/Slider"); //GetAsync>verileri listelemek için 

            if (responseMessage.IsSuccessStatusCode) // başarılı ise
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); // Jsondan gelen içeriği string olarak okuduk
                var values = JsonConvert.DeserializeObject<List<ResultSliderDto>>(jsonData); // Jsondan (jsonData) gelen içeriği listeye çevirdik
                return View(values); // listeyi view'e gönderdik

            }
            return View(); // başarısız ise boş view döndürdük
        }




        // CREATE KISMI

        [HttpGet]
        public IActionResult CreateSlider()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateSlider(CreateSliderDto createSliderDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createSliderDto); // parametreden gelen değeri Json'a çevirdik
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); // TR karakterler dahil olsun
            var responseMessage = await client.PostAsync("https://localhost:7113/api/Slider", stringContent); // PostAsync>veri eklemek için


            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();

        }


        // DELETE İŞLEMİ
        public async Task<IActionResult> DeleteSlider(int id) // api kısmında da delethttp kısmında ("{id}") verdik
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7113/api/Slider/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }




        // UPDATE İŞLEMİ. ÖNCE ID İLE VERİYİ GETİRİP SONRA GÜNCELLEME İŞLEMİ YAPACAĞIZ
        [HttpGet]
        public async Task<IActionResult> UpdateSlider(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7113/api/Slider/{id}"); // ilk önce güncellenecek veriyi getiriyoruz

            if (responseMessage.IsSuccessStatusCode)
            {

                // güncellenecek veriyi DTO aracılığıyla alıyoruz

                var jsonData = await responseMessage.Content.ReadAsStringAsync();// Jsondan gelen içeriği string olarak okuduk
                var values = JsonConvert.DeserializeObject<UpdateSliderDto>(jsonData); // Jsondan gelen içeriği listeye çevirdik
                // jsonData'dan gelen değerle , UpdateCategoryDto'yu Deserialize ettik.
                return View(values);
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UpdateSlider(UpdateSliderDto updateSliderDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateSliderDto); // parametreden gelen değeri (updateCategoryDto) Json'a çevirdik

            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); // TR karakterler dahil olsun
            var responseMessage = await client.PutAsync("https://localhost:7113/api/Slider", stringContent); // PutAsync>veri güncellemek için
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }




    }
}
