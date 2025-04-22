using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.ContactDtos;
using System.Text;

namespace SignalRWebUI.Controllers
{
    public class ContactController : Controller
    {


        private readonly IHttpClientFactory _httpClientFactory;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }




        // API'yi CONSUME ettiğimiz yer.  LİSTELEME KISMI
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient(); // istemci oluşturduk
            var responseMessage = await client.GetAsync("https://localhost:7113/api/Contact"); //GetAsync>verileri listelemek için 

            if (responseMessage.IsSuccessStatusCode) // başarılı ise
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); // Jsondan gelen içeriği string olarak okuduk
                var values = JsonConvert.DeserializeObject<List<ResultContactDto>>(jsonData); // Jsondan (jsonData) gelen içeriği listeye çevirdik
                return View(values); // listeyi view'e gönderdik

            }
            return View(); // başarısız ise boş view döndürdük
        }




        // CREATE KISMI

        [HttpGet]
        public IActionResult CreateContact()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createContactDto); // parametreden gelen değeri Json'a çevirdik
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); // TR karakterler dahil olsun
            var responseMessage = await client.PostAsync("https://localhost:7113/api/Contact", stringContent); // PostAsync>veri eklemek için


            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();

        }


        // DELETE İŞLEMİ
        public async Task<IActionResult> DeleteContact(int id) // api kısmında da delethttp kısmında ("{id}") verdik
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7113/api/Contact/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }




        // UPDATE İŞLEMİ. ÖNCE ID İLE VERİYİ GETİRİP SONRA GÜNCELLEME İŞLEMİ YAPACAĞIZ
        [HttpGet]
        public async Task<IActionResult> UpdateContact(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7113/api/Contact/{id}"); // ilk önce güncellenecek veriyi getiriyoruz

            if (responseMessage.IsSuccessStatusCode)
            {

                // güncellenecek veriyi DTO aracılığıyla alıyoruz

                var jsonData = await responseMessage.Content.ReadAsStringAsync();// Jsondan gelen içeriği string olarak okuduk
                var values = JsonConvert.DeserializeObject<UpdateContactDto>(jsonData); // Jsondan gelen içeriği listeye çevirdik
                // jsonData'dan gelen değerle , UpdateCategoryDto'yu Deserialize ettik.
                return View(values);
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UpdateContact(UpdateContactDto updateContactDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateContactDto); // parametreden gelen değeri (updateCategoryDto) Json'a çevirdik

            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); // TR karakterler dahil olsun
            var responseMessage = await client.PutAsync("https://localhost:7113/api/Contact", stringContent); // PutAsync>veri güncellemek için
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }



    }
}
