using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Abstract;
using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.Concrete
{
    public class ProductManager : IProductService
    {




        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }






        public void TAdd(Product entity)
        {
            _productDal.Add(entity);
        }

        public void TDelete(Product entity)
        {
            _productDal.Delete(entity);
        }

        public Product TGetById(int id)
        {
            return _productDal.GetById(id);
        }

        public List<Product> TGetListAll()
        {
            return _productDal.GetListAll();
        }

        

        public void TUpdate(Product entity)
        {
            _productDal.Update(entity);
        }



        // ************** Entitye özgü işlemler **************
        public List<Product> TGetProductsWithCategories()
        {
            return _productDal.GetProductsWithCategories();
        }

        public int TProductCount()
        {
            return _productDal.ProductCount();
        }

        // kateogri adı hamburger olan ürünlerin sayısını verir.
        public int TCountByCategorNameHamburger()
        {
            return _productDal.CountByCategoryNameHamburger();
        }

        // kateogri adı içecek olan ürünlerin sayısını verir.
        public int TCountByCategorNameDrink()
        {
            return _productDal.CountByCategorNameDrink();
        }

        // yukarıdakilerin genel hali
        public int TGetProductCountByCategoryName(string name)
        {
            return _productDal.GetProductCountByCategoryName(name);
        }


        // ürünlerin fiyatlarının ortalamasını verir.
        public decimal TProductPriceAvg()
        {
            return _productDal.ProductPriceAvg();
        }


        // en yüksek fiyatlı ürünün adını verir.
        public string TProductNamePriceByMax()
        {
            return _productDal.ProductNamePriceByMax();
        }

        public string TProductNamePriceByMin()
        {
            return _productDal.ProductNamePriceByMin();
        }

        //  1 den fazla sayıda minimum fiyatlı ürün olabilir. Bu yüzden string yerine List<string> yaptık.
        //public List<string >TProductNamePriceByMin()
        //{
        //    return _productDal.ProductNamePriceByMin();
        //}






        // Herhangi bir kategoriye ait ürünlerin fiyatlarının ortalamasını verir.
        public decimal TProductAvgPriceByName(string name)
        {
            return _productDal.ProductAvgPriceByName(name);
        }

        public decimal TProductPriceBySteakBurger()
        {
            return _productDal.ProductPriceBySteakBurger();
        }

        public decimal TTotalPriceByDrinkCategory()
        {
            return _productDal.TotalPriceByDrinkCategory();
        }

        public decimal TTotalPriceBySaladCategory()
        {
            return _productDal.TotalPriceBySaladCategory();
        }

        public List<Product> TGetLast9Products()
        {
            return _productDal.GetLast9Products();
        }
    }
}
