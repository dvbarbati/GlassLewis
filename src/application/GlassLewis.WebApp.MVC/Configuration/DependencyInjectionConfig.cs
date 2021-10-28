using GlassLewis.WebApp.MVC.Extensions;
using GlassLewis.WebApp.MVC.Extensions.Interfaces;
using GlassLewis.WebApp.MVC.Service;
using GlassLewis.WebApp.MVC.Service.Handler;
using GlassLewis.WebApp.MVC.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace GlassLewis.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddRegisterServices(this IServiceCollection services)
        {

            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
            services.AddHttpClient<IAuthenticationService, AuthenticationService>();
            services.AddHttpClient<ICompanyService, CompanyService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}
