using GlassLewis.WebApp.MVC.Models.Response;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GlassLewis.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
      protected bool ResponseContainsErrors(ResponseResult response)
        {
            if (response != null && response.Errors.Messages.Any())
            {
                foreach (var message in response.Errors.Messages)
                {
                    ModelState.AddModelError(string.Empty, message);
                }
                    
                return true;
            }
            else
                return false;
        }
    }
}
