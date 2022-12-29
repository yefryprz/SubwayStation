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
        public static void AddAuthenticationInjection(this IServiceCollection services, IConfiguration Configuration)
        {
            var setting = Configuration.GetSection("Settings").Get<AppSetting>();
            var secret = Encoding.ASCII.GetBytes(setting.JwtSecret);

            services.AddAuthentication(p =>
            {
                p.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                p.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                p.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secret)
                };
            });
        }
    }
}
