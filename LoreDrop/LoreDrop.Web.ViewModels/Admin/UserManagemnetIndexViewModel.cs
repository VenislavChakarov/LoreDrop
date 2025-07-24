namespace LoreDrop.Web.ViewModels.Admin;

public class UserManagemnetIndexViewModel
{
    public string Id { get; set; } = null!;

    public string Email { get; set; } = null!;

    public IEnumerable<string> Roles { get; set; } = null!;
}