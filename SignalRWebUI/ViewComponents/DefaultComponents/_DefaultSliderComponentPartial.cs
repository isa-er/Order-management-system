using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.SliderDtos;

namespace SignalRWebUI.ViewComponents.DefaultComponents
{
    public class _DefaultSliderComponentPartial:ViewComponent
    {



        private readonly IHttpClientFactory _httpClientFactory;

        public _DefaultSliderComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient(); // istemci oluşturduk
            var responseMessage = await client.GetAsync("https://localhost:7113/api/Slider"); //GetAsync>verileri listelemek için 


            var jsonData = await responseMessage.Content.ReadAsStringAsync(); // Jsondan gelen içeriği string olarak okuduk
            var values = JsonConvert.DeserializeObject<List<ResultSliderDto>>(jsonData); // Jsondan (jsonData) gelen içeriği listeye çevirdik
            return View(values); // listeyi view'e gönderdik
        }
    }
}
