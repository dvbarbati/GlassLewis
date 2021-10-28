using System.Collections.Generic;

namespace GlassLewis.Identity.API.ViewModels
{
    public class UserToken
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaim> Claims { get; set; }
    }
}

