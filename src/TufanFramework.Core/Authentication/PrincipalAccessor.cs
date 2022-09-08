using System.Security.Claims;

namespace TufanFramework.Core.Authentication
{
    public class PrincipalAccessor : IPrincipalAccessor
    {
        public ClaimsPrincipal CurrentPrincipal { get; set; }
    }
}