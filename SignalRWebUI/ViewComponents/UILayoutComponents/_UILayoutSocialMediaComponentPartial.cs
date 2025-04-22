using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.SocialMediaDtos;

namespace SignalRWebUI.ViewComponents.UILayoutComponents
{
    public class _UILayoutSocialMediaComponentPartial : ViewComponent
    {



        private readonly IHttpClientFactory _httpClientFactory;

        public _UILayoutSocialMediaComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }




        // API'yi CONSUME ettiğimiz yer.  LİSTELEME KISMI
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient(); // istemci oluşturduk
            var responseMessage = await client.GetAsync("https://localhost:7113/api/SocialMedia"); //GetAsync>verileri listelemek için 

            if (responseMessage.IsSuccessStatusCode) // başarılı ise
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); // Jsondan gelen içeriği string olarak okuduk
                var values = JsonConvert.DeserializeObject<List<ResultSocialMediaDto>>(jsonData); // Jsondan (jsonData) gelen içeriği listeye çevirdik
                return View(values); // listeyi view'e gönderdik

            }
            return View(); // başarısız ise boş view döndürdük
        }

    }


}
