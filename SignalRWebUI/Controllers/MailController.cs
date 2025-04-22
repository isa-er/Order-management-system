using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using SignalRWebUI.Dtos.MailDtos;

namespace SignalRWebUI.Controllers
{
    public class MailController : Controller
    {


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(CreateMailDto createMailDto)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("SignalRRezervasyon", "err.isa99@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom); // Gönderen adresi ekle


            MailboxAddress mailboxAddressTo = new MailboxAddress("User", createMailDto.ReceiverMail);
            mimeMessage.To.Add(mailboxAddressTo);  // Alıcı adresi ekle

            var bodyBuilder = new BodyBuilder(); // MimeKit kütüphanesinden BodyBuilder sınıfı
            //bodyBuilder.TextBody = createMailDto.Body;
            bodyBuilder.HtmlBody = createMailDto.Body; // text editör html anladığı için
            mimeMessage.Body = bodyBuilder.ToMessageBody(); // Mesaj gövdesini ayarla

            mimeMessage.Subject = createMailDto.Subject; // Konu başlığını ayarla

            // SMTP ayarları
            SmtpClient client = new SmtpClient(); // SMTP istemcisi oluştur
            client.Connect("smtp.gmail.com", 587, false); // SMTP sunucusuna bağlan
            client.Authenticate("err.isa99@gmail.com", "ciaw lazq ofde will"); // SMTP kimlik doğrulaması. GOOGLE'DAN KEY ALDIK
            client.Send(mimeMessage); // Mesajı gönder
            client.Disconnect(true); // Bağlantıyı kapat


            return RedirectToAction("Index","Category");
        }



    }
}
