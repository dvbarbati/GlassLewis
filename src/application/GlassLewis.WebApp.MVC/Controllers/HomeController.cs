using GlassLewis.WebApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace GlassLewis.WebApp.MVC.Controllers
{
    public class HomeController : MainController
    {

        [Route("error/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelError = new ErrorViewModel();

            if (id == 500)
            {
                modelError.Message = "An error has occurred! Please try again later or contact support.";
                modelError.Title = "An error has occurred!";
                modelError.ErrorCode = id;
            }
            else if (id == 404)
            {
                modelError.Message = "The page you are looking for does not exist! <br/> Please contact our support.";
                modelError.Title = "Oops! Page not found.";
                modelError.ErrorCode = id;
            }
            else if (id == 403)
            {
                modelError.Message = "You are not allowed to do this.";
                modelError.Title = "Access denied";
                modelError.ErrorCode = id;
            }
            else
                return StatusCode(404);

            return View(nameof(Error), modelError);
        }
    }
}
