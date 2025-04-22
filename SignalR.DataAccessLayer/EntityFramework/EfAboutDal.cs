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

    //Bu kod, Entity Framework(EF) tabanlı bir repository(depo) sınıfı oluşturuyor ve bu sınıfı GenericRepository ile IAboutDal arayüzü arasında bir köprü olarak kullanıyor.

    //class interface ve GenericRepository'i haberleştireceğimiz yer
    public class EfAboutDal : GenericRepository<About>, IAboutDal
    {
        public EfAboutDal(SignalRContext context) : base(context)
        {
        }
    }
}
