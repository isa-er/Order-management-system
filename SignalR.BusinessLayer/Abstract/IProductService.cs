using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.Abstract
{
    public interface IProductService:IGenericService<Product>
    {
        public List<Product> TGetProductsWithCategories();
        public int TProductCount();

        int TCountByCategorNameHamburger();
        int TCountByCategorNameDrink();

        int TGetProductCountByCategoryName(string name);
        decimal TProductPriceAvg();

        string TProductNamePriceByMax();
        string TProductNamePriceByMin();
        //List<string> TProductNamePriceByMin();

        decimal TProductAvgPriceByName(string name);
        decimal TProductPriceBySteakBurger();
        decimal TTotalPriceByDrinkCategory();
        decimal TTotalPriceBySaladCategory();
        List<Product> TGetLast9Products();





    }
}
