using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace HousingRepairsOnline.Authentication.Tests.ControllersTests
{
    public static class ControllerTestsHelper
    {
        public static T GetResultData<T>(this IActionResult result) => (T)(result as ObjectResult)?.Value;

        public static int? GetStatusCode(this IActionResult result) => (result as IStatusCodeActionResult).StatusCode;
    }
}
