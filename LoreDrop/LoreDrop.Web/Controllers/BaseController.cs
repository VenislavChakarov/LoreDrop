using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace LoreDrop.Controllers;

public class BaseController : Controller
{
    protected bool IsUserAuthenticated()
    {
        if (User == null)
        {
            return false;
        }

        if (User.Identity == null)
        {
            return false;
        }

        return User.Identity.IsAuthenticated;
    }

    protected string GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new InvalidOperationException("User ID not found in claims.");;
    }

}