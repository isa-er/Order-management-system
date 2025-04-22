using SignalRWebUI.Dtos.CategoryDtos;
using SignalRWebUI.Dtos.ProductDtos;

namespace SignalRWebUI.Views.Shared.Components
{
    public class ProductCategoryViewModel
    {
        public List<ResultProductDto> Products { get; set; }
        public List<ResultCategoryDto> Categories { get; set; }
    }
}
