using Microsoft.EntityFrameworkCore;
using SignalR.DataAccessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DataAccessLayer.Repositories;
using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.EntityFramework
{
    public class EfProductDal : GenericRepository<Product>, IProductDal
    {

        private readonly SignalRContext _context;
        public EfProductDal(SignalRContext context) : base(context)
        {
            _context= context;
        }


        // entitye özgü metodlar. Kategori adı İçecek olan productların sayısını veriyor.
        public int CountByCategorNameDrink()
        {
            // return _context.Products.Where(x=>x.Category.CategoryName == "Drink").Count();
            return _context.Products.Where(x=>x.CategoryId==(_context.Categories.Where(y=>y.CategoryName=="İçecek").Select(z=>z.CategoryId).FirstOrDefault())).Count();
        }


        // entitye özgü metodlar. Kategori adı Hamburger olan productların sayısını veriyor.

        public int CountByCategoryNameHamburger()
        {
            return _context.Products.Where(x => x.CategoryId == (_context.Categories.Where(y => y.CategoryName == "Hamburger").Select(z => z.CategoryId).FirstOrDefault())).Count();
        }





        // Yukarıdakilerin Genel Hali
        public int GetProductCountByCategoryName(string name)

        {
            return _context.Products.Where(x => x.CategoryId == (_context.Categories.Where(y => y.CategoryName == name).Select(z => z.CategoryId).FirstOrDefault())).Count();


        }


        public List<Product> GetProductsWithCategories()
        {

            // include ile product'ın içine ekliyoruz
            var values=_context.Products.Include(x=>x.Category).ToList();    
            return values;
        }

        // entitye özgü metod
        public int ProductCount()
        {
            using var Context = new SignalRContext();
            return Context.Products.Count();
        }


        // entitye özgü metod. en yüksek fiyatlı ürünün adını verir.
        public string ProductNamePriceByMax()
        {
           return _context.Products.Where(x=>x.Price==(_context.Products.Max(y=>y.Price))).Select(z => z.ProductName).FirstOrDefault();
        }


        //entitye özgü metod.en düşük fiyatlı ürünün adını verir.
        public string ProductNamePriceByMin()
        {
            return _context.Products.Where(x => x.Price == (_context.Products.Min(y => y.Price))).Select(z => z.ProductName).FirstOrDefault();

        }

        // 1 den fazla sayıda minimum fiyatlı ürün olabilir. Bu yüzden string yerine List<string> yaptık.
        //public List<string> ProductNamePriceByMin()
        //{
        //    return _context.Products.Where(x => x.Price == (_context.Products.Min(y => y.Price))).Select(z => z.ProductName).ToList();

        //}



        // entitye özgü metod. ürünlerin fiyatlarının ortalamasını verir.
        public decimal ProductPriceAvg()
        {
            return Math.Round(_context.Products.Average(x => x.Price), 2);
        }



        // herhangi bir kategoriye ait ürünlerin fiyatlarının ortalamasını verir.
        public decimal ProductAvgPriceByName(string name)
        {
            return _context.Products.Where(x=>x.CategoryId==(_context.Categories.Where(y=>y.CategoryName==name).Select(z => z.CategoryId).FirstOrDefault())).Average(a => a.Price);
        }

        public decimal ProductPriceBySteakBurger()
        {
            return _context.Products.Where(x=>x.ProductName== "Steak Burger").Select(y => y.Price).FirstOrDefault();
        }


        // Alt sorgulu
        public decimal TotalPriceByDrinkCategory()
        {
            
            return _context.Products.Where(x=>x.CategoryId == (_context.Categories.Where(y => y.CategoryName == "İçecek").Select(z => z.CategoryId).FirstOrDefault())).Sum(a => a.Price);
        }


        // Alt sorgulu
        public decimal TotalPriceBySaladCategory()
        {
            return _context.Products.Where(x => x.CategoryId == (_context.Categories.Where(y => y.CategoryName == "Salata").Select(z => z.CategoryId).FirstOrDefault())).Sum(a => a.Price);
        }

        public List<Product> GetLast9Products()
        {
            var values=_context.Products.Take(9).ToList();
            return values;
        }
    }
}
