using System;
using JWT.Algorithms;
using JWT.Builder;

namespace HousingRepairsOnline.Authentication.Helpers
{
    /// <summary>A JWT Helper.</summary>
    public class JwtHelper : IJwtHelper
    {
        private readonly string secret;
        private string issuer;
        private string audience;

        /// <summary>The constructor.</summary>
        /// <param name="secret">Secret to use when generating JWT.</param>
        /// <param name="issuer">Issuer of the token.</param>
        /// <param name="audience">Audience of the token.</param>
        public JwtHelper(string secret, string issuer, string audience)
        {
            this.secret = secret;
            this.issuer = issuer;
            this.audience = audience;
        }

        /// <summary>Generates a JWT with a 1 minute expiry.</summary>
        /// <returns>A JWT.</returns>
        public string Generate()
        {
            var result = JwtBuilder.Create()
                .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric
                .WithSecret(this.secret)
                .ExpirationTime(DateTimeOffset.UtcNow.AddMinutes(1).ToUnixTimeSeconds())
                .Issuer(this.issuer)
                .Audience(this.audience)
                .Encode();

            return result;
        }
    }
}
