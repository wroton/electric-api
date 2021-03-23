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
        /// Creates a signed jwt token with an id as a claim.
        /// </summary>
        /// <param name="id">Id to store in the token as a claim.</param>
        /// <returns>Signed token.</returns>
        public string Create(int id)
        {
            // Store the user id as a claim.
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, id.ToString())
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
            var tokenHandler = new JwtSecurityTokenHandler();
            var signedToken = tokenHandler.WriteToken(token);
            return signedToken;
        }

        /// <summary>
        /// Reads the id from a signed jwt token.
        /// </summary>
        /// <param name="token">Signed token from which the id should be read.</param>
        /// <returns>Id from the jwt token. Null if the token couldn't be read.</returns>
        public int? Read(string token)
        { 
            if (string.IsNullOrWhiteSpace(token))
            {
                return null;
            }

            // Parameters to instruct the token reader on how the token should be validated and read.
            var key = new SymmetricSecurityKey(_settings.Key);
            var tokenValidationParameters = new TokenValidationParameters
            { 
                IssuerSigningKey = key,
                ValidIssuer = _settings.Issuer
            };

            // Read the token.
            var handler = new JwtSecurityTokenHandler();
            var principal = handler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
            if (validatedToken == null)
            {
                return null;
            }

            // Attempt to parse the "name" as the id.
            if (!int.TryParse(principal.Identity.Name, out int id))
            {
                return null;
            }

            return id;
        }
    }
}
