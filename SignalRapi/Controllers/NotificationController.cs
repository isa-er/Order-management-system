using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.NotificationDto;
using SignalR.EntityLayer.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SignalRapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public NotificationController(INotificationService notificationService, IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }



        [HttpGet]
        public IActionResult NotificationList()
        {
            var values = _notificationService.TGetListAll();
            return Ok(_mapper.Map<List<ResultNotificationDto>>(values));
        }


        // TOPLAM okunmamış bildirim sayısını verir

        [HttpGet("NotificationCountByStatusFalse")]
        public IActionResult NotificationCountByStatusFalse()
        {
            return Ok(_notificationService.TNotificationCountByStatusFalse());
        }




        [HttpGet("GetAllNotificationByFalse")]
        public IActionResult GetAllNotificationByFalse()
        {
            return Ok(_notificationService.TGetAllNotificationByFalse());
        }



        [HttpPost]
        public IActionResult CreateNotification(CreateNotificationDto createNotificationDto)
        {
            createNotificationDto.Status=false; // yeni bildirim eklenirken status false olacak
            createNotificationDto.Date = DateTime.UtcNow;
            
            var values= _mapper.Map<Notification>(createNotificationDto); // createAboutDto'yu about'a dönüştürüyoruz

            _notificationService.TAdd(values);

            return Ok("Ekleme işlemi yapıldı");

        }




        [HttpDelete("{id}")]
        public IActionResult DeleteNotification(int id)
        {
            var value=_notificationService.TGetById(id);
            _notificationService.TDelete(value);
            return Ok("Bildirim Silindi");
        }


        [HttpGet("{id}")]
        public IActionResult GetNotification(int id)
        {
            var value = _notificationService.TGetById(id);
            return Ok(_mapper.Map<GetByIdNotificationDto>(value));
        }



        [HttpPut]
        public IActionResult UpdateNotification(UpdateNotificationDto updateNotificationDto)
        {
            var value = _notificationService.TGetById(updateNotificationDto.NotificationId);
            _notificationService.TUpdate(value);
            return Ok("Güncelleme işlemi yapıldı");

        }



        [HttpGet("NotificationStatusChangeToFalse/{id}")]
        public IActionResult NotificationStatusChangeToFalse(int id)
        {
            _notificationService.TNotificationStatusChangeToFalse(id);
            return Ok("Güncelleme Yapıldı");
        }


        [HttpGet("NotificationStatusChangeToTrue/{id}")]
        public IActionResult NotificationStatusChangeToTrue(int id)
        {
            _notificationService.TNotificationStatusChangeToTrue(id);
            return Ok("Güncelleme Yapıldı");
        }







    }
}
