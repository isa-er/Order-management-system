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
    public class EfOrderDal : GenericRepository<Order>, IOrderDal
    {


        private readonly SignalRContext _context;
        public EfOrderDal(SignalRContext context) : base(context)
        {
            _context = context;
        }



        // entitye özgü metodlar. toplam sipariş sayısını verir.
        public int TotalOrderCount()
        {
            return _context.Orders.Count();
        }


        public int ActiveOrderCount()
        {
            return _context.Orders.Where(x => x.Description == "Müşteri masada").Count();
        }

        // son sipariş fiyatını verir.
        public decimal LastOrderPrice()
        {
            // 3 sorgu da aynı işi yapar.

            // return _context.Orders.OrderByDescending(x=>x.OrderId).Take(1).FirstOrDefault().TotalPrice;

            //return _context.Orders
            //.OrderByDescending(x => x.OrderId)
            //.Select(y => y.TotalPrice)
            //.FirstOrDefault();


            return _context.Orders.OrderByDescending(x => x.OrderId).Take(1).Select(y => y.TotalPrice).FirstOrDefault();
        }


        // bugün toplam kazancı verir.
        public decimal TodayTotalPrice()
        {
            var today = DateTime.UtcNow.Date;
            
            return  _context.Orders
                .Where(x => x.Date.Date == today)
                .Sum(x => x.TotalPrice);

            
        }
    }
}
