using GlassLewis.WebApp.MVC.Extensions.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace GlassLewis.WebApp.MVC.Extensions
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext.User.Identity.Name;

        public IEnumerable<Claim> GetClaims()
            => _accessor.HttpContext.User.Claims;

        public HttpContext GetHttpContext()
            => _accessor.HttpContext;

        public string GetUserEmail()
            => IsAuthenticated() ? _accessor.HttpContext.User.GetUserEmail() : string.Empty;

        public Guid GetUserId()
             => IsAuthenticated() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.Empty;

        public string GetUserToken()
            => IsAuthenticated() ? _accessor.HttpContext.User.GetUserToken() : string.Empty;

        public bool HasRole(string role)
            => _accessor.HttpContext.User.IsInRole(role);

        public bool IsAuthenticated()
            => _accessor.HttpContext.User.Identity.IsAuthenticated; 
    }
}
