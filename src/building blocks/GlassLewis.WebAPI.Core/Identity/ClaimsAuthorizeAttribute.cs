using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GlassLewis.WebAPI.Core.Identity
{
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue)
            :base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }
}
