using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlassLewis.WebAPI.Core.Identity
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int ExpirationInHours { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
