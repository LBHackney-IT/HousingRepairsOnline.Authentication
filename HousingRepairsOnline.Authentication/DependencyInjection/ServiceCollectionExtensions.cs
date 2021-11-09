using System;
using System.Linq;
using System.Security.Claims;
using HousingRepairsOnline.Authentication.Helpers;
using JWT;
using JWT.Algorithms;
using Microsoft.Extensions.DependencyInjection;

namespace HousingRepairsOnline.Authentication.DependencyInjection
{
    /// <summary>
    /// Registration extensions for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        private const string AuthenticationIdentifier = "AUTHENTICATION_IDENTIFIER";

        private const string HousingRepairsOnline = "Housing Repairs Online";

        /// <summary>Adds services required for authentication.</summary>
        /// <remarks>The following environment variables are used for configuration and are mandatory:
        /// <list type="bullet">
        ///     <item>JWT_SECRET</item>
        ///     <item>AUTHENTICATION_IDENTIFIER</item>
        /// </list>
        /// If any are not configured an <see cref="InvalidOperationException"/> will be thrown.
        /// </remarks>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="issuer">An identifier of the service creating the authentication token, i.e. project/API name.
        /// This will be present within the JWT field 'issuer'./// </param>
        /// <exception cref="InvalidOperationException">Thrown when environment variable is not configured.</exception>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddHousingRepairsOnlineAuthentication(this IServiceCollection services, string issuer)
        {
            var authenticationIdentifier = GetEnvironmentVariable(AuthenticationIdentifier);
            services.AddTransient<IIdentifierValidator, IdentifierValidator>(_ =>
                new IdentifierValidator(authenticationIdentifier));

            var jwtSecret = GetEnvironmentVariable("JWT_SECRET");
            services.AddTransient<IJwtHelper, JwtHelper>(_ =>
                new JwtHelper(jwtSecret, issuer, HousingRepairsOnline));

            var authenticationScheme = JwtAuthenticationDefaults.AuthenticationScheme;

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = authenticationScheme;
                    options.DefaultChallengeScheme = authenticationScheme;
                })
                .AddJwt(authenticationScheme, options =>
                {
                    // secrets, required only for symmetric algorithms
                    options.Keys = new[] { jwtSecret };

                    // force JwtDecoder to throw exception if JWT signature is invalid
                    options.VerifySignature = true;

                    options.IdentityFactory = dic => new ClaimsIdentity(dic.Select(p => new Claim(p.Key, p.Value)), authenticationScheme);
                });

            services.AddSingleton<IAlgorithmFactory>(_ => new DelegateAlgorithmFactory(() => new HMACSHA256Algorithm()));

            return services;
        }

        /// <summary>Gets the environment variable specified by '<paramref name="name"/>'.</summary>
        /// <param name="name">The name of the environment variable.</param>
        /// <returns>The value of the environment variable.</returns>
        /// <exception cref="InvalidOperationException">Thrown when environment variable is not found.</exception>
        private static string GetEnvironmentVariable(string name)
        {
            return Environment.GetEnvironmentVariable(name) ??
                   throw new InvalidOperationException($"Incorrect configuration: '{name}' environment variable must be set");
        }
    }
}
