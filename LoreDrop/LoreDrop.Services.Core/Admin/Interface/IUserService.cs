using LoreDrop.Web.ViewModels.Admin;

namespace LoreDrop.Services.Core.Admin.Interface;

public interface IUserService
{
    Task<IEnumerable<UserManagemnetIndexViewModel>> GetUserManagementBoardDataAsync(string userId);
}