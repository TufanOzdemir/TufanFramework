using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace TufanFramework.Core.Authentication
{
    public abstract class DomainClaimsPrincipal : IDomainPrincipal
    {
        public DomainClaimsPrincipal()
        {
        }

        private string GetClaim(string name)
        {
            return GetPrincipal().FindFirstValue(name);
        }

        private T GetClaim<T>(string name)
        {
            var value = GetClaim(name);
            if (string.IsNullOrWhiteSpace(value))
                return default;

            return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(value);
        }

        public virtual int Id
        {
            get
            {
                return GetClaim<int>(ClaimTypes.NameIdentifier);
            }
        }

        public virtual string Name
        {
            get
            {
                return GetClaim<string>(ClaimTypes.Name);
            }
        }

        public virtual string FirstName
        {
            get
            {
                return GetClaim(ClaimTypes.GivenName);
            }
        }

        public virtual string LastName
        {
            get
            {
                return GetClaim(ClaimTypes.Surname);
            }
        }

        public virtual string Email
        {
            get
            {
                return GetClaim(ClaimTypes.Email);
            }
        }

        public string UserData
        {
            get
            {
                return GetClaim(ClaimTypes.UserData);
            }
        }

        public virtual Guid AccessToken
        {
            get
            {
                return GetClaim<Guid>(ClaimTypes.Sid);
            }
        }

        public virtual IIdentity Identity
        {
            get
            {
                return GetPrincipal().Identity;
            }
        }

        public virtual IEnumerable<Claim> Claims
        {
            get
            {
                return GetPrincipal().Claims;
            }
        }

        public virtual bool IsInRole(string roles)
        {
            if (string.IsNullOrWhiteSpace(roles))
                return true;

            var principal = GetPrincipal();

            var roleArray = roles.Split(',');
            foreach (var role in roleArray)
            {
                if (principal.IsInRole(role))
                {
                    return true;
                }
            }

            return false;
        }

        public virtual bool IsInScheme(string schemes)
        {
            if (string.IsNullOrWhiteSpace(schemes))
                return true;

            var principal = GetPrincipal();

            var schemeArray = schemes.Split(',');
            foreach (var scheme in schemeArray)
            {
                if (principal.Identity.AuthenticationType == scheme)
                {
                    return true;
                }
            }

            return false;
        }

        public abstract Task<bool> Validate();

        public abstract ClaimsPrincipal GetPrincipal();
    }
}