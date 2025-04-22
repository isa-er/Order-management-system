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
    public class EfBasketDal : GenericRepository<Basket>, IBasketDal
    {

        private readonly SignalRContext _context;
        public EfBasketDal(SignalRContext context) : base(context)
        {
            _context = context;
        }


        // dışarıdan gönderilen menüTable id'sine göre o id'ye ait kaydı getir
        public List<Basket> GetBasketByMenuTableNumber(int id)
        {
            // .Include() ile direkt olarak Product nesnesini dahil ediyoruz.
            var values=_context.Baskets.Where(x=>x.MenuTableId == id).Include(y=>y.Product).ToList();
            return values;
            
        }







        public void IncreaseProductCount(int id)
        {
            var basketItem = _context.Baskets.FirstOrDefault(x => x.BasketId == id);

            if (basketItem != null)
            {
                basketItem.Count += 1;
                basketItem.TotalPrice = basketItem.Count * basketItem.Price;
                _context.Baskets.Update(basketItem);
                _context.SaveChanges();
            }
        }

        public void DecreaseProductCount(int id)
        {
            var basketItem = _context.Baskets.FirstOrDefault(x => x.BasketId == id);

            if (basketItem != null && basketItem.Count > 1)
            {
                basketItem.Count -= 1;
                basketItem.TotalPrice = basketItem.Count * basketItem.Price;
                _context.Baskets.Update(basketItem);
                _context.SaveChanges();
            }
            else
            {
                _context.Baskets.Remove(basketItem);
                _context.SaveChanges();
            }
        }




    }
}
