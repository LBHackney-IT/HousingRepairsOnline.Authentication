using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HousingRepairsOnline.Authentication.Controllers
{
    using Helpers;

    /// <summary>A controller for authentication.</summary>
    /// <remarks></remarks>
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IIdentifierValidator identifierValidator;
        private readonly IJwtHelper jwtHelper;

        /// <inheritdoc />
        public AuthenticationController(IIdentifierValidator identifierValidator, IJwtHelper jwtHelper)
        {
            this.identifierValidator = identifierValidator;
            this.jwtHelper = jwtHelper;
        }

        /// <summary>An endpoint to authenticate.</summary>
        /// <param name="identifier">The identifier to validate against.</param>
        /// <returns>If <paramref name="identifier"/> is valid, status Ok (200) and a JWT with a 1 minute expiry;
        /// otherwise Unauthorised (401).</returns>
        [HttpPost]
        public async Task<IActionResult> Authenticate(string identifier)
        {
            IActionResult result;
            if (!identifierValidator.Validate(identifier))
            {
                result = Unauthorized();
            }
            else
            {
                var token = this.jwtHelper.Generate();
                result = Ok(token);
            }

            return await Task.FromResult(result);
        }
    }
}
