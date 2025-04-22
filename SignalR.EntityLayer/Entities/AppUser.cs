using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace SignalR.EntityLayer.Entities
{
    public class AppUser:IdentityUser <int> // biz bu sınıfı kendimize göre özelleştircez
    {

        // Id default olarak string geliyordu biz yukaruıda <int> diye belirttik 


        // varsayılan propların üzerine ekstra bu propları ekliyoruz
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
