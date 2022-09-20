using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace TufanFramework.Core.Swagger
{
    public static class SwaggerExtensions
    {
        public static void AddSwaggerWithJWT(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.AddSecurityDefinition("Bearer",
                       new OpenApiSecurityScheme
                       {
                           Name = "Authorization",
                           Type = SecuritySchemeType.ApiKey,
                           Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                           In = ParameterLocation.Header
                       });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new[] { "" }
                    }
                });
            });
        }
    }
}
