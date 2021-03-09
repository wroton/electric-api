using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.Server.Configuration;
using Service.Server.Services.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Service.Server.Services.Implementations
{
    /// <summary>
    /// Performs JWT related functions.
    /// </summary>
    public sealed class JwtService : IJwtService
    {
        private readonly JwtSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtService" /> class.
        /// </summary>
        /// <param name="options">Option settings to use.</param>
        public JwtService(IOptions<JwtSettings> options)
        { 
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _settings = options.Value ?? throw new ArgumentException("Cannot have a null value.", nameof(options)); ;
        }

        /// <summary>
        /// Creates a signed jwt token for a user.
        /// </summary>
        /// <param name="userId">Id of the user for whom the token is being created.</param>
        /// <returns>Signed token.</returns>
        public string Token(int userId)
        {
            // Store the user id as a claim.
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userId.ToString())
            };

            // Use the configure secret key to sign the tokens.
            var key = new SymmetricSecurityKey(_settings.Key);
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            // Build the token.
            var token = new JwtSecurityToken
            (
                claims: claims,
                issuer: _settings.Issuer,
                expires: TimeProvider.Current.Now.AddDays(1),
                signingCredentials: credentials
            );

            // Write the token.
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var signedToken = tokenHandler.WriteToken(token);
            return signedToken;
        }
    }
}
