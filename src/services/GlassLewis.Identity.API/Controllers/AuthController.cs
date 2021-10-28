using GlassLewis.Identity.API.ViewModels;
using GlassLewis.WebAPI.Core.Controller;
using GlassLewis.WebAPI.Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GlassLewis.Identity.API.Controllers
{
    [Route("api/v1/identity")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IOptions<AppSettings> appSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route("accounts")]
        public async Task<ActionResult> Register(UserRegistration userRegistration)
        {
            if (!ModelState.IsValid) return BadRequest();

            var identityUser = new IdentityUser
            {
                UserName = userRegistration.Email,
                Email = userRegistration.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(identityUser, userRegistration.Password);

            if (result.Succeeded)
                return CustomResponse(await GenerateJwt(userRegistration.Email));

            foreach (var error in result.Errors)
                AddError(error.Description);

            return CustomResponse();
        }

        [HttpPost]
        [Route("authentication")]
        public async Task<ActionResult> Login(UserLogin userLogin)
        {
            if (!ModelState.IsValid) return CustomResponse();

            var result = await _signInManager.PasswordSignInAsync(
                                userName: userLogin.Email,
                                password: userLogin.Password,
                                isPersistent: false,
                                lockoutOnFailure: true);

            if (result.Succeeded)
                return CustomResponse(await GenerateJwt(userLogin.Email));

            if (result.IsLockedOut)
            {
                AddError("User blocked for too many invalid attempts");
                return CustomResponse();
            }

            AddError("Incorrect username and password");

            return CustomResponse();
        }

        private async Task<ActionResult<UserResponseLogin>> GenerateJwt(string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(usuario);
            await ObterClaimsUsuario(usuario, claims);

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            string encodedToken = CodificarToken(identityClaims);

            return GetGeneratedToken(usuario, claims, encodedToken);
        }

        private UserResponseLogin GetGeneratedToken(IdentityUser usuario, IList<Claim> claims, string encodedToken)
        {
            return new UserResponseLogin
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationInHours).TotalSeconds,
                UserToken = new UserToken
                {
                    Id = usuario.Id,
                    Email = usuario.Email,
                    Claims = claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
                }
            };
        }

        private string CodificarToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationInHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            });

            return tokenHandler.WriteToken(token);
        }

        private async Task ObterClaimsUsuario(IdentityUser usuario, IList<Claim> claims)
        {
            var roles = await _userManager.GetRolesAsync(usuario);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, usuario.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, usuario.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var role in roles)
            {
                claims.Add(new Claim("role", role));
            }
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(year: 1970, month: 1, day: 1, hour: 0, minute: 0, second: 0, offset: TimeSpan.Zero)).TotalSeconds);
    }
}
