using SignalR.DataAccessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;





//CRUD işlemlerini tekrar etmemesini ve bunları Generic bir yapı üzerinde tutmamızı sağlayacak

namespace SignalR.DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {


        private readonly SignalRContext _context;

        public GenericRepository(SignalRContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }
        public T GetById(int id)
        {


            //T Nedir?
            //T, generic bir türdür.Yani bu metod, herhangi bir entity(varlık) ile çalışabilir. Örneğin, Product, User, Category gibi modeller olabilir.
            //T, IGenericDal<T> içinde tanımlanan generic parametredir.Hangi entity ile çalışıyorsak, ona dönüşür.

            //_context.Set<T>() Ne İşe Yarıyor?
            //_context, DbContext sınıfından türetilmiş olan veritabanı bağlantı nesnesidir.
            //.Set<T>() metodu, belirtilen türde(T) bir DbSet nesnesi almayı sağlar.Yani, T yerine Product gelirse, .Set<Product>() olur ve Products tablosuyla çalışır.

            //Find(id) Ne Yapıyor?
            //Find(id), belirtilen id değerine sahip veriyi veritabanından çeker.
            //Eğer bu id değerine sahip bir veri yoksa, null döndürür.


            return _context.Set<T>().Find(id); // id'ye göre bulma işlemi.   .Set<T> demek, T'ye göre(yani entity'e göre) ayarla demek.
        }

        public List<T> GetListAll()
        {
            return _context.Set<T>().ToList(); // ilgili entity'nin (T) bütün verilerini listeler
        }

        public void Update(T entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
