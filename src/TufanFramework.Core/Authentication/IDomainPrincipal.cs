using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace TufanFramework.Core.Authentication
{
    public interface IDomainPrincipal : IPrincipal
    {
        int Id { get; }
        string strId { get; }
        string Name { get; }
        string FirstName { get; }
        string LastName { get; }
        string Email { get; }
        Guid AccessToken { get; }
        IEnumerable<Claim> Claims { get; }
        bool IsInScheme(string schemes);
        Task<bool> Validate();
    }
}