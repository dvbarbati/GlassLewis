using GlassLewis.WebApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace GlassLewis.WebApp.MVC.Extensions
{
    public class PagingViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IPagedList model)
        {
            return View(model);
        }
    }
}
