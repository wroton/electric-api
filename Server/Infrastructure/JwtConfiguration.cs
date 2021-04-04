using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

using Service.Server.Configuration;

namespace Service.Server.Infrastructure
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
        /// <param name="configuration">Configuration to use.</param>
        /// <returns>Adjusted authentication builder.</returns>
        public static AuthenticationBuilder AddJwtBearerConfiguration(this AuthenticationBuilder builder, IConfiguration configuration)
        {
            // Read the jwt settings.
            var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();

            // Setup the bearer token.
            return builder.AddJwtBearer(options =>
            {
                // Validation options.
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtSettings.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtSettings.Key),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                // Attempt to read the token from the cookie.
                options.Events = new JwtBearerEvents
                { 
                    OnMessageReceived = MessageReceived
                };
            });
        }

        /// <summary>
        /// Called when a message is received. Checks for an authorization cookie if a header isn't present.
        /// </summary>
        /// <param name="context">Message context to check.</param>
        /// <returns>Task processing the message..</returns>
        private static Task MessageReceived(MessageReceivedContext context)
        {
            // Do nothing if there is already an authorization header.
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                return Task.CompletedTask;
            }

            // If there is no authorization cookie, move on.
            if (!context.Request.Cookies.ContainsKey("Authorization"))
            {
                return Task.CompletedTask;
            }

            // Read the authoirzation token from the cookie.
            context.Token = context.Request.Cookies["Authorization"];
            return Task.CompletedTask;
        }
    }
}
