using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignalR.EntityLayer.Entities;
using SignalRWebUI.Dtos.IdentityDtos;

namespace SignalRWebUI.Controllers
{
    public class SettingController : Controller
    {


        private readonly UserManager<AppUser> _userManager;

        public SettingController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var value = await _userManager.FindByNameAsync(User.Identity.Name); // giriş yapan kullanıcının bilgilerini alıyoruz
            UserEditDto userEditDto = new UserEditDto();
            userEditDto.Surname = value.Surname;
            userEditDto.Name = value.Name;
            userEditDto.Mail = value.Email;
            userEditDto.Username = value.UserName;
            return View(userEditDto);
        }



        [HttpPost]
        public async Task<IActionResult> Index(UserEditDto userEditDto)
        {
            if (userEditDto.Password == userEditDto.ConfirmPassword)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name); // giriş yapan kullanıcının bilgilerini al.

                user.Name = userEditDto.Name;
                user.Surname = userEditDto.Surname;
                user.Email = userEditDto.Mail;
                user.UserName = userEditDto.Username;

                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userEditDto.Password); // şifreyi hashliyoruz

                await _userManager.UpdateAsync(user); // güncellenmiş kullanıcıyı veritabanına kaydediyoruz

                return RedirectToAction("Index", "Category"); // güncelleme başarılıysa login sayfasına yönlendiriyoruz

            }



            return View();



        }
    }
}
