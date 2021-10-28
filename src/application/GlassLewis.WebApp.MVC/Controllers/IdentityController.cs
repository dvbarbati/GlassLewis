using GlassLewis.WebApp.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using IAuthenticationService = GlassLewis.WebApp.MVC.Service.Interfaces.IAuthenticationService;

namespace GlassLewis.WebApp.MVC.Controllers
{
    public class IdentityController : MainController
    {
        private readonly IAuthenticationService _authenticationService;

        public IdentityController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        [Route("new-account")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("new-account")]
        public async Task<IActionResult> Register(UserViewModel userRegistration)
        {

            if (!ModelState.IsValid)
                return View(userRegistration);

            // API - Registro
            var response = await _authenticationService.Register(userRegistration);

            if (ResponseContainsErrors(response.ResponseResultError))
                return View(userRegistration);

            await LoginUser(response.Value);
            // Realizar Login na APi

            return RedirectToAction(nameof(Index), "Company");
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLogin userLogin, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
                return View(userLogin);

            var response = await _authenticationService.Login(userLogin);

            if (ResponseContainsErrors(response.ResponseResultError))
                return View(userLogin);

            await LoginUser(response.Value);
            await LoginUser(response.Value);

            if (string.IsNullOrEmpty(returnUrl)) 
                return RedirectToAction(nameof(Index), "Company");

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Index), "Company");
        }

        private async Task LoginUser(UserResponseLogin response)
        {
            var token = GetFormattedToken(response.AccessToken);

            var claims = new List<Claim>();

            claims.Add(new Claim("JWT", response.AccessToken));
            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        private static JwtSecurityToken GetFormattedToken(string jwtToken)
        {
            return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
        }
    }
}
