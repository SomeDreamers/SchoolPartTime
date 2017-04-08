using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace SchoolPartTime.WebApp.Helpers
{
    public static class IdentityExtension
    {
        public static string FullName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(ClaimTypes.Name);
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string Role(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(ClaimTypes.Role);
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static long Uid(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(ClaimTypes.Sid);
            return (claim != null) ? Convert.ToInt64(claim.Value) : 0;
        }
    }
}
