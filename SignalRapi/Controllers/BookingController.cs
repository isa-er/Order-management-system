using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.BookingDto;
using SignalR.EntityLayer.Entities;

namespace SignalRapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateBookingDto> _validator; // createBookingDto için validatör tanımladık

        public BookingController(IBookingService bookingService, IMapper mapper, IValidator<CreateBookingDto> validator)
        {
            _bookingService = bookingService;
            _mapper = mapper;
            _validator = validator;
        }



        [HttpGet]
        public IActionResult BookingList()
        {
            var values = _bookingService.TGetListAll();
            return Ok(_mapper.Map<List<ResultBookingDto>>(values));
        }


        [HttpPost]
        public IActionResult CreateBooking(CreateBookingDto createBookingDto)
        {
            var validation= _validator.Validate(createBookingDto); // createBookingDto'dan gelen verileri validatör ile kontrol ediyoruz

            if(!validation.IsValid) // eğer validatör geçerli değilse
            {
                return BadRequest(validation.Errors); // hata mesajlarını döndürüyoruz
            }


            var value = _mapper.Map<Booking>(createBookingDto); // createBookingDto'yu booking'e dönüştürüyoruz
            _bookingService.TAdd(value);
            return Ok("Rezervasyon yapıldı");

        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            var value=_bookingService.TGetById(id);
            _bookingService.TDelete(value);
            return Ok("Rezervasyon silindi");
        }



        [HttpPut]
        public IActionResult UpdateBooking(UpdateBookingDto updateBookingDto)
        {
            var value=_mapper.Map<Booking>(updateBookingDto); // updateBookingDto'yu booking'e dönüştürüyoruz

            _bookingService.TUpdate(value);
            return Ok("Rezervasyon güncellendi");

        }



        //  ***********  SADECE BELİRLİ BİR FIELD GÜNCELLEMEK İÇİN  ***********

        //[HttpPut]
        //public IActionResult UpdateBooking(UpdateBookingDto updateBookingDto)
        //{
        //    var existingBooking = _bookingService.TGetById(updateBookingDto.BookingId);

        //    if (existingBooking == null)
        //    {
        //        return NotFound("Rezervasyon bulunamadı!");
        //    }

        //    existingBooking.Name = updateBookingDto.Name; // Sadece ismi güncelle

        //    _bookingService.TUpdate(existingBooking);

        //    return Ok("Rezervasyon güncellendi");
        //}







        [HttpGet("{id}")]
        public IActionResult GetBooking(int id)
        {
            var value = _bookingService.TGetById(id);
            return Ok(_mapper.Map<GetBookingDto>(value));
        }




        [HttpGet("BookingStatusApproved/{id}")]
        public IActionResult BookingStatusApproved(int id)
        {
            _bookingService.BookingStatusApproved(id);
            return Ok("Açıklama değişti");
        }



        [HttpGet("BookingStatusCancelled/{id}")]
        public IActionResult BookingStatusCancelled(int id)
        {
            _bookingService.BookingStatusCancelled(id);
            return Ok("Açıklama değişti");
        }




    }
}
