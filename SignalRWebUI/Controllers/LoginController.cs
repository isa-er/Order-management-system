using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignalR.EntityLayer.Entities;
using SignalRWebUI.Dtos.IdentityDtos;

namespace SignalRWebUI.Controllers
{
    [AllowAnonymous] // kurallardan muaf
    public class LoginController : Controller
    {


        private readonly SignInManager<AppUser> _signInManager; // SignInManager sınıfı identity ile ilgili işlemleri yapmamızı sağlıyor 

        public LoginController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }



        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Index(LoginDto loginDto)
        {
            var result= await _signInManager.PasswordSignInAsync(loginDto.Username, loginDto.Password, false, false); // kullanıcı adı ve şifreyi kontrol ediyor. false: beni hatırla, true: lockout (kullanıcıyı kilitleme) işlemi için kullanılıyor
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Category"); // giriş başarılıysa home'un indexine yönlendiriyoruz
            }
            return View(); // giriş başarısızsa tekrar login sayfasına yönlendiriyoruz
        }




        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync(); // çıkış yapma fonksiyonu
            return RedirectToAction("Index", "Login"); // login sayfasına yönlendir
        }



    }
}
