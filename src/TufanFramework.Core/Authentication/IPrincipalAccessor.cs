using System.Security.Claims;

namespace TufanFramework.Core.Authentication
{
    public interface IPrincipalAccessor
    {
        ClaimsPrincipal CurrentPrincipal { get; set; }
    }
}