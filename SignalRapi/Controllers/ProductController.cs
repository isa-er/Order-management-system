using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DtoLayer.CategoryDto;
using SignalR.DtoLayer.ProductDto;
using SignalR.EntityLayer.Entities;

namespace SignalRapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {


        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult ProductList()
        {
            var values = _productService.TGetListAll();
            return Ok(values);
        }



        // entity'e özel yazdığımız metot
        [HttpGet("ProductCount")]
        public IActionResult ProductCount()
        {
            return Ok(_productService.TProductCount());
        }



        // entity'e özel yazdığımız metot . hamburger olanların sayısını verir.
        [HttpGet("ProductCountByHamburger")]
        public IActionResult ProductCountByHamburger()
        {
            return Ok(_productService.TCountByCategorNameHamburger());
        }




        // entity'e özel yazdığımız metot. içecek olanların sayısını verir.
        [HttpGet("ProductCountByDrink")]
        public IActionResult ProductCountByDrink()
        {
            return Ok(_productService.TCountByCategorNameDrink());
        }


        // entity'e özel yazdığımız metot. içecek olanların sayısını verir.
        [HttpGet("TotalPriceBySaladCategory")]
        public IActionResult TotalPriceBySaladCategory()
        {
            return Ok(_productService.TTotalPriceBySaladCategory());
        }


        // entity'e özel yazdığımız metot. yukarıdakilerin genel hali
        [HttpGet("ProductCountByName")]
        public IActionResult ProductCountByName(string name)
        {
            return Ok(_productService.TGetProductCountByCategoryName(name));
        }



        // entity'e özel yazdığımız metot. ürünlerin fiyatlarının ortalamasını verir.
        [HttpGet("ProductPriceAvg")]
        public IActionResult ProductPriceAvg()
        {
            return Ok(_productService.TProductPriceAvg());
        }



        [HttpGet("TotalPriceByDrinkCategory")]
        public IActionResult TotalPriceByDrinkCategory()
        {
            return Ok(_productService.TTotalPriceByDrinkCategory());
        }




        // entity'e özel yazdığımız metot. ürünlerin fiyatlarının ortalamasını verir.
        [HttpGet("ProductPriceBySteakBurger")]
        public IActionResult ProductPriceBySteakBurger()
        {
            return Ok(_productService.TProductPriceBySteakBurger());
        }


        // entity'e özel yazdığımız metot. en yüksek fiyatlı ürünün adını verir.
        [HttpGet("ProductNamePriceByMax")]
        public IActionResult ProductNamePriceByMax()
        {
            return Ok(_productService.TProductNamePriceByMax());
        }


        // entity'e özel yazdığımız metot. en düşük fiyatlı ürünün adını verir.
        [HttpGet("ProductNamePriceByMin")]
        public IActionResult ProductNamePriceByMin()
        {
            return Ok(_productService.TProductNamePriceByMin());
        }



        // entity'e özel yazdığımız metot. herhangi bir kategoriye ait ürünlerin fiyatlarının ortalamasını verir.
        [HttpGet("ProductAvgPriceByName")]
        public IActionResult ProductAvgPriceByName(string name)
        {
            return Ok(_productService.TProductAvgPriceByName(name));
        }


        // entity'e özel yazdığımız metot 
        [HttpGet("ProductListWithCategory")]  // UI kısmındaki Controller'da kullanacağız. 

        // var responseMessage = await client.GetAsync("https://localhost:7113/api/Product/ProductListWithCategory");  şeklinde
        public IActionResult ProductListWithCategory()
        {
            var context = new SignalRContext();
            var values = context.Products.Include(y => y.Category).Select(x => new ResultProductWithCategory
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Description = x.Description,
                Price = x.Price,
                ImageUrl = x.ImageUrl,
                ProductStatus = x.ProductStatus,
                CategoryName = x.Category.CategoryName
            });

            return Ok(values.ToList());


        }


        [HttpPost]
        public IActionResult CreateProduct(CreateProductDto createProductDto)
        {
            var value= _mapper.Map<Product>(createProductDto); // createProductDto'yu product'a dönüştürüyoruz

            _productService.TAdd(value);
            return Ok("Ürün eklendi");
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var value = _productService.TGetById(id);
            _productService.TDelete(value);
            return Ok("Ürün  silindi");
        }





        [HttpPut]
        public IActionResult UpdateProduct(UpdateProductDto updateProductDto)
        {

            var value = _mapper.Map<Product>(updateProductDto); // updateProductDto'yu product'a dönüştürüyoruz

            _productService.TUpdate(value);
            return Ok("Ürün güncellendi");
        }



        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var value = _productService.TGetById(id);
            return Ok(_mapper.Map<GetProductDto>(value));
        }







        [HttpGet("GetLast9Products")]
        public IActionResult GetLast9Products()
        {
            var value=_productService.TGetLast9Products();
            return Ok(value);
        }







    }
}

