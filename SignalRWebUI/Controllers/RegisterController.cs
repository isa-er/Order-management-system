using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignalR.EntityLayer.Entities;
using SignalRWebUI.Dtos.IdentityDtos;

namespace SignalRWebUI.Controllers
{
    public class RegisterController : Controller
    {


        private readonly UserManager<AppUser> _userManager; // UserManager sınıfı identity ile ilgili işlemleri yapmamızı sağlıyor

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }





        [HttpPost]
        public async Task <IActionResult> Index(RegisterDto registerDto)
        {
            var appUser = new AppUser()
            {
                Name = registerDto.Name,
                Surname = registerDto.Surname,
                UserName = registerDto.Username,
                Email = registerDto.Mail
            };

            var result = await _userManager.CreateAsync(appUser, registerDto.Password); // Identity'de yeni bir kullanıcı oluşturmak için CreateAsync metodunu kullanıyoruz. şifre olarak ise registerDto'dan gelen şifreyi kullanıyoruz.


            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Login"); // kullanıcı başarılı bir şekilde oluşturulduysa login'in indexine yönlendiriyoruz
            }

            return View();






        }

    }
}
