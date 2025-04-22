using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.AboutDto;
using SignalR.EntityLayer.Entities;

namespace SignalRapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        private readonly IAboutService _aboutService;

        private readonly IMapper _mapper;

        public AboutController(IAboutService aboutService, IMapper mapper)
        {
            _aboutService = aboutService;
            _mapper = mapper;
        }



        [HttpGet]
        public IActionResult AboutList()
        {
            var values = _aboutService.TGetListAll();
            return Ok(_mapper.Map<List<ResultAboutDto>>(values));
        }

        [HttpPost]
        public IActionResult CreateAbout(CreateAboutDto createAboutDto)
        {


            //            var hedef = _mapper.Map<HEDEF_TIPI>(KAYNAK_NESNE);

            //            Map<T> içinde → hangi tipe dönüştürmek istiyorsun?
            //            Parantez içinde → hangi nesneden dönüştürmek istiyorsun?



//            Ne Yapmak İstiyorsun?                             AutoMapper Kodun
//              Product → ProductDto                             _mapper.Map<ProductDto>(product)
//              ProductDto → Product                             _mapper.Map<Product>(productDto)
//              User → UserDto                                   _mapper.Map<UserDto>(user)
//              UserDto → User                                   _mapper.Map<User>(userDto)



            // Mapleme işlemini > About sınıfına göre yapıyoruz, nerden gelen değer? >> (createAboutDto)

            var value = _mapper.Map<About>(createAboutDto); // createAboutDto'yu about'a dönüştürüyoruz

            // yukarıdaki kod ile aşağıdaki kod aynı işlemi yapar

            //About about = new About()
            //{
            //    Title = createAboutDto.Title,
            //    Description = createAboutDto.Description,
            //    ImageUrl = createAboutDto.ImageUrl
            //};

            //_aboutService.TAdd(about);

            _aboutService.TAdd(value);
            return Ok("Hakkımda kısmı başarılı bir şekilde eklendi");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAbout(int id)
        {
            var value = _aboutService.TGetById(id);
            _aboutService.TDelete(value);
            return Ok("Hakkımda alanı silindi");
        }


        [HttpPut]
        public IActionResult UpdateAbout(UpdateAboutDto updateAboutDto)
        {

            //About about = new About()
            //{
            //    AboutId = updateAboutDto.AboutId,
            //    ImageUrl = updateAboutDto.ImageUrl,
            //    Description = updateAboutDto.Description,
            //    Title = updateAboutDto.Title
            //};


            //_aboutService.TUpdate(about);


            var value = _mapper.Map<About>(updateAboutDto); // updateAboutDto'yu about'a dönüştürüyoruz
            _aboutService.TUpdate(value);

            return Ok("Hakkımda alanı güncellendi");
        }

        [HttpGet("{id}")]
        public IActionResult GetAbout(int id)
        {
            //var value=_aboutService.TGetById(id);
            //return Ok(value);


            var value = _aboutService.TGetById(id);
            return Ok(_mapper.Map<GetAboutDto>(value)); // GetAboutDto'ya dönüştürüyoruz    

        }



    }
}
