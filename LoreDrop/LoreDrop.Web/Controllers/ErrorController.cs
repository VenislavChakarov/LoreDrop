using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoreDrop.Controllers;

[AllowAnonymous]
public class ErrorController : BaseController
{
    // Handles 404 - Not Found
    [Route("Error/404")]
    public IActionResult Error404()
    {
        return View("Error404");
    }

    // Handles 500 - Internal Server Error
    [Route("Error/500")]
    public IActionResult Error500()
    {
        return View("Error500");
    }

    // Handles other status codes
    [Route("Error/{statusCode}")]
    public IActionResult GeneralError(int statusCode)
    {
        return statusCode switch
        {
            404 => RedirectToAction(nameof(Error404)),
            500 => RedirectToAction(nameof(Error500)),
            _ => View("UnknownError") // Optional: Create UnknownError.cshtml
        };
    }
}