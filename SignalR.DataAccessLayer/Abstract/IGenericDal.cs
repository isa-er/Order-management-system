using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.Abstract
{



    //   Interface, bir sınıfın hangi metotları içermesi gerektiğini belirten bir şablondur. Ancak içinde metotların içeriği (gövdesi)  olmaz, SADECE İMZALARI  bulunur.
    public interface IGenericDal<T> where T:class  // dışarıdan sadece class alacak
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        T GetById(int id);
        List<T> GetListAll();
    }
}
