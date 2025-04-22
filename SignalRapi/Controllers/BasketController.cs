using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DtoLayer.BasketDto;
using SignalR.EntityLayer.Entities;
using SignalRapi.Models;
using SignalRWebUI.Dtos.BasketDtos;

namespace SignalRapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly SignalRContext _context;
        private readonly IMapper _mapper;

        public BasketController(IBasketService basketService, SignalRContext context, IMapper mapper)
        {
            _basketService = basketService;
            _context = context;
            _mapper = mapper;
            
        }


        [HttpGet]
        public IActionResult GetBasketByMenuTableId(int id)
        {
            var values=_basketService.TGetBasketByMenuTableNumber(id);
            return Ok(values);

        }

        [HttpGet("BasketListByMenuTableWithProductName")]
        public IActionResult BasketListByMenuTableWithProductName(int id)
        {
            var values = _context.Baskets.Include(x => x.Product).Where(y => y.MenuTableId == id).Select(z => new ResultBasketListWithProducts
            {
                BasketId = z.BasketId,
                Count = z.Count,
                MenuTableId = z.MenuTableId,
                Price = z.Price,
                ProductId = z.ProductId,
                TotalPrice = z.TotalPrice,
                ProductName = z.Product.ProductName,


            }).ToList();

            return Ok(values);

        }


        [HttpPost]
        public IActionResult CreateBasket(SignalR.DtoLayer.BasketDto.CreateBasketDto createBasketDto)
        {
            _basketService.TAdd(new Basket()
            {
                ProductId = createBasketDto.ProductId,
                MenuTableId = createBasketDto.MenuTableId,
                Count = 1,
                // MenuTableId = 1,
                // masa numarası atanmalı
                Price = _context.Products.Where(x=>x.ProductId==createBasketDto.ProductId).Select(y=>y.Price).FirstOrDefault(),
                TotalPrice=createBasketDto.TotalPrice,

            });


            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBasket(int id) { 

            var value=_basketService.TGetById(id);
            _basketService.TDelete(value);
            return Ok("Sepet silindi");
        }




        [HttpPut("increase/{id}")]
        public IActionResult IncreaseProductCount(int id)
        {
            try
            {
                _basketService.TIncreaseProductCount(id);
                return Ok("Ürün miktarı artırıldı");
            }
            catch (Exception ex)
            {
                return BadRequest($"Bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPut("decrease/{id}")]
        public IActionResult DecreaseProductCount(int id)
        {
            try
            {
                _basketService.TDecreaseProductCount(id);
                return Ok("Ürün miktarı azaltıldı");
            }
            catch (Exception ex)
            {
                return BadRequest($"Bir hata oluştu: {ex.Message}");
            }
        }







    }
}
