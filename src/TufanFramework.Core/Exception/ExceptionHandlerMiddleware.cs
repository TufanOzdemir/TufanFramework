using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;
using TufanFramework.Common.Log;
using FluentValidation;

namespace TufanFramework.Common.Exception
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogService _logService;

        public ExceptionHandlerMiddleware(RequestDelegate next, IWebHostEnvironment webHostEnvironment, ILogService logService)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _webHostEnvironment = webHostEnvironment;
            _logService = logService;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UnauthorizedAccessException ex)
            {
                var exModel = new GeneralError { Message = ex.Message, Status = HttpStatusCode.Unauthorized };
                await ContextDecorator(context, exModel);
            }
            catch (AuthenticationException ex)
            {
                var exModel = new GeneralError { Message = ex.Message, Status = HttpStatusCode.Forbidden };
                await ContextDecorator(context, exModel);
            }
            catch (ValidationException ex)
            {
                var exModel = new GeneralError { Message = _webHostEnvironment.IsProduction() ? "ValidationException" : ex.ToString() , Status = HttpStatusCode.BadRequest };
                await ContextDecorator(context, exModel);
            }
            catch (System.Exception ex)
            {
                var exModel = new GeneralError { Message = _webHostEnvironment.IsProduction() ? "An error occurred" : ex.ToString(), Status = HttpStatusCode.InternalServerError };
                _logService.Error(ex.Message, ex);
                await ContextDecorator(context, exModel);
            }
        }

        private async Task ContextDecorator(HttpContext context, GeneralError error)
        {
            context.Response.Clear();
            context.Response.ContentType = "json";
            context.Response.StatusCode = error.Status.GetHashCode();
            await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
        }
    }

    public class GeneralError
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
    }

    public static class HttpStatusCodeExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}