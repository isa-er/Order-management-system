using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.CategoryDto;
using SignalR.DtoLayer.ContactDto;
using SignalR.EntityLayer.Entities;

namespace SignalRapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {


        private readonly IContactService _contactService;
        private readonly IMapper _mapper;
        public ContactController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }



        [HttpGet]
        public IActionResult ContactList()
        {
            var values = _mapper.Map<List<ResultContactDto>>(_contactService.TGetListAll());
            return Ok(values);
        }


        [HttpPost]
        public IActionResult CreateContact(CreateContactDto createContactDto)
        {
            var value= _mapper.Map<Contact>(createContactDto); // createAboutDto'yu about'a dönüştürüyoruz

            _contactService.TAdd(value);
            return Ok("İletişim bilgisi eklendi");
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            var value = _contactService.TGetById(id);
            _contactService.TDelete(value);
            return Ok("İletişim  silindi");
        }





        [HttpPut]
        public IActionResult UpdateContact(UpdateContactDto updateContactDto)
        {

           var value=_mapper.Map<Contact>(updateContactDto); // createAboutDto'yu about'a dönüştürüyoruz

            _contactService.TUpdate(value);
            return Ok("İletişim bilgisi güncellendi");
        }



        [HttpGet("{id}")]
        public IActionResult GetContact(int id)
        {
            var value = _contactService.TGetById(id);
            return Ok(_mapper.Map<GetContactDto>(value));
        }

    }
}
