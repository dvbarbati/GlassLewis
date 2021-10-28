using GlassLewis.WebApp.MVC.Extensions;
using GlassLewis.WebApp.MVC.Models;
using GlassLewis.WebApp.MVC.Models.Response;
using GlassLewis.WebApp.MVC.Service.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GlassLewis.WebApp.MVC.Service
{
    public class AuthenticationService : Service, IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.IdentityApiEndpoint);
            _httpClient = httpClient;
        }

        public async Task<ResponseResult<UserResponseLogin>> Login(UserLogin userLogin)
        {
            var loginContent = GetContent(userLogin);

            var response = await _httpClient.PostAsync("api/v1/identity/authentication", loginContent);

            if (!ResponseErrorsHandler(response))
            {
                return new ResponseResult<UserResponseLogin>
                {
                    ResponseResultError = await DeserealizeObjetResponse<ResponseResult>(response)
                };
            }

            return await DeserealizeObjetResponse<ResponseResult<UserResponseLogin>>(response);
        }

        public async Task<ResponseResult<UserResponseLogin>> Register(UserViewModel userRegister)
        {
            var registerContent = GetContent(userRegister);

            var response = await _httpClient.PostAsync("api/v1/identity/accounts", registerContent);

            if (!ResponseErrorsHandler(response))
            {
                return new ResponseResult<UserResponseLogin>
                {
                    ResponseResultError = await DeserealizeObjetResponse<ResponseResult>(response)
                };
            }

            return await DeserealizeObjetResponse<ResponseResult<UserResponseLogin>>(response);
        }
    }
}
