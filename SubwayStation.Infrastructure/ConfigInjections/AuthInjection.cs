using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SubwayStation.Domain;
using System.Text;

namespace SubwayStation.Infrastructure.ConfigInjections
{
    public static class AuthInjection
    {
        public static void AddAuthenticationConfig(this IServiceCollection services, IConfiguration Configuration)
        {
            var setting = Configuration.GetSection("Settings").Get<AppSetting>();

            services.AddAuthentication(p =>
            {
                p.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                p.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                p.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.JwtSecret))
                };
            });
        }
    }
}
