using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.MessageDto;
using SignalR.EntityLayer.Entities;
using SignalRWebUI.Dtos.MessageDtos;
using ResultMessageDto = SignalR.DtoLayer.MessageDto.ResultMessageDto;

namespace SignalRapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {



        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public MessageController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult MessageList()
        {
            var values = _messageService.TGetListAll();
            return Ok(_mapper.Map<List<ResultMessageDto>>(values));
        }

        [HttpPost]
        public IActionResult CreateMessage(SignalR.DtoLayer.MessageDto.CreateMessageDto createMessageDto)
        {

            createMessageDto.Status = false;
            createMessageDto.MessageSendDate = DateTime.UtcNow;

            var value = _mapper.Map<Message>(createMessageDto); // createAboutDto'yu about'a dönüştürüyoruz

            _messageService.TAdd(value);
            return Ok("api kısmında Mesaj kısmı başarılı şekilde gönderildi");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(int id)
        {
            var value = _messageService.TGetById(id);
            _messageService.TDelete(value);
            return Ok("Mesaj silindi");
        }


        [HttpPut]
        public IActionResult UpdateMessage(SignalR.DtoLayer.MessageDto.UpdateMessageDto updateMessageDto)
        {


            var value = _mapper.Map<Message>(updateMessageDto); // createAboutDto'yu about'a dönüştürüyoruz

            _messageService.TUpdate(value);
            return Ok("Mesaj Bilgisi Güncellendi");
        }

        [HttpGet("{id}")]
        public IActionResult GetMessage(int id)
        {
            var value = _messageService.TGetById(id);
            return Ok(_mapper.Map<GetMeesageDto>(value));
        }

       







    }
}
