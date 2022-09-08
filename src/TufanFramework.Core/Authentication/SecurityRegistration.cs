using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TufanFramework.Common.Configuration;

namespace TufanFramework.Core.Authentication
{
    public static class SecurityRegistration
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services, IConfigResolver configResolver)
        {
            AuthenticationConfig configuration = null;
            configuration = configResolver.Resolve<AuthenticationConfig>();

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration), "Authentication configuration is missing");
            var principalAccessor = new PrincipalAccessor();
            services.AddScoped<IPrincipalAccessor>(c => principalAccessor);
            services.AddScoped(typeof(IDomainPrincipal), typeof(DomainPrincipal));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuer = true,
                    ValidIssuer = configuration.ValidIssuer,
                    ValidateAudience = false,
                    RequireSignedTokens = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(configuration.Secret))
                };
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = ctx =>
                    {
                        ClaimsPrincipal identity = ctx.Principal as ClaimsPrincipal;
                        if (identity != null)
                        {
                            principalAccessor.CurrentPrincipal = identity;
                        }
                        return Task.CompletedTask;
                    },
                    OnChallenge = ctx =>
                    {
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = ctx =>
                    {
                        if (ctx.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            ctx.Response.Headers.Add("Token-Expired", true.ToString().ToLower());
                        }
                        ctx.Fail("Not Authorized");
                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }
    }
}