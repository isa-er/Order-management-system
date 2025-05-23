﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.Abstract
{



    //başında T harfi olan metotlar Business katmanının, olmayanlar DataAccess katmanına ait.
    public interface IGenericService<T> where T:class
    {
        void TAdd(T entity);
        void TDelete(T entity);
        void TUpdate(T entity);
        T TGetById(int id);
        List<T> TGetListAll();
    }
}
