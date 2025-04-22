using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRWebUI.Dtos.AboutDtos
{
    public class CreateAboutDto
    {
        // public int AboutId { get; set; }  id'ye gerek yok. çünkü kendi otomatik artan gelecek
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
