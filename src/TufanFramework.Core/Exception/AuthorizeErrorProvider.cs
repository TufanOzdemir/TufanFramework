using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace TufanFramework.Common.Exception
{
    public class AuthorizeErrorProvider : JsonResult
    {
        public AuthorizeErrorProvider(string message) : base(new AuthenticationException(message))
        {
            StatusCode = StatusCodes.Status401Unauthorized;
        }
    }
}