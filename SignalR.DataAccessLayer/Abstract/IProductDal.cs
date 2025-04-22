
using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.Abstract
{
    public interface IProductDal:IGenericDal<Product>
    {

        // Ürünlerin kategorileri ile birlikte (isimleriyle) getirilmesi
        List<Product> GetProductsWithCategories();

        public int ProductCount();
        int CountByCategoryNameHamburger();
        int CountByCategorNameDrink();

        int GetProductCountByCategoryName(string name);
        decimal ProductPriceAvg(); 
        string ProductNamePriceByMax(); // geriye ürün adı döneceğimiz için string yaptık
        string ProductNamePriceByMin(); // geriye ürün adı döneceğimiz için string yaptık

        //List<string> ProductNamePriceByMin(); 1 den fazla sayıda minimum fiyatlı ürün olabilir. Bu yüzden string yerine List<string> yaptık.


        decimal ProductAvgPriceByName(string name);

        decimal ProductPriceBySteakBurger();
        decimal TotalPriceByDrinkCategory();
        decimal TotalPriceBySaladCategory();

        List<Product> GetLast9Products();


    }
}
