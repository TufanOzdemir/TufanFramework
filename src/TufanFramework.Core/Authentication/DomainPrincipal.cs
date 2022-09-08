using System.Security.Claims;
using System.Threading.Tasks;

namespace TufanFramework.Core.Authentication
{
    public class DomainPrincipal : DomainClaimsPrincipal
    {
        private IPrincipalAccessor _principalAccessor;

        public DomainPrincipal(IPrincipalAccessor principalAccessor)
        {
            _principalAccessor = principalAccessor;
        }

        public override ClaimsPrincipal GetPrincipal()
        {
            return _principalAccessor.CurrentPrincipal;
        }

        public override Task<bool> Validate()
        {
            return Task.FromResult(true);
        }
    }
}