using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Service.Server.Configuration
{
    /// <summary>
    /// Middleware that handles gathering authorization information per request.
    /// </summary>
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationMiddleware" /> class.
        /// </summary>
        /// <param name="next">Callback to make when executing the next step.</param>
        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        /// <summary>
        /// Called when this middleware step should be executed.
        /// </summary>
        /// <param name="httpContext">Http context of the current request.</param>
        /// <param name="options">Configuration options to use.</param>
        /// <returns>Next step to execute.</returns>
        public Task Invoke(HttpContext httpContext, IOptions<ServiceSettings> options)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));    
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (options.Value == null)
            {
                throw new ArgumentException("Cannot have a null value.", nameof(options));
            }

            // Authorization header overrides the cookie, so check this first.
            var token = (string)null;
            if (httpContext.Request.Headers.ContainsKey("Authorization"))
            {
                token = httpContext.Request.Headers["Authorization"];
            }

            // If there was no token in the authorization header, check for the cookie.
            if (token == null && httpContext.Request.Cookies.ContainsKey("Authorization"))
            {
                token = httpContext.Request.Cookies["Authorization"];
            }

            // If there is no token at this point, then the caller has no authentication.
            if (string.IsNullOrWhiteSpace(token))
            {
                return _next(httpContext);
            }

            // Read the secret key used to validate the token
            // and prepare the handle to validate the token.
            var key = options.Value.JwtKey;
            var handler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            { 
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            // Validate the token.
            var claims = handler.ValidateToken(token, validationParameters, out _);

            // Read the user id.
            if (!int.TryParse(claims.Identity.Name, out int userId))
            {
                return _next(httpContext);
            }

            // Set the user id in the http context.
            httpContext.Session.SetInt32("UserId", userId);

            // Set the user to something so the authorize checks will work.
            httpContext.User = new ClaimsPrincipal();
            return _next(httpContext);
        }
    }
}
