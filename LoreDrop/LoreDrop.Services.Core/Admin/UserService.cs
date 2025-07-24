using LoreDrop.Services.Core.Admin.Interface;
using LoreDrop.Web.ViewModels.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LoreDrop.Services.Core.Admin;

public class UserService : IUserService
{
    private readonly UserManager<IdentityUser> userManager;
    
    public UserService(UserManager<IdentityUser> userManager)
    {
        this.userManager = userManager;
    }
    
    public async Task<IEnumerable<UserManagemnetIndexViewModel>> GetUserManagementBoardDataAsync(string userId)
    {
        var users = await this.userManager
            .Users
            .Where(u => u.Id.ToLower() != userId.ToLower())
            .Select(u => new UserManagemnetIndexViewModel
            {
                Id = u.Id,
                Email = u.Email,
                Roles = userManager.GetRolesAsync(u)
                    .GetAwaiter()
                    .GetResult()
            })
            .ToListAsync();

        return users;
    }
}