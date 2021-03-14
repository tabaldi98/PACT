using app.Tabaldi.PACT.Application.AttendanceRecurrenceAgg;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;

namespace app.Tabaldi.PACT.Api.Extensions
{
    public static class AuthenticationExtensions
    {
        public static void AddJwtBearerAuthentication(this IServiceCollection services, IJwtOptions jwtOptions)
        {
            services.AddSingleton(jwtOptions);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = jwtOptions.RequireHttpsMetadata;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = jwtOptions.SymmetricSecurityKey,
                            ValidateIssuer = true,
                            ValidIssuer = jwtOptions.Issuer,
                            ValidateAudience = true,
                            ValidAudience = jwtOptions.Audience,
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero
                        };
                    });
        }
    }
}
