using GlassLewis.WebApp.MVC.Models;
using GlassLewis.WebApp.MVC.Models.Response;
using System.Threading.Tasks;

namespace GlassLewis.WebApp.MVC.Service.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ResponseResult<UserResponseLogin>> Login(UserLogin userLogin);
        Task<ResponseResult<UserResponseLogin>> Register(UserViewModel userRegister);
    }
}
