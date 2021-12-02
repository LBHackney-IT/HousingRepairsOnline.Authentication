using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HousingRepairsOnline.Authentication.DependencyInjection
{
    /// <summary>Extensions for <see cref="SwaggerGenOptions"/>.</summary>
    public static class SwaggerGenOptionsExtensions
    {
        /// <summary>Add JWT Security scheme to <see cref="SwaggerGenOptions"/>.</summary>
        /// <remarks>This will enable an option to provide an authentication token via the Swagger UI.</remarks>
        /// <param name="swaggerGenOptions">The <see cref="SwaggerGenOptions"/> to operate upon.</param>
        public static void AddJwtSecurityScheme(this SwaggerGenOptions swaggerGenOptions)
        {
            // Include 'SecurityScheme' to use JWT Authentication
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                Scheme = "bearer",
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            };

            swaggerGenOptions.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { jwtSecurityScheme, Array.Empty<string>() }
            });
        }
    }
}
