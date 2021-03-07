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
        /// <param name="key">Key used to sign tokens.</param>
        /// <returns>Adjusted authentication builder.</returns>
        public static AuthenticationBuilder AddJwtBearerConfiguration(this AuthenticationBuilder builder, byte[] key)
        { 
            return builder.AddJwtBearer(options =>
            {
                // Validation options.
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
