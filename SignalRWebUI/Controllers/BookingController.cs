using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.BookingDtos;
using System.Text;

namespace SignalRWebUI.Controllers
{
    public class BookingController : Controller
    {





        private readonly IHttpClientFactory _httpClientFactory;

        public BookingController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }




        // API'yi CONSUME ettiğimiz yer.  LİSTELEME KISMI
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient(); // istemci oluşturduk
            var responseMessage = await client.GetAsync("https://localhost:7113/api/Booking"); //GetAsync>verileri listelemek için 

            if (responseMessage.IsSuccessStatusCode) // başarılı ise
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); // Jsondan gelen içeriği string olarak okuduk
                var values = JsonConvert.DeserializeObject<List<ResultBookingDto>>(jsonData); // Jsondan (jsonData) gelen içeriği listeye çevirdik
                return View(values); // listeyi view'e gönderdik

            }
            return View(); // başarısız ise boş view döndürdük
        }




        // CREATE KISMI

        [HttpGet]
        public IActionResult CreateBooking()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingDto createBookingDto)
        {

            createBookingDto.Description = "Rezervasyon";

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createBookingDto); // parametreden gelen değeri Json'a çevirdik
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); // TR karakterler dahil olsun
            var responseMessage = await client.PostAsync("https://localhost:7113/api/Booking", stringContent); // PostAsync>veri eklemek için


            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
                
        }


        // DELETE İŞLEMİ
        public async Task<IActionResult> DeleteBooking(int id) // api kısmında da delethttp kısmında ("{id}") verdik
        {
            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.DeleteAsync($"https://localhost:7113/api/Booking/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }




        // UPDATE İŞLEMİ. ÖNCE ID İLE VERİYİ GETİRİP SONRA GÜNCELLEME İŞLEMİ YAPACAĞIZ
        public async Task<IActionResult> UpdateBooking(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7113/api/Booking/{id}"); // ilk önce güncellenecek veriyi getiriyoruz

            if (responseMessage.IsSuccessStatusCode)
            {

                // güncellenecek veriyi DTO aracılığıyla alıyoruz

                var jsonData = await responseMessage.Content.ReadAsStringAsync();// Jsondan gelen içeriği string olarak okuduk
                var values = JsonConvert.DeserializeObject<UpdateBookingDto>(jsonData); // Jsondan gelen içeriği listeye çevirdik
                // jsonData'dan gelen değerle , UpdateCategoryDto'yu Deserialize ettik.
                return View(values);
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UpdateBooking(UpdateBookingDto updateBookingDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateBookingDto); // parametreden gelen değeri (updateCategoryDto) Json'a çevirdik

            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); // TR karakterler dahil olsun
            var responseMessage = await client.PutAsync("https://localhost:7113/api/Booking", stringContent); // PutAsync>veri güncellemek için
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }




        // rezervasyon onaylama ve iptal etme işlemleri
        public async Task<IActionResult> BookingStatusApproved(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.GetAsync($"https://localhost:7113/api/Booking/BookingStatusApproved/{id}");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> BookingStatusCancelled(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.GetAsync($"https://localhost:7113/api/Booking/BookingStatusCancelled/{id}");
            return RedirectToAction("Index");

        }



    }
}
