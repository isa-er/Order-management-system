using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.Abstract
{
    public interface IOrderService:IGenericService<Order>
    {
        int TTotalOrderCount(); // toplam sipariş sayısı
        int TActiveOrderCount(); // aktif sipariş sayısı
        decimal TLastOrderPrice(); // son sipariş fiyatı
        decimal TTodayTotalPrice(); // bugün toplam kazanç
    }
}
