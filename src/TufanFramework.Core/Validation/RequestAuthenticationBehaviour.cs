using TufanFramework.Core.Authentication;
using MediatR;
using System;
using System.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;

namespace TufanFramework.Common.Validation
{
    public class RequestAuthenticationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
             where TRequest : IRequest<TResponse>
    {
        private const string _authenticationExceptionLogFormat = "Access is denied for user {0}. Required permissions for this service: {1}";
        private readonly IPrincipalAccessor _principalAccessor;
        public RequestAuthenticationBehaviour(IPrincipalAccessor principalAccessor)
        {
            _principalAccessor = principalAccessor;
        }
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var currentUser = _principalAccessor.CurrentPrincipal;
            
            var attrs = request.GetType()
                .GetCustomAttributes(false)
                .Where(c=> c is RequiredPermissionAttribute)
                .Cast<RequiredPermissionAttribute>()
                .ToList();

            if (attrs != null && attrs.Any())
            {
                if (currentUser == null)
                {
                    throw new UnauthorizedAccessException("Unauthorized");
                }

                foreach (var attr in attrs)
                {
                    if (currentUser.HasClaim(c => c.Value == attr.RequiredPermission.PermissionCode))
                        return next();
                }

                string username = currentUser.Identity.Name;

                string permList = string.Join(',', attrs.Select(e => e.RequiredPermission.PermissionCode));
                throw new AuthenticationException(string.Format(_authenticationExceptionLogFormat, username, permList));
            }

            return next();
        }
    }
}