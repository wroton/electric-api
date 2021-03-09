using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Service.Server.Configuration
{
    /// <summary>
    /// Sets up jwt configuration for the application.
    /// </summary>
    public static class JwtConfiguration
    {
        /// <summary>
        /// Configures the authentication pipeline to use a jwt bearer token.
        /// </summary>
        /// <param name="builder">Authentication builder to use.</param>
        /// <returns>Adjusted authentication builder.</returns>
        public static AuthenticationBuilder AddJwtBearerConfiguration(this AuthenticationBuilder builder)
        { 
            return builder.AddJwtBearer(options =>
            {
                // Validation options.
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(new byte[4] { 1, 2, 3, 4 }),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
