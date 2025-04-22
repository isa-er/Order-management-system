using SignalR.DataAccessLayer.Abstract;
using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Bu, IGenericDal<T> arayüzünü About sınıfına özel hale getirir.

//IAboutDal ne yapar?
//IGenericDal<About>'ı miras alarak, About entity'sine özgü veri erişim işlemlerini yapabilen bir arayüz oluşturur.

//Özelleştirme için neden kullanılır?
//IGenericDal her entity için ortak metotları (Add, Delete, Update vs.) barındırır.
//Ancak IAboutDal içine ABOUT'A ÖZEL BİR İŞLEM eklemek istersen, buraya ekleyebilirsin. Örneğin:

//public interface IAboutDal : IGenericDal<About>
//{
//    List<About> GetRecentAbouts(); // Sadece About için geçerli bir metod
//}





namespace SignalR.DataAccessLayer.Abstract
{
    public  interface IAboutDal : IGenericDal<About>
    {
    }
}
