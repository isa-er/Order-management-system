using Microsoft.AspNetCore.Mvc;

namespace SignalRWebUI.ViewComponents.LayoutComponents
{
    public class _LayoutHeaderPartialComponent:ViewComponent
    {

        // artık burası bir view component
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
