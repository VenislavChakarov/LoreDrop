using LoreDrop.Areas.Admin.Controllers;
using LoreDrop.Services.Core.Admin.Interface;
using LoreDrop.Web.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoreDrop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class UserManagmentController : BaseAdminController
{
    private readonly IUserService userService;

    public UserManagmentController(IUserService userService)
    {
        this.userService = userService;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<UserManagemnetIndexViewModel> allUsers = await this.userService
            .GetUserManagementBoardDataAsync(this.GetUserId()!);
            
        return View(allUsers);
    }
}